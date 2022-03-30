using System.Reflection;
using wms.Dto.Responses.Validation;
using wms.Filters;

using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication()
    .AddApplicationAutomapper(new []
    {
        Assembly.GetExecutingAssembly(),
        Assembly.GetAssembly(typeof(Infrastructure.DependencyInjection))!
    });

builder.Services
    .AddControllers(options => { options.Filters.Add<ExceptionFilter>(); })
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory =
            actionContext => new BadRequestObjectResult(actionContext.ModelState);
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();