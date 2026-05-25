using System.Security.Cryptography.X509Certificates;
using GameStore.API.Data;
using GameStore.API.Dtos;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Endpoints;

public static class GenreEndpoints
{
    public static void MapGenreEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/genres");

        //GET/genres
        group.MapGet("/", async (GameStoreContext dbContext) =>
        {
            var genres = await dbContext.Genres
            .Select(genre => new GenreDto(
                genre.Id,
                genre.Name
            ))
            .AsNoTracking()
            .ToListAsync();
            return Results.Ok(genres);
        });
    }
}
