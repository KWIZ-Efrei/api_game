using kwiz_api_game.Models;
using kwiz_api_game.Models.Entities;
using Microsoft.Extensions.Options;

namespace kwiz_api_game.Services;

using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;

public class DataContext : DbContext
{
    public DbSet<Game> Games { get; init; }
    
    public static DataContext Create(IMongoDatabase database) =>
        new(new DbContextOptionsBuilder<DataContext>()
            .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
            .Options);
    public DataContext(DbContextOptions options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Game>().ToCollection("games");
    }
}