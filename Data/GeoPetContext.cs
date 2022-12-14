using Microsoft.EntityFrameworkCore;
using GeoPet.Models;

namespace GeoPet.Data;

public class GeoPetContext : DbContext
{
    public GeoPetContext(DbContextOptions<GeoPetContext> options) : base(options) { }

    public DbSet<Pet> Pets { get; set; } = default!;

    public DbSet<PetCarer> PetCarers { get; set; } = default!;

    public DbSet<Breed> Breeds { get; set; } = default!;

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region BreedSeed
        var breeds = File.ReadAllLines("Data/breeds.csv")
            .Skip(1)
            .Select(line => line.Split(','))
            .Select(value => new Breed
            {
                BreedId = int.Parse(value[0]),
                Name = value[1]
            });
        if (breeds is null) throw new ArgumentNullException(nameof(breeds));
        modelBuilder.Entity<Breed>().HasData(breeds);
        #endregion

        #region PetCarerSeed
        modelBuilder.Entity<PetCarer>().HasData(
            new PetCarer
            {
                PetCarerId = 1,
                Name = "John Doe",
                Email = "johndoe@email.com",
                ZipCode = "05426200",
                Password = "123456",
            }
        );
        #endregion

        #region PetSeed
        modelBuilder.Entity<Pet>().HasData(
            new Pet
            {
                PetId = 1,
                Name = "Bailey",
                Age = 3,
                Weight = 5.5,
                BreedId = 26,
                PetCarerId = 1,
            }
        );
        #endregion
    }
}
