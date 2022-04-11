using System.Reflection;
using Application;
using Authentication;
using Infrastructure;
using Infrastructure.Persistence.Database;
using wms;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication()
    .AddApplicationAutomapper(new[]
    {
        Assembly.GetExecutingAssembly(),
        Assembly.GetAssembly(typeof(Infrastructure.DependencyInjection))!
    });
builder.Services.AddApplicationAuthentication(builder.Configuration);

builder.Services
    .AddApplicationControllers()
    .AddSwaggerDocumentation();

var app = builder.Build();

app.EnsureDatabaseOps();

// Configure the HTTP request pipeline.

app.UseCors(
    policy => policy
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
);

app.UseSwaggerMiddlewares();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();