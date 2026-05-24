using GameStore.API.Data;
using GameStore.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

builder.AddGameStoreData();
var app = builder.Build();

app.MapGamesEndPoints();

app.MigrateDb();

app.Run();
