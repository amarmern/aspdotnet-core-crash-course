using GameStore.API.Data;
using GameStore.API.Dtos;
using GameStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Endpoints;

public static class GamesEndPoints
{
    const string GetEndpointName = "GetGame";
    //     private static readonly List<GameSummaryDto> games = [
    //     new (1,"Street Fieghter 1","Fighting 1",19.9M, new DateOnly(1992, 7, 15)),
    //     new (2,"Street Fieghter 2","Fighting 2",20.9M, new DateOnly(2000, 8, 11)),
    //     new (3,"Street Fieghter 3","Fighting 3",66.9M, new DateOnly(2026, 9, 14)),
    // ];

    public static void MapGamesEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        //GET/games
        group.MapGet("/", async (GameStoreContext dbContext) =>
        {
            var games = await dbContext.Games
            .Include(game => game.Genre)
            .Select(game => new GameSummaryDto(
                game.Id,
                game.Name,
                game.Genre!.Name,
                game.Price,
                game.ReleaseDate
            ))
            .AsNoTracking()
            .ToListAsync();
            return Results.Ok(games);
        });

        //GET/games/:id
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            var game = await dbContext.Games.FindAsync(id);
            return game is null ? Results.NotFound() : Results.Ok(new GameDetailsDto(
                game.Id,
                game.Name,
                game.GenreId,
                game.Price,
                game.ReleaseDate
            ));
        })
        .WithName(GetEndpointName);
        //POST/games
        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            Game game = new()
            {
                Name = newGame.Name,
                GenreId = newGame.GenreId,
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate
            };

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            GameDetailsDto gameDetailsDto = new(
                game.Id,
                game.Name,
                game.GenreId,
                game.Price,
                game.ReleaseDate
            );

            return Results.CreatedAtRoute(GetEndpointName, new { id = gameDetailsDto.Id }, gameDetailsDto);
        });

        //PUT/games/:id

        group.MapPut("/{id}", async (int id, UpdateGameDto updateGameDto, GameStoreContext dbContext) =>
       {
           var existingGame = await dbContext.Games.FindAsync(id);
           if (existingGame is null)
           {
               return Results.NotFound();
           }

           existingGame.Name = updateGameDto.Name ?? existingGame.Name;
           await dbContext.SaveChangesAsync();
           return Results.NoContent();
       });

        //DELETE/games/:id
        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            var existingGame = await dbContext.Games.FindAsync(id);
            if (existingGame is null)
            {
                return Results.NotFound();
            }

            dbContext.Games.Remove(existingGame);
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        });
    }
}
