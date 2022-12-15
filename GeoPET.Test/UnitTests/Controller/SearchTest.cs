using FluentAssertions;
using GeoPet.Controllers;
using GeoPet.Interfaces;
using GeoPet.Models.Response;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoPET.Test.UnitTests.ServicesTests
{
    public class SearchTest
    {
        [Fact]
        public async Task GetAddress_ShouldBeFilledWithSuccess()
        {
            var searchServiceMock = new Mock<ISearchService>();
            searchServiceMock.Setup(x => x.GetAddress(It.IsAny<double>(), It.IsAny<double>())).ReturnsAsync(SearchMock.GetAddress());
            var controller = new SearchController(searchServiceMock.Object);
            var result = await controller.GetAddress(0.1, 0.1);
            result.Should().NotBeNull();
        }

        [ExcludeFromCodeCoverage]
        public static class SearchMock
        {
            public static AddressResponse GetAddress()
            {
                return new()
                {
                    address = new(),
                    addresstype = "type",
                    boundingbox = new(),
                    category = "category",
                    display_name = "display_name",
                    importance = 0.1,
                    lat = "000",
                    lon = "000",
                    licence = "license",
                    name = "name",
                    osm_id = 1,
                    osm_type = "osm",
                    place_id = 1,
                    place_rank = 1,
                    type = "type"
                };
            }
        }
    }
}
