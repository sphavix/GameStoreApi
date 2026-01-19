using System;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Infrastructure;

public static class DataExtensions
{
    public static void MigrateDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreDbContext>();
        dbContext.Database.Migrate();
    }

    public static void AddGameStoreDatabase(this WebApplicationBuilder builder)
    {
        var connectionString = "Data Source=GameStore.db";

        builder.Services.AddSqlite<GameStoreDbContext>(
            connectionString,
            optionsAction: options => options.UseSeeding((context, _) =>
            {
                if (!context.Set<Genre>().Any())
                {
                    context.Set<Genre>().AddRange(
                        new Genre { Name = "Sports" },
                        new Genre { Name = "Racing" },
                        new Genre { Name = "Arcade" },
                        new Genre { Name = "Fights" },
                        new Genre { Name = "Platformer" }
                    );

                    context.SaveChanges();
                }
            }));
    }
}
