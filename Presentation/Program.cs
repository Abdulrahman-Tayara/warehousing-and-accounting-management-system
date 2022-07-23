using System.Reflection;
using Application;
using Authentication;
using Authorization;
using Infrastructure;

using wms;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.local.json", true, true);

builder.Services.AddApplication()
    .AddApplicationAutomapper(new[]
    {
        Assembly.GetExecutingAssembly(),
        Assembly.GetAssembly(typeof(Infrastructure.DependencyInjection))!
    });

builder.Services.AddApplicationAuthentication(builder.Configuration);
builder.Services.AddApplicationAuthorization();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services
    .AddApplicationControllers()
    .AddCurrentUserService()
    .AddSwaggerDocumentation();

var app = builder.Build();

await app.EnsureDatabaseOps();

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

app.UseCors(options =>
{
    options.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});

app.Run();