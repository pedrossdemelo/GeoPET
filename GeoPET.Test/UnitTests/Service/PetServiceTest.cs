using FluentAssertions;
using GeoPet.Data;
using GeoPet.Entities;
using GeoPet.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.OpenApi.Any;
using Moq;
using System;
using System.CodeDom;
using System.Drawing;

namespace GeoPET.Test.UnitTests.Service
{
    public class PetServiceTest
    {
        [Fact]
        public async Task GetAllPets_ShouldBeCompletedWithSuccess()
        {
            var contextMock = new Mock<GeoPetContext>();
            //contextMock.SetupGet(it => it.Pets.ToList()).Returns(ContextMock.GetAllPets());
            contextMock.As<IQueryable<Pet>>().Setup(it => it.ToList()).Returns(new List<Pet>());

            var service = new PetService(contextMock.Object);
            var result = await service.GetAllPets();
            result.Should().NotBeNull();
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
                var mock = new Mock<DbSet<Pet>>(pet);
                return mock.Object;
            }

            public static Task<List<Pet>> GetAllPets()
            {
                var pets =
                    new List<Pet>()
                    {
                        new Pet()
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
                        }
                    };
                return pets;
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
