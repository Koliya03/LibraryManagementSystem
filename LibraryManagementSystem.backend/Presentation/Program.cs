using Application.Interfaces;
using Application.Mapper;
using Infranstructure.Persistence;
using JasperFx;
using Marten;
using Microsoft.AspNetCore.Builder;
using Wolverine;
using Wolverine.ErrorHandling;
using Wolverine.Marten;
using Wolverine.Postgresql;
using Wolverine.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("Postgres") ?? "Host=localhost;Port=5432;Database=catalogdb;Username=postgres;Password=1234";

var rabbitMqHost = builder.Configuration["RabbitMq:Host"]?? "amqp://guest:guest@localhost:5672";

var rabbitMqQueue = builder.Configuration["RabbitMq:Queue"] ?? "catalog_queue";

builder.Services.AddMarten(opts =>
{
    opts.Connection(connectionString);
    opts.DatabaseSchemaName = "catalog"; 
    opts.AutoCreateSchemaObjects = AutoCreate.All;
})
    .IntegrateWithWolverine();

builder.Host.UseWolverine(opts =>
{
    opts.UseRabbitMq(rabbitMqHost)
        .AutoProvision();

    opts.PublishAllMessages()
         .ToRabbitExchange("library.events");
    //opts.ListenToRabbitQueue("catalog.queue")
    //.BindExchange("library.events", binding =>
    //{
    //    binding.RoutingKey("book.borrowed");
    //    binding.RoutingKey("book.returned");
    //});


    opts.PersistMessagesWithPostgresql(connectionString, schemaName: "catalog_wolverine");

});

builder.Services.AddScoped<IEventStore, MartenEventStore>();
builder.Services.AddScoped<IReadStore, MartenReadStore>();
builder.Services.AddAutoMapper(cfg => { },
    typeof(MappingProfile).Assembly);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwagger();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
