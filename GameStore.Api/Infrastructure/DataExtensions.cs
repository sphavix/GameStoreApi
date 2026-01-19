using System;
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
}
