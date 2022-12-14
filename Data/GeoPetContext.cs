using Microsoft.EntityFrameworkCore;
using GeoPet.Entities;

namespace GeoPet.Data;

public class GeoPetContext : DbContext
{
    protected readonly IConfiguration Configuration;
    public GeoPetContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public DbSet<Pet> Pets { get; set; } = default!;

    public DbSet<PetCarer> PetCarers { get; set; } = default!;

    public DbSet<Breed> Breeds { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = Configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);
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
                PasswordHash = "VlKqPxGgnUZQSClj9S6ZA2ubZdyDuxsqqVgFBAvZ57aMLABXA45YJH5ewrntl4klJ8vm7lK+d3yQnFOVegdzoPTgiy5AubUzL6lBqDxT1sZC7pVbXLsgyUBaZ1mr/j8k/Y+XQxZ9M8mfLfQPJeeVrxiLfQ5wgT0aXTqfFv68tBdvD6V0ZMbnwcVrijZq+bdurp+GV1+wqDshGVpBh3FpI8WRVyfxzfBfiUWrZfuvc5t+srZqM8MUZCVsTHNyvCBxBd1k0AWKTUmbnLTIERqTzZF6lq/C/9OOiPPd0c2hM/+W1QSWs8vYOFT5Ogf0D087IEbUSb1pOcgLa877imMv9Q==",
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
