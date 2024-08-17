using Microsoft.EntityFrameworkCore;
namespace GameStore_API.Data;

public static class DataExtensions{
    public static async Task MigrateDbAsync(this WebApplication app){
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        await context.Database.MigrateAsync();
    }
}