using GameStore.Api.Endpoints;
using GameStore.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddValidation();

builder.AddGameStoreDatabase();



var app = builder.Build();

app.MapGamesEndpoints();

app.MigrateDatabase();

app.Run();
