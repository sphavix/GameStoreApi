using System;
using GameStore.Api.Dtos;
using GameStore.Api.Infrastructure;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpoint = "GetGame";

    public static void MapGamesEndpoints(this WebApplication app)
    {

        var endpointsGroup = app.MapGroup("/games");

        // GET: /games
        endpointsGroup.MapGet("/", async (GameStoreDbContext context) 
            => await context.Games.Include(game => game.Genre)
                            .Select(game => new GameDto(
                                game.Id,
                                game.Name,
                                game.Genre!.Name,
                                game.Price,
                                game.ReleaseDate
                            ))
                            .AsNoTracking()
                            .ToListAsync());

        // GET: /games/{id}
        endpointsGroup.MapGet("/{id}", async (int id, GameStoreDbContext context) =>
        {
            var game = await context.Games.FirstOrDefaultAsync( game => game.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(
                new GameDetailsDto(
                game.Id,
                game.Name,
                game.GenreId,
                game.Price,
                game.ReleaseDate
            ));
        })
        .WithName(GetGameEndpoint);

        // POST: /games
        endpointsGroup.MapPost("/", async (CreateGameDto game, GameStoreDbContext context) =>
        {
            Game newGame = new()
            {
                Name = game.Name,
                GenreId = game.GenreId,
                Price = game.Price,
                ReleaseDate = game.ReleaseDate
            };

            context.Games.Add(newGame);
            await context.SaveChangesAsync();

            GameDetailsDto gameDto = new(
                newGame.Id,
                newGame.Name,
                newGame.GenreId,
                newGame.Price,
                newGame.ReleaseDate
            );

            return Results.CreatedAtRoute(GetGameEndpoint, new { id = gameDto.Id }, gameDto);
        });

        // PUT: /games/{id}
        endpointsGroup.MapPut("/{id}", async (int id, UpdateGameDto game, GameStoreDbContext context) =>
        {
            var gameToUpdate = await context.Games.FirstOrDefaultAsync(game => game.Id == id);

            if(gameToUpdate is null)
            {
                return default;
            }

            gameToUpdate.Name = game.Name;
            gameToUpdate.Price = game.Price;
            gameToUpdate.GenreId = game.GenreId;
            gameToUpdate.ReleaseDate = game.ReleaseDate;

            context.Games.Update(gameToUpdate);
            await context.SaveChangesAsync();

            return Results.NoContent();
        });

        // DELETE: /games/{id}
        endpointsGroup.MapDelete("/{id}", async (int id, GameStoreDbContext context) =>
        {
            await context.Games.Where(game => game.Id == id).ExecuteDeleteAsync();

            return Results.NoContent();
        });
    }

}
