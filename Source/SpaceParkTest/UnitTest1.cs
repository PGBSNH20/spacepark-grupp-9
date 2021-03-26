using SpaceParkConsole;
using SpaceParkModel.Database;
using SpaceParkModel.SwApi;
using System;
using System.Linq;
using Xunit;

namespace SpaceParkTest
{
    public class UnitTest1
    {
        private readonly SwApi swApi = new();

        //[Fact]
        //public void SearchFor_LukeSkywalker_Returns_LukeSkywalker()
        //{
        //    var result = swApi.SearchResource<SwPeople>(SwApiResource.people, "Luke Skywalker").Result;
        //    Assert.Equal("Luke Skywalker", result[0].Name);
        //}

        //[Fact]
        //public void SearchFor_LukiSkywalker_Returns_Empty()
        //{
        //    var result = swApi.SearchResource<SwPeople>(SwApiResource.people, "Luki Skywalker").Result;
        //    Assert.Empty(result);
        //}

        //[Fact]
        //public void GetAllResourcesFor_People_Expect_MoreThan10Entries()
        //{
        //    var result = swApi.GetAllResources<SwPeople>(SwApiResource.people).Result;
        //    Assert.True(result.Count > 10);
        //}

        //[Fact]
        //public void SearchFor_Skywalker_Returns_InvalidName()
        //{
        //    var result = swApi.ValidateSwName("Skywalker");
        //    Assert.False(result);
        //}

        //[Fact]
        //public void SearchFor_WhiteSpaceLukeSkywalker_Returns_ValidName()
        //{
        //    var result = swApi.ValidateSwName(" Luke Skywalker");
        //    Assert.True(result);
        //}

        //[Fact]
        //public void ResizeText_ToResizeText()
        //{
        //    // Shortening the text, adding ellipsis
        //    var result = Menu.ResizeTextToLength("Translate", 5);
        //    Assert.Equal("Tr...", result);

        //    // Adding some padding
        //    var result2 = Menu.ResizeTextToLength("Dog", 5);
        //    Assert.Equal("Dog  ", result2);

        //    // Checking for same size
        //    var result3 = Menu.ResizeTextToLength("Translate", 9);
        //    Assert.Equal("Translate", result3);
        //}

        //[Fact]
        //public void SearchFor_ParkingSpotSizes_Expecting_1000_10000_150000()
        //{
        //    int[] expectedSizes = new[] { 1000, 10000, 150000 };
        //    var result = DBQuery.GetParkingSizes();
        //    Assert.True(result.TrueForAll(r => expectedSizes.Contains(r.Size)));
        //    Assert.True(result.Count == 3);
        //}
    }
}
