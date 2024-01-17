using FamilyFoundsApi.Api;
using FamilyFoundsApi.Api.Mediator;
using FamilyFoundsApi.Core;
using FamilyFoundsApi.Core.Contracts.API;
using FamilyFoundsApi.Persistance;
using FamilyFoundsApi.Infrastructure;

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
builder.Services.AddInfrastructureServices();

builder.Services.AddSingleton<IMediator, FamilyFoundsContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseMiddleware<ExceptionHandlingMiddleware>();

var api = app.MapGroup("api");

api.MapTransactionEndpoints();
api.MapCategoryEndpoints();
api.MapImportSourceEndpoints();

app.Run();