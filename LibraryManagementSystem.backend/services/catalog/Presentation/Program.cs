using Application.Interfaces;
using Application.Mapper;
using ImTools;
using Infranstructure.Persistence;
using Infranstructure.Projections;
using JasperFx;
using JasperFx.CodeGeneration;
using JasperFx.Core;
using JasperFx.Events.Projections;
using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
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

builder.Services.AddJasperFx(cfg =>
{
    if (builder.Environment.IsDevelopment())
    {
        cfg.Development.GeneratedCodeMode = TypeLoadMode.Dynamic;
        cfg.Development.AssertAllPreGeneratedTypesExist = false;

    }
    else
    {
        cfg.Production.GeneratedCodeMode = TypeLoadMode.Static;
        cfg.Production.AssertAllPreGeneratedTypesExist = true;

    }
});

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

var rabbitMqHost =
    builder.Configuration["RabbitMq:Host"] ??
    "amqp://guest:guest@rabbitmq:5672";



builder.Services.AddMarten(opts =>
{
    opts.Connection(connectionString);
    opts.DatabaseSchemaName = "catalog";
    opts.AutoCreateSchemaObjects = AutoCreate.All;

    opts.Projections.Add<BookProjection>(ProjectionLifecycle.Inline);
    opts.Projections.Add<MemberProjection>(ProjectionLifecycle.Inline);

    opts.CreateDatabasesForTenants(c =>
        {
            c.MaintenanceDatabase(connectionString);
            c.ForTenant()
            .CheckAgainstPgDatabase()
            .WithOwner("postgres")
            .WithEncoding("UTF-8")
            .ConnectionLimit(-1);
        });
})

.IntegrateWithWolverine();
builder.Services.AddMartenStore<IApplicationStore>(opts =>
{
    opts.Connection(connectionString);
})
.ApplyAllDatabaseChangesOnStartup();



builder.Host.UseWolverine(opts =>
{
    opts.UseRabbitMq(rabbitMqHost).AutoProvision()
    .UseConventionalRouting(x =>
    {
        x.QueueNameForListener(type => type.Name + "catalog");
    });

    opts.Discovery.IncludeAssembly(typeof(Messages.Catalog.Events.Books.BookMadeAvailableMessage).Assembly);
});

builder.Services.AddScoped<IEventStore, MartenEventStore>();
builder.Services.AddScoped<IReadStore, MartenReadStore>();
builder.Services.AddAutoMapper(cfg => { },
    typeof(MappingProfile).Assembly);


builder.Host.ApplyJasperFxExtensions();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    CreateDatabaseIfNotExists(connectionString);
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
            Console.WriteLine($" Database '{databaseName}' created automatically.");
        }
        else
        {
            Console.WriteLine($" Database '{databaseName}' already exists.");
        }
    }
}