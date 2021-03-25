using SpaceParkModel.SwApi;
using System;
using Xunit;

namespace SpaceParkTest
{
    public class UnitTest1
    {
        private readonly SwApi swApi = new();

        [Fact]
        public void SearchFor_LukeSkywalker_Returns_LukeSkywalker()
        {
            var result = swApi.SearchResource<SwPeople>(SwApiResource.people, "Luke Skywalker").Result;
            Assert.Equal("Luke Skywalker", result[0].Name);
        }

        [Fact]
        public void SearchFor_LukiSkywalker_Returns_Empty()
        {
            var result = swApi.SearchResource<SwPeople>(SwApiResource.people, "Luki Skywalker").Result;
            Assert.Empty(result);
        }

        [Fact]
        public void GetAllResourcesFor_People_Expect_MoreThan10Entries()
        {
            var result = swApi.GetAllResources<SwPeople>(SwApiResource.people).Result;
            Assert.True(result.Count > 10);
        }

        [Fact]
        public void SearchFor_Skywalker_Returns_InvalidName()
        {
            var result = swApi.ValidateSwName("Skywalker");
            Assert.False(result);
        }

        [Fact]
        public void SearchFor_WhiteSpaceLukeSkywalker_Returns_ValidName()
        {
            var result = swApi.ValidateSwName(" Luke Skywalker");
            Assert.True(result);
        }
    }
}
