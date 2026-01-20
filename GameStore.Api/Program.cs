using GameStore.Api.Endpoints;
using GameStore.Api.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddValidation();

builder.AddGameStoreDatabase();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "Games API";
        options.DarkMode = true;
    });
}


app.MapGamesEndpoints();
app.MapGenreEndpoints();

app.MigrateDatabase();

app.Run();
