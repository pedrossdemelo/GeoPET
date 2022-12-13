using Microsoft.EntityFrameworkCore;
using GeoPet.Models;

namespace GeoPet.Data;

public class GeoPetContext : DbContext
{
    public GeoPetContext(DbContextOptions<GeoPetContext> options) : base(options) { }

    public DbSet<Pet> Pets { get; set; }

    public DbSet<PetCarer> PetCarers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
