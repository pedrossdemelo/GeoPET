using FluentAssertions;
using GeoPet.Controllers;
using GeoPet.Entities;
using GeoPet.Interfaces;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace GeoPET.Test.UnitTests.ServicesTests
{
    public class PetTest
    {
        [Fact()]
        public async Task GetAllPets_ShouldBeFilledWithSuccess()
        {
            // arrange
            var petServiceMock = new Mock<IPetService>();

            petServiceMock.Setup(it => it.GetAllPets()).ReturnsAsync(PetMock.GetAll());

            var controller = new PetController(petServiceMock.Object);

            // act
            var result = controller.GetAllPets();

            // assert
            var testResult = result.GetAwaiter().GetResult();
            testResult.Should().NotBeNull();
        }

        [Fact()]
        public async Task GetPetById_ShouldBeFilledWithSuccess()
        {
            // arrange
            int id = 1;
            var petServiceMock = new Mock<IPetService>();

            petServiceMock.Setup(it => it.GetPetById(id)).ReturnsAsync(PetMock.GetById());

            var controller = new PetController(petServiceMock.Object);

            // act
            var result = controller.GetPetById(id);

            // assert
            var testResult = result.GetAwaiter().GetResult();
            testResult.Should().NotBeNull();
        }

        [Fact()]
        public async Task AddPet_ShouldBeCompletedWithSuccess()
        {
            var petServiceMock = new Mock<IPetService>();
            petServiceMock.Setup(it => it.AddPet(It.IsAny<Pet>())).ReturnsAsync(PetMock.GetById());
            var controller = new PetController(petServiceMock.Object);
            var result = controller.AddPet(PetMock.GetById());

            var testResult = result.GetAwaiter().GetResult();
            testResult.Should().NotBeNull();
        }

        [Fact()]
        public async Task UpdatePet_ShouldBeCompletedWithSuccess()
        {
            var petServiceMock = new Mock<IPetService>();
            int id = 1;
            petServiceMock.Setup(it => it.UpdatePet(id, It.IsAny<Pet>())).ReturnsAsync(PetMock.GetById());
            var controller = new PetController(petServiceMock.Object);
            var result = controller.AddPet(PetMock.GetById());

            var testResult = result.GetAwaiter().GetResult();
            testResult.Should().NotBeNull();
        }
    }

    [ExcludeFromCodeCoverage]
    class PetMock
    {
        public static List<Pet> GetAll()
        {
            return new List<Pet>()
            {
                new Pet()
                {
                    Age = 10,
                    Breed = new() { BreedId = 1, Name = "Raça" },
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
        }

        public static Pet GetById()
        {
            return new Pet()
            {
                Age = 10,
                Breed = new() { BreedId = 1, Name = "Raça" },
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
            };
        }
    }
}
