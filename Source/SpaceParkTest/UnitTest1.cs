using SpaceParkModel.Data;
using SpaceParkModel.SwApi;
using System;
using Xunit;

namespace SpaceParkTest
{
    public class UnitTest1
    {
        [Fact]
        public void SearchFor_LukeSkywalker_Returns_LukeSkywalker()
        {
            SwApi swApi = new SwApi();
            var result = swApi.SearchResource<SwPeople>(SwApiResource.people, "Luke Skywalker").Result;
            Assert.Equal("Luke Skywalker", result[0].Name);
        }

        [Fact]
        public void SearchFor_LukiSkywalker_Returns_Empty()
        {
            SwApi swApi = new SwApi();
            var result = swApi.SearchResource<SwPeople>(SwApiResource.people, "Luki Skywalker").Result;
            Assert.Empty(result);
        }

        [Fact]
        public void GetAllResourcesFor_People_Expect_MoreThan10Entries()
        {
            SwApi swApi = new SwApi();
            var result = swApi.GetAllResources<SwPeople>(SwApiResource.people).Result;
            Assert.True(result.Count > 10);
        }
    }
}
