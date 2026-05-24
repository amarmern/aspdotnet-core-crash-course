using GameStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider
                        .GetRequiredService<GameStoreContext>();
        dbContext.Database.Migrate();
    }

    public static void AddGameStoreData(this WebApplicationBuilder builder)
    {
        // Data initialization logic can be added here
        var connectionString = "Data Source=GameStore.db";
        builder.Services.AddSqlite<GameStoreContext>(connectionString,
            optionsAction: options => options.UseSeeding((context, _) =>
            {
                if (!context.Set<Genre>().Any())
                {
                    context.Set<Genre>().AddRange(
                        new Genre { Name = "Action" },
                        new Genre { Name = "Adventure" },
                        new Genre { Name = "RPG" },
                        new Genre { Name = "Strategy" },
                        new Genre { Name = "Sports" },
                        new Genre { Name = "Simulation" },
                        new Genre { Name = "Racing" }
                    );
                    context.SaveChanges();
                }
            })
        );


    }
}
