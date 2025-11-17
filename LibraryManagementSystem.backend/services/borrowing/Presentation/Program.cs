using Application.Interfaces;
using Application.Mapper;
using Infranstructure.Persistence;
using Infranstructure.Projections;
using JasperFx;
using JasperFx.Events.Projections;
using Marten;
using Messages.Borrowing;
using Messages.Borrowing.Requests;
using Npgsql;
using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;
using Wolverine.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
builder.Services.AddAuthorization();

builder.Services.AddWolverineHttp();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Catalog Service API",
        Version = "v1",
        Description = "API endpoints for managing catalog books"
    });
});

var connectionString = builder.Configuration.GetConnectionString("Postgres") ?? "Host=localhost;Port=5432;Database=borrowingdb;Username=postgres;Password=1234";

var rabbitMqHost = builder.Configuration["RabbitMq:Host"] ?? "amqp://guest:guest@localhost:5672";


CreateDatabaseIfNotExists(connectionString);

builder.Services.AddMarten(opts =>
{
    opts.Connection(connectionString);
    opts.DatabaseSchemaName = "borrowing";
    opts.AutoCreateSchemaObjects = AutoCreate.All;

    opts.Projections.Add<BorrowProjection>(ProjectionLifecycle.Inline);
})
.IntegrateWithWolverine();


builder.Host.UseWolverine(opts =>
{
    opts.UseRabbitMq(rabbitMqHost).AutoProvision();

    //opts.PublishAllMessages().ToRabbitTopics("library.topics", exchange =>
    //    {
    //        exchange.BindTopic("catalog.book.*").ToQueue("borrowing.catalog");

    //        exchange.BindTopic("catalog.member.*").ToQueue("borrowing.catalog");

    //        exchange.BindTopic("catalog.*").ToQueue("borrowing.catalog");
    //    });
    opts.PublishMessage<TestPing>()
    .ToRabbitQueue("catalog-test");

    opts.PublishMessage<GetMemberStatusRequest>()
       .ToRabbitQueue("catalog-requests");

    opts.PublishMessage<GetBookAvailabilityRequest>()
        .ToRabbitQueue("catalog-requests");
    
    opts.PublishMessage<TestPingRequest>()
    .ToRabbitQueue("catalog-requests");
});

builder.Services.AddScoped<IEventStore, MartenEventStore>();
builder.Services.AddScoped<IReadStore, MartenReadStore>();
builder.Services.AddAutoMapper(cfg => { },
    typeof(MappingProfile).Assembly);


//builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Borrowing Service API v1");
        c.RoutePrefix = string.Empty;
    });
}

//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapWolverineEndpoints();

//app.MapControllers();

return await app.RunJasperFxCommands(args);


