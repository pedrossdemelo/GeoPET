using GeoPet.Data;
using GeoPet.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace GeoPET.Test.UnitTests.Service
{
    public class PetServiceTest
    {
        [Fact]
        public async Task GetAllPets_ShouldBeCompletedWithSuccess()
        {
            var contextMock = new Mock<GeoPetContext>();
            contextMock.Setup(it => it.Pets).Returns(ContextMock.GetOnePet());
        }

        public static class ContextMock
        {
            public static DbSet<Pet> GetOnePet()
            {
                var pet = new Pet()
                {
                    Age = 1,
                    Breed = new() { BreedId = 1, Name = "", Pets = new() { } },
                    BreedId = 1,
                    Name = "",
                    LocalizationHash = "",
                    PetCarer = GetOnePetCarer(),
                    PetId = 1,
                    PetCarerId = 1,
                    Weight = 1,
                };

                ObjectContext
            }

            public static PetCarer GetOnePetCarer()
            {
                return new()
                {
                    Email = "email@eamil.com",
                    Name = "name",
                    PasswordHash = "hasth-to-be",
                    PetCarerId = 1,
                    ZipCode = "12341234",
                    Pets = new List<Pet>() { }
                };
            }
        }
    }
}
