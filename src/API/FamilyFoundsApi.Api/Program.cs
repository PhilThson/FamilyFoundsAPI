using FamilyFoundsApi.Api.Mediator;
using FamilyFoundsApi.Core;
using FamilyFoundsApi.Core.Contracts.API;
using FamilyFoundsApi.Persistence;
using FamilyFoundsApi.Infrastructure;
using System.Text.Json.Serialization;
using FamilyFoundsApi.Api.CustomMiddleware;
using FamilyFoundsApi.Api.Extensions;
using FamilyFoundsApi.Core.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(o => 
        o.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

builder.Services.AddHttpContextAccessor();

builder.Services.ConfigureCors(builder.Configuration, FFConstants.CorsPolicy);
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup => 
    setup.AddSecurityDefinition("FamilyFounds API Authorization", new OpenApiSecurityScheme()
    {
        BearerFormat = "Bearer {value}",
        Description = "Enter Bearer Token",
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Type = SecuritySchemeType.Http
    }));

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

app.UseHttpsRedirection();

app.UseCors(FFConstants.CorsPolicy);

app.Use(next => context =>
{
    context.Request.EnableBuffering();
    return next(context);
});

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

var api = app.MapGroup("api")
    .RequireAuthorization(FFConstants.AuthorizationPolicy);

api.MapTransactionEndpoints();
api.MapCategoryEndpoints();
api.MapImportSourceEndpoints();

app.MapControllers();

app.Run();