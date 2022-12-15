using FluentAssertions;
using GeoPet.Controllers;
using GeoPet.Entities;
using GeoPet.Interfaces;
using GeoPet.Models.Authorization;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace GeoPET.Test.UnitTests.ServicesTests
{
    [ExcludeFromCodeCoverage]
    public class PetCarerTest
    {
        [Fact()]
        public async Task GetAllPetCarers_ShouldBeFilledWithSuccess()
        {
            var petCarerServiceMock = new Mock<IPetCarerService>();
            var httpContextAccessorMock = new Mock<HttpContextAccessor>();
            petCarerServiceMock.Setup(it => it.GetAllPetCarers()).ReturnsAsync(PetCarerMock.GetAll());
            var controller = new PetCarerController(petCarerServiceMock.Object, httpContextAccessorMock.Object);
            var result = await controller.GetAllPetCarers();
            result.Should().NotBeNull();
        }

        [Fact()]
        public async Task GetPetCarerById_ShouldBeFilledWithSuccess()
        {
            var petCarerServiceMock = new Mock<IPetCarerService>();
            var httpContextAccessorMock = new Mock<HttpContextAccessor>();
            int id = 1;
            petCarerServiceMock.Setup(it => it.GetPetCarerById(id)).ReturnsAsync(PetCarerMock.GetOne());
            var controller = new PetCarerController(petCarerServiceMock.Object, httpContextAccessorMock.Object);
            var result = await controller.GetPetCarerById(id);
            result.Should().NotBeNull();
        }

        [Fact()]
        public async Task AddPetCarer_ShouldBeCompletedWithSuccess()
        {
            var petCarerServiceMock = new Mock<IPetCarerService>();
            var httpContextAccessorMock = new Mock<HttpContextAccessor>();
            petCarerServiceMock.Setup(it => it.AddPetCarer(It.IsAny<RegisterRequest>())).ReturnsAsync(PetCarerMock.GetOne());
            var controller = new PetCarerController(petCarerServiceMock.Object, httpContextAccessorMock.Object);
            var request = new RegisterRequest()
            {
                Email = PetCarerMock.GetOne().Email,
                Name = PetCarerMock.GetOne().Name,
                Password = "123123",
                ZipCode = PetCarerMock.GetOne().ZipCode,
            };
            var result = await controller.AddPetCarer(request);
            result.Should().NotBeNull();
        }

        [Fact()]
        public async Task UpdatePetCarer_ShouldBeCompletedWithSuccess()
        {
            var petCarerServiceMock = new Mock<IPetCarerService>();
            var httpContextAccessorMock = new Mock<HttpContextAccessor>();
            int id = 1;
            petCarerServiceMock.Setup(it => it.UpdatePetCarer(id, It.IsAny<UpdateRequest>())).ReturnsAsync(PetCarerMock.GetOne());
            var controller = new PetCarerController(petCarerServiceMock.Object, httpContextAccessorMock.Object);
            var request = new UpdateRequest()
            {
                Email = PetCarerMock.GetOne().Email,
                Name = PetCarerMock.GetOne().Name,
                Password = "123123",
                ZipCode = PetCarerMock.GetOne().ZipCode,
            };
            var result = await controller.UpdatePetCarer(id, request);
            result.Should().NotBeNull();
        }

        [Fact()]
        public async Task DeletePetCarer_ShouldBeCompletedWithSuccess()
        {
            var petCarerServiceMock = new Mock<IPetCarerService>();
            var httpContextAccessorMock = new Mock<HttpContextAccessor>();
            int id = 1;
            petCarerServiceMock.Setup(it => it.DeletePetCarer(id)).Returns(Task.FromResult(false));
            var controller = new PetCarerController(petCarerServiceMock.Object, httpContextAccessorMock.Object);
            var result = await controller.DeletePetCarer(id);
            result.Should().NotBeNull();
        }

        [Fact()]
        public async Task GetPetsByCarerId_ShouldBeCompletedWithSuccess()
        {
            var petCarerServiceMock = new Mock<IPetCarerService>();
            var httpContextAccessorMock = new Mock<HttpContextAccessor>();
            int id = 1;
            petCarerServiceMock.Setup(it => it.GetPetsByCarerId(id)).ReturnsAsync(PetCarerMock.GetAllPets());
            var controller = new PetCarerController(petCarerServiceMock.Object, httpContextAccessorMock.Object);
            var result = await controller.GetPetsByCarerId(id);
            result.Should().NotBeNull();
        }

        [Fact()]
        public async Task Authenticate_ShouldBeCompletedWithSuccess()
        {
            var petCarerServiceMock = new Mock<IPetCarerService>();
            var httpContextAccessorMock = new Mock<HttpContextAccessor>();
            petCarerServiceMock.Setup(it => it.Authenticate(It.IsAny<AuthenticateRequest>())).ReturnsAsync(PetCarerMock.Auth());
            var controller = new PetCarerController(petCarerServiceMock.Object, httpContextAccessorMock.Object);
            var request = new AuthenticateRequest() { Email = "email@email.com", Password = "senhamuitoforte" };
            var result = await controller.Authenticate(request);
            result.Should().NotBeNull();
        }
    }

    [ExcludeFromCodeCoverage]
    class PetCarerMock
    {
        public static List<PetCarer> GetAll()
        {
            return new List<PetCarer>()
            {
                new PetCarer()
                {
                    Email = "email@email.com",
                    Name = "Nome",
                    PasswordHash = "hash-to-be",
                    PetCarerId = 1,
                    ZipCode = "12345678",
                    Pets = new() {}
                },
            };
        }

        public static PetCarer GetOne()
        {
            return new()
            {
                Email = "email@email.com",
                Name = "Nome",
                PasswordHash = "hash-to-be",
                PetCarerId = 1,
                ZipCode = "12345678",
                Pets = new() { }
            };
        }

        public static List<Pet> GetAllPets()
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

        public static AuthenticateResponse Auth()
        {
            var response = new AuthenticateResponse() { Id = 1, Token = "token"};
            return response;
        }
    }
}
