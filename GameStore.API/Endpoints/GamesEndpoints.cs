using GameStore.API.Dtos;

namespace GameStore.API.Endpoints;

public static class GamesEndPoints
{
    const string GetEndpointName = "GetGame";
    private static readonly List<GameDto> games = [
    new (1,"Street Fieghter 1","Fighting 1",19.9M, new DateOnly(1992, 7, 15)),
    new (2,"Street Fieghter 2","Fighting 2",20.9M, new DateOnly(2000, 8, 11)),
    new (3,"Street Fieghter 3","Fighting 3",66.9M, new DateOnly(2026, 9, 14)),
];

    public static void MapGamesEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        //GET/games
        group.MapGet("/", () => games);

        //GET/games/:id
        group.MapGet("/{id}", (int id) =>
        {
            var game = games.Find(game => game.Id == id);
            return game is not null ? Results.Ok(game) : Results.NotFound();
        })
        .WithName(GetEndpointName);

        //POST/games
        group.MapPost("/", (CreateGameDto createGameDto) =>
        {
            var game = new GameDto(
                games.Count + 1,
                createGameDto.Name,
                createGameDto.Genre,
                createGameDto.Price,
                createGameDto.ReleaseDate
            );
            games.Add(game);
            return Results.CreatedAtRoute(GetEndpointName, new { id = game.Id }, game);
        });

        //PUT/games/:id

        group.MapPut("/{id}", (int id, UpdateGameDto updateGameDto) =>
        {
            var index = games.FindIndex(game => game.Id == id);

            games[index] = new GameDto(
                id,
                updateGameDto.Name,
                updateGameDto.Genre,
                updateGameDto.Price,
                updateGameDto.ReleaseDate
            );
            return Results.NoContent();
        });

        //DELETE/games/:id
        group.MapDelete("/{id}", (int id) =>
        {
            var index = games.FindIndex(game => game.Id == id);
            games.RemoveAt(index);
            return Results.NoContent();
        });

    }
}
