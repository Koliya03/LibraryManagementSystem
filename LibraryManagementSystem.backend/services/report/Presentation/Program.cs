using Application.Services;
using JasperFx;
using Microsoft.OpenApi.Models;
using Wolverine;
using Wolverine.Http;
using Wolverine.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthorization();

builder.Services.AddWolverineHttp();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Reporting Service API",
        Version = "v1",
    });
});
var rabbitMqHost = builder.Configuration["RabbitMq:Host"]
                   ?? "amqp://guest:guest@localhost:5672";

builder.Host.UseWolverine(opts =>
{
    opts.UseRabbitMq(rabbitMqHost)
        .AutoProvision()
        .UseConventionalRouting(x =>
        {
            x.QueueNameForListener(type => type.Name + "reportng");
        });

});

builder.Services.AddScoped<MemberHistoryReportService>();
builder.Services.AddScoped<TopBorrowedBooksReportService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reporting API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapWolverineEndpoints();
return await app.RunJasperFxCommands(args);
