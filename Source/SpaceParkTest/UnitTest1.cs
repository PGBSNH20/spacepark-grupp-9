using SpaceParkConsole;
using SpaceParkModel.Database;
using SpaceParkModel.SwApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var result = swApi.ValidateSwName("Skywalker").Result;
            Assert.False(result);
        }

        [Fact]
        public void SearchFor_WhiteSpaceLukeSkywalker_Returns_ValidName()
        {
            var result = swApi.ValidateSwName(" Luke Skywalker").Result;
            Assert.True(result);
        }

        [Fact]
        public void PadText_Short_Even_Long()
        {
            // Shortening the text, adding ellipsis
            var result = Menu.PadText("Translate", 5);
            Assert.Equal("Tr...", result);

            // Adding some padding
            var result2 = Menu.PadText("Dog", 5);
            Assert.Equal("Dog  ", result2);

            // Checking for same size
            var result3 = Menu.PadText("Translate", 9);
            Assert.Equal("Translate", result3);
        }

        [Fact]
        public void SearchFor_ParkingSpotSizes_Expecting_1000_10000_150000()
        {
            int[] expectedSizes = new[] { 1000, 10000, 150000 };
            var result = DBQuery.GetParkingSizes().Result;
            Assert.True(result.TrueForAll(r => expectedSizes.Contains(r.Size)));
            Assert.True(result.Count == 3);
        }

        [Fact]
        public void Get_ParkingSpotID_Expect_Zero()
        {
            var result = DBQuery.GetAvailableParkingSpotID(55).Result;
            Assert.Equal(0, result);
        }

        [Fact]
        public async Task Scenario_User_LogsIn_Parks_Unparks()
        {
            // park => check occupancy for the new entry, check if person is in the person database, check if starship, etc. are correct
            SwStarship testSwStarship = new()
            {
                Name = "Test Spaceship",
                Length = 150.0
            };
            string personName = "Test Person";

            int parkingSpotID = await DBQuery.GetAvailableParkingSpotID(testSwStarship);

            // park
            await DBQuery.FillOccupancy(personName, testSwStarship.Name, parkingSpotID);

            Occupancy occupancyAfterParking = await DBQuery.GetOpenOccupancyByName(personName);
            Assert.Null(occupancyAfterParking.DepartureTime);

            int personID = await DBQuery.GetPersonID(personName);
            Assert.Equal(occupancyAfterParking.PersonID, personID);

            int spaceshipID = await DBQuery.GetSpaceshipID(testSwStarship.Name);
            Assert.Equal(spaceshipID, occupancyAfterParking.SpaceshipID);

            // unpark
            //await DBQuery.AddPaymentAndDeparture(occupancyAfterParking);

            //Occupancy occupancyAfterLeaving = await DBQuery.GetOpenOccupancyByName(personName);
            //Assert.NotNull(occupancyAfterLeaving.DepartureTime);

            //List<OccupancyHistory> occupancyHistory = await DBQuery.GetPersonalHistory(personName);
            //Assert.Equal(occupancyAfterParking.ArrivalTime, occupancyHistory[^1].ArrivalTime);
            //Assert.Equal(occupancyAfterParking.DepartureTime, occupancyHistory[^1].DepartureTime);
            //Assert.Equal(personName, occupancyHistory[^1].PersonName);
            //Assert.Equal(testSwStarship.Name, occupancyHistory[^1].SpaceshipName);

            //decimal paymentAmount = await DBQuery.CalculatePaymentAmount(occupancyAfterLeaving);
            //Assert.Equal(paymentAmount, occupancyHistory[^1].AmountPaid);
        }
    }
}
