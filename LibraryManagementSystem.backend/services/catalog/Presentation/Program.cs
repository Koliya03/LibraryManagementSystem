using Application.Interfaces;
using Application.Mapper;
using Infranstructure.Persistence;
using Infranstructure.Projections;
using JasperFx;
using JasperFx.Core;
using JasperFx.Events.Projections;
using Marten;
using Microsoft.AspNetCore.Builder;
using Npgsql;
using Oakton;
using Presentation;
using Presentation.Consumer;
using Wolverine;
using Wolverine.ErrorHandling;
using Wolverine.Http;
using Wolverine.Marten;
using Wolverine.Postgresql;
using Wolverine.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);



//builder.Services.AddControllers();
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

var connectionString = builder.Configuration.GetConnectionString("Postgres") ?? "Host=localhost;Port=5432;Database=catalogdb;Username=postgres;Password=1234";

var rabbitMqHost = builder.Configuration["RabbitMq:Host"]?? "amqp://guest:guest@localhost:5672";


CreateDatabaseIfNotExists(connectionString);

builder.Services.AddMarten(opts =>
{
    opts.Connection(connectionString);
    opts.DatabaseSchemaName = "catalog";
    opts.AutoCreateSchemaObjects = AutoCreate.All;

    opts.Projections.Add<BookProjection>(ProjectionLifecycle.Inline);
    opts.Projections.Add<MemberProjection>(ProjectionLifecycle.Inline);
})
.IntegrateWithWolverine();



builder.Host.UseWolverine(opts =>
{

    opts.Discovery.IncludeAssembly(typeof(GetMemberStatusHandler).Assembly);
    opts.Discovery.IncludeAssembly(typeof(GetBookAvailabilityHandler).Assembly);

    opts.UseRabbitMq(rabbitMqHost).AutoProvision();
    //opts.PublishAllMessages().ToRabbitTopics("library.topics");

    //// Console.WriteLine(opts.DescribeHandlerMatch(typeof(RegisterBookHandler)));
    ////opts.Discovery.IncludeAssembly(typeof(RegisterBookHandler).Assembly);
    ////opts.Discovery.IncludeAssembly(typeof(RegisterMemberHandler).Assembly);

    //opts.ListenToRabbitQueue("catalog-test");
    //opts.ListenToRabbitQueue("catalog-requests");

    //opts.Discovery.IncludeAssembly(typeof(TestPingHandler).Assembly);
    //opts.Discovery.IncludeAssembly(typeof(TestPingRequestHandler).Assembly);

    opts.PublishAllMessages().ToRabbitQueue("catalog-borrowing");
    opts.ListenToRabbitQueue("borrowing-catalog");
});

builder.Services.AddScoped<IEventStore, MartenEventStore>();
builder.Services.AddScoped<IReadStore, MartenReadStore>();
builder.Services.AddAutoMapper(cfg => { },
    typeof(MappingProfile).Assembly);


builder.Host.ApplyJasperFxExtensions();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog Service API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();



app.MapWolverineEndpoints();


//app.Run();
return await app.RunJasperFxCommands(args);



static void CreateDatabaseIfNotExists(string connectionString)
{
    var builder = new NpgsqlConnectionStringBuilder(connectionString);
    var databaseName = builder.Database;
    builder.Database = "postgres"; 

    using var connection = new NpgsqlConnection(builder.ConnectionString);
    connection.Open();

    using (var cmd = new NpgsqlCommand(
        $"SELECT 1 FROM pg_database WHERE datname = '{databaseName}'", connection))
    {
        var exists = cmd.ExecuteScalar() != null;
        if (!exists)
        {
            using var create = new NpgsqlCommand($"CREATE DATABASE \"{databaseName}\"", connection);
            create.ExecuteNonQuery();
            Console.WriteLine($"✅ Database '{databaseName}' created automatically.");
        }
        else
        {
            Console.WriteLine($"ℹ️ Database '{databaseName}' already exists.");
        }
    }
}