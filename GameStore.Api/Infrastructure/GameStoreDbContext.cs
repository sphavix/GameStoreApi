using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Infrastructure;

public class GameStoreDbContext 
    : DbContext
{
    public GameStoreDbContext(DbContextOptions<GameStoreDbContext> options) : base(options){}

    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre> Genres => Set<Genre>();
    
}
