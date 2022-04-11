using System.Reflection;
using Application;
using Authentication;
using Infrastructure;
using wms;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication()
    .AddApplicationAutomapper(new []
    {
        Assembly.GetExecutingAssembly(),
        Assembly.GetAssembly(typeof(Infrastructure.DependencyInjection))!
    });
builder.Services.AddApplicationAuthentication(builder.Configuration);

builder.Services
    .AddApplicationControllers()
    .AddSwaggerDocumentation();

var app = builder.Build();

// Configure the HTTP request pipeline.

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