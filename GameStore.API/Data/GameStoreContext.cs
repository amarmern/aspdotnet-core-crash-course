using GameStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Data;

public class GameStoreContext : DbContext
{
    public GameStoreContext(DbContextOptions<GameStoreContext> options) : base(options)
    {
    }

    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre> Genres => Set<Genre>();
}