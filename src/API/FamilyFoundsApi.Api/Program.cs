using FamilyFoundsApi.Api;
using FamilyFoundsApi.Api.Mediator;
using FamilyFoundsApi.Core;
using FamilyFoundsApi.Core.Contracts.API;
using FamilyFoundsApi.Persistence;
using FamilyFoundsApi.Infrastructure;
using System.Text.Json.Serialization;
using FamilyFoundsApi.Api.Extensions;

const string LOCAL_ORIGIN = nameof(LOCAL_ORIGIN);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(o => 
        o.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(o => 
    o.AddPolicy(LOCAL_ORIGIN, policy => 
        policy
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithExposedHeaders("Content-Type", "Content-Length", "Cache-Control")
            .WithOrigins("http://localhost:3000")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCoreServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices();

builder.Services.AddSingleton<IMediator, FamilyFoundsContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors(LOCAL_ORIGIN);

app.Use(next => context =>
{
    context.Request.EnableBuffering();
    return next(context);
});

app.UseMiddleware<ExceptionHandlingMiddleware>();

var api = app.MapGroup("api");

api.MapTransactionEndpoints();
api.MapCategoryEndpoints();
api.MapImportSourceEndpoints();

app.MapControllers();

app.Run();