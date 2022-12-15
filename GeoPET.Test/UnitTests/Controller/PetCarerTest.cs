using FluentAssertions;
using GeoPet.Controllers;
using GeoPet.Entities;
using GeoPet.Interfaces;
using GeoPet.Models.Authorization;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoPET.Test.UnitTests.ServicesTests
{
    public class PetCarerTest
    {
        [Fact()]
        public async Task GetAllPetCarers_ShouldBeFilledWithSuccess()
        {
            var petCarerServiceMock = new Mock<IPetCarerService>();
            petCarerServiceMock.Setup(it => it.GetAllPetCarers()).ReturnsAsync(PetCarerMock.GetAll());
            var controller = new PetCarerController(petCarerServiceMock.Object);
            var result = await controller.GetAllPetCarers();
            result.Should().NotBeNull();
        }

        [Fact()]
        public async Task GetPetCarerById_ShouldBeFilledWithSuccess()
        {
            var petCarerServiceMock = new Mock<IPetCarerService>();
            int id = 1;
            petCarerServiceMock.Setup(it => it.GetPetCarerById(id)).ReturnsAsync(PetCarerMock.GetOne());
            var controller = new PetCarerController(petCarerServiceMock.Object);
            var result = await controller.GetPetCarerById(id);
            result.Should().NotBeNull();
        }

        [Fact()]
        public async Task AddPetCarer_ShouldBeCompletedWithSuccess()
        {
            var petCarerServiceMock = new Mock<IPetCarerService>();
            petCarerServiceMock.Setup(it => it.AddPetCarer(It.IsAny<RegisterRequest>())).ReturnsAsync(PetCarerMock.GetOne());
            var controller = new PetCarerController(petCarerServiceMock.Object);
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
        public async Task UpdatePetCarer_ShouldBeCompletedWithSuccess() { }
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
    }
}
