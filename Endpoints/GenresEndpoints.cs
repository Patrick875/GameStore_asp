using System;
using GameStore_API.Data;
using GameStore_API.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore_API.Endpoints;

public static class GenresEndpoints
{
public static RouteGroupBuilder MapGenresEndpoints (this WebApplication app){
    var group=app.MapGroup("genres");
    group.MapGet("/",async(GameStoreContext dbContext)=>{
      return  await dbContext.Genres
        .Select(genre=>genre.ToDto())
        .AsNoTracking()
        .ToListAsync();
    });
    return group;
}
}
