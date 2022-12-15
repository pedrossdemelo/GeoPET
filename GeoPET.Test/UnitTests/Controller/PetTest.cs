using FluentAssertions;
using GeoPet.Controllers;
using GeoPet.Entities;
using GeoPet.Interfaces;
using GeoPet.Models.Request;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace GeoPET.Test.UnitTests.ServicesTests
{
    [ExcludeFromCodeCoverage]
    public class PetTest
    {
        [Fact()]
        public async Task GetAllPets_ShouldBeFilledWithSuccess()
        {
            // arrange
            var petServiceMock = new Mock<IPetService>();
            var httpContextAccessorMock = new Mock<HttpContextAccessor>();

            petServiceMock.Setup(it => it.GetAllPets()).ReturnsAsync(PetMock.GetAll());

            var controller = new PetController(petServiceMock.Object, httpContextAccessorMock.Object);

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
            var httpContextAccessorMock = new Mock<HttpContextAccessor>();

            petServiceMock.Setup(it => it.GetPetById(id)).Returns(Task.FromResult(PetMock.GetById()));

            var controller = new PetController(petServiceMock.Object, httpContextAccessorMock.Object);

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
            var httpContextAccessorMock = new Mock<HttpContextAccessor>();
            petServiceMock.Setup(it => it.AddPet(It.IsAny<PetRegisterRequest>())).ReturnsAsync(PetMock.GetById());
            var controller = new PetController(petServiceMock.Object, httpContextAccessorMock.Object);
            var result = controller.AddPet(PetMock.RegisterRequest());

            var testResult = result.GetAwaiter().GetResult();
            testResult.Should().NotBeNull();
        }

        [Fact()]
        public async Task UpdatePet_ShouldBeCompletedWithSuccess()
        {
            var petServiceMock = new Mock<IPetService>();
            var httpContextAccessorMock = new Mock<HttpContextAccessor>();
            int id = 1;

            petServiceMock.Setup(it => it.UpdatePet(id, It.IsAny<PetRegisterRequest>())).ReturnsAsync(PetMock.GetById());
            var controller = new PetController(petServiceMock.Object, httpContextAccessorMock.Object);

            var result = await controller.UpdatePet(id, PetMock.RegisterRequest());

            result.Should().NotBeNull();
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


        public static PetRegisterRequest RegisterRequest()
        {
            return new PetRegisterRequest()
            {
                Age = 10,
                BreedId = 1,
                LocalizationHash = "string",
                Name = "Nome",
                Weight = 10
            };
        }

        public static List<PetRegisterRequest> RegisterListRequest()
        {
            return new List<PetRegisterRequest>()
            {
                new PetRegisterRequest()
                {
                    Age = 10,
                    //Breed = new() { BreedId = 1, Name = "Raça" },
                    BreedId = 1,
                    LocalizationHash = "string",
                    Name = "Nome",
                    //PetId = 1,
                    Weight = 10,
                    //PetCarerId = 1,
                    //PetCarer = new()
                    //{
                    //    PetCarerId = 1,
                    //    Name = "Nome",
                    //    Email = "string@string.com",
                    //    PasswordHash = "hash to be",
                    //    ZipCode = "12345678",
                    //    Pets = new List<Pet>()
                    //}
                }
            };
        }

    }
}
