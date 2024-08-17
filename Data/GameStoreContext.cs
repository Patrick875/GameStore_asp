using System;
using GameStore_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

namespace GameStore_API.Data;

public class GameStoreContext (DbContextOptions <GameStoreContext> options):DbContext(options)
{
//A DbSet can be used to query and save instances of Game. LINQ queries against a DbSet will be translated into queries against the database.
public DbSet<Game> Games => Set <Game>();
public DbSet<Genre> Genres=>Set <Genre>();

protected override void OnModelCreating (ModelBuilder modelBuilder) {
    modelBuilder.Entity<Genre>()
       .HasData(
        new {id=1,Name="Fighting"},
        new {id=2,Name="RolePlaying"},
        new {id=3,Name="Sports"},
        new {id=4,Name="Racing"},
        new {id=5,Name="Kids ans Family"}
        );

}

}
