using Application.Interfaces;
using Application.Mapper;
using Domain.Entities;
using Infranstructure;
using JasperFx;
using JasperFx.CodeGeneration;
using JasperFx.Core;
using Microsoft.OpenApi.Models;
using Nest;
using Wolverine;
using Wolverine.ErrorHandling;
using Wolverine.Http;
using Wolverine.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJasperFx(cfg =>
{
    if (!builder.Environment.IsDevelopment())
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

builder.Services.AddAuthorization();

builder.Services.AddWolverineHttp();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Search Service API",
        Version = "v1",
    });
});


var rabbitMqHost =
    //builder.Configuration["RabbitMq__Host"] ??
    builder.Configuration["RabbitMq:Host"] ??
    "amqp://guest:guest@rabbitmq:5672";

builder.Host.UseWolverine(opts =>
{
    opts.UseRabbitMq(rabbitMqHost)
        .AutoProvision()
        .UseConventionalRouting(x=>
        {
            x.QueueNameForListener(type  => type.Name + "search");
        });

    opts.Discovery.IncludeAssembly(typeof(Messages.Catalog.Events.Books.BookMadeAvailableMessage).Assembly);
    opts.Discovery.IncludeAssembly(typeof(Messages.Borrowing.Events.BookFoundMessage).Assembly);

    opts.Policies.OnException<TimeoutException>().ScheduleRetry(5.Seconds());
});


builder.Services.Configure<ElasticsearchSettings>(builder.Configuration.GetSection("Elasticsearch"));

builder.Services.AddSingleton<IElasticsearchClient, ElasticsearchClient>();
builder.Services.AddSingleton<IElasticClient>(opt =>
{
    var factory = opt.GetRequiredService<IElasticsearchClient>();
    return factory.CreateClient();
});

builder.Services.AddSingleton<ISearchIndex<Book>>(opt =>
    new BookSearchIndex(opt.GetRequiredService<IElasticClient>()));
builder.Services.AddSingleton<ISearchIndex<Member>>(opt =>
    new MemberSearchIndex(opt.GetRequiredService<IElasticClient>()));

builder.Services.AddAutoMapper(cfg => { },
    typeof(MappingProfile).Assembly);

//builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Search Service API v1");
        c.RoutePrefix = string.Empty;
    });
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapWolverineEndpoints();


return await app.RunJasperFxCommands(args);