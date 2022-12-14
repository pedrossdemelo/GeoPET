using GeoPet.Entities;
using GeoPet.Interfaces;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Moq;
using Moq.Language;

namespace GeoPET.Test.UnitTests.ServicesTests
{
    public class PetServiceTest
    {
        [Fact()]
        public async Task GetPets_ShouldBeFilledWithSuccess()
        {
            // arrange
            var petServiceMock = new Mock<IPetService>();

            List<Pet> petResponse = new List<Pet>()
            {
                new Pet()
                {
                    Age = 10,
                    Breed = new() {BreedId = 1, Name = "Raça"},
                    BreedId = 1,
                    LocalizationHash = "string",
                    Name = "Nome",
                    PetId = 1,
                    Weight = 10,
                    PetCarerId = 1,
                    PetCarer = new() 
                    {
                        PetCarerId = 1, 
                        Name = "Nome", 
                        Email = "string@string.com", 
                        PasswordHash = "hash to be", 
                        ZipCode = "12345678", 
                        Pets = new List<Pet>() 
                    }
                }
            };

            petServiceMock.Setup(it => it.GetAllPets());

            // act

            // assert
        }
    }
}
