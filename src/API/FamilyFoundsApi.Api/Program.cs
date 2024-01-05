using System.Runtime.CompilerServices;
using FamilyFoundsApi.Api;
using FamilyFoundsApi.Core;
using FamilyFoundsApi.Persistance;

const string LOCAL_ORIGIN = nameof(LOCAL_ORIGIN);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(o => 
    o.AddDefaultPolicy(policy => 
        policy
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins("http://localhost:3000")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCoreServices();
builder.Services.AddPersistanceServices(builder.Configuration);

builder.Services.AddSingleton<IMediator, FamilyFoundsContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.UseCors();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

var api = app.MapGroup("api");

api.MapTransactionEndpoints();
api.MapCategoryEndpoints();
api.MapImportSourceEndpoints();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
