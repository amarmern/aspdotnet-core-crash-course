using GameStore.API.Data;
using GameStore.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

var connectionString = "Data Source=GameStore.db";
builder.Services.AddSqlite<GameStoreContext>(connectionString);


var app = builder.Build();

app.MapGamesEndPoints();

app.MigrateDb();

app.Run();
