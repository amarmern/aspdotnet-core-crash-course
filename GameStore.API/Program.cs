using GameStore.API.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDto> games = [
  new (1,"Street Fieghter 1","Fighting 1",19.9M, new DateOnly(1992, 7, 15)),
  new (2,"Street Fieghter 2","Fighting 2",20.9M, new DateOnly(2000, 8, 11)),
  new (3,"Street Fieghter 3","Fighting 3",66.9M, new DateOnly(2026, 9, 14)),
];

const string GetEndpointName = "GetGame";
//GET/games
app.MapGet("/games", () => games);

//GET/games/:id
app.MapGet("/games/{id}", (int id) => games.Find(game => game.Id == id))
.WithName(GetEndpointName);

//POST/games
app.MapPost("/games", (CreateGameDto createGameDto) =>
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

app.MapPut("/games/{id}", (int id, UpdateGameDto updateGameDto) =>
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
app.MapDelete("/games/{id}", (int id) =>
{
    var index = games.FindIndex(game => game.Id == id);
    games.RemoveAt(index);
    return Results.NoContent();
});

app.Run();
