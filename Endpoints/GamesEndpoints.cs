using System;
using GameStore_API.Data;
using GameStore_API.Dtos;
using GameStore_API.Mapping;
using GameStore_API.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore_API.Endpoints;

public static  class GamesEndpoints
{
  private static   readonly List <GameSummaryDto> games=[
   new (
    1,
    "Street Fighter II", 
    "Fighting", 
    19.99M, 
    new DateOnly(2010,9,30)),
   new (
    2,
    "Final Fantasy XIV", 
    "RolePlaying", 
    59.99M, 
    new DateOnly(2010,9,12)),
   new (
    3,
    "FIFA 23", 
    "Sports", 
    69.99M, 
    new DateOnly(2010,9,27)),
    
];
private static readonly  string GetGameEndpointName="GetGame";


public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app){

        var group= app.MapGroup("games").WithParameterValidation();
        //GET /games
        group.MapGet("/", async (GameStoreContext dbContext)=>{
         return  await  dbContext.Games
            .Include(game=>game.Genre)
            .Select(game=>game.ToGameSummaryDto())
            .AsNoTracking().ToListAsync();
        });

        //GET /games/:id
        group.MapGet("/{id}", async (int id,GameStoreContext dbContext)=>{
            Game? game= await dbContext.Games.FindAsync(id);
            Console.WriteLine(game);
            return game is null? 
            Results.NotFound():Results.Ok(game.ToGameDetailsDto());

        }).WithName(GetGameEndpointName);

        //POST /games
        group.MapPost("/",async (CreateGameDto newGame,GameStoreContext dbContext)=>{

            Game game= newGame.ToEntity();
            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();
    
            return Results.CreatedAtRoute(
            GetGameEndpointName,
            new {id=game.id},game.ToGameDetailsDto()
            );
        });

        //PUT /games/:id
        group.MapPut("/{id}",async (int id,UpdateGameDto updatedGame,GameStoreContext dbContext)=>{
            var existingGame= await dbContext.Games.FindAsync(id);
            if (existingGame is null)
            {
                return Results.NotFound();
            }
            dbContext.Entry(existingGame)
            .CurrentValues
            .SetValues(updatedGame.ToEntity(id));

           await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        //Delete /games/:id
        group.MapDelete("/{id}",async (int id,GameStoreContext dbContext)=>{
            await dbContext.Games
            .Where(game=>game.id==id)
            .ExecuteDeleteAsync();
            return Results.NoContent();
        });

    return group;
    
}

}
