using GameStore.Api.Entities;
using GameStore.Api.Repositories;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";


    public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes)
    {
        InMemGamesRepository repository = new();

        var group = routes.MapGroup("/games").WithParameterValidation();


        group.MapGet("/", () => repository.GetAll());

        group.MapGet("/{id}", (int id) =>
        {
            Game? game = repository.Get(id);
            return game is not null ? Results.Ok(game) : Results.NotFound();
        })
        .WithName(GetGameEndpointName);

        group.MapPost("/", (Game game) =>
        {
            repository.Create(game);
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });

        group.MapPut("/{id}", (int id, Game updatedGame) =>
        {
            Game? exisitingGame = repository.Get(id);

            if (exisitingGame is null)
            {
                return Results.NotFound();
            }

            exisitingGame.Name = updatedGame.Name;
            exisitingGame.Genre = updatedGame.Genre;
            exisitingGame.Price = updatedGame.Price;
            exisitingGame.ReleaseDate = updatedGame.ReleaseDate;
            exisitingGame.ImageUri = updatedGame.ImageUri;

            repository.Update(exisitingGame);

            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id) =>
        {
            Game? game = repository.Get(id);

            if (game is not null)
            {
                repository.Delete(id);
            }

            return Results.NoContent();
        });

        return group;
    }
}