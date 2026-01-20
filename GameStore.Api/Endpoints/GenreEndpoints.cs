using System;
using GameStore.Api.Dtos;
using GameStore.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GenreEndpoints
{
    public static void MapGenreEndpoints(this WebApplication app)
    {
        var endpointsGroup = app.MapGroup("/genres");

        // GET: /genres
        endpointsGroup.MapGet("/", async(GameStoreDbContext context) 
            => await context.Genres.Select(genre => 
                    new GenreDto(
                        genre.Id, 
                        genre.Name))
                        .AsNoTracking()
                        .ToListAsync());
    }
}
