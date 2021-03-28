using Microsoft.EntityFrameworkCore;
using SpaceParkModel.SwApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceParkModel.Database
{
    public static class DBQuery
    {
        public static async Task<List<ParkingSize>> GetParkingSizes()
        {
            await using var context = new SpaceParkContext();
            List<ParkingSize> parkingSizes = await context.ParkingSizes.ToListAsync();

            return parkingSizes;
        }

        public static async Task<int> GetAvailableParkingSpotID(int sizeID)
        {
            await using var context = new SpaceParkContext();
            // I am filling an array with parkingSpot id's where the sizeID is equal to the parkingSizeID in the database table
            int[] parkingSpots = await context.ParkingSpots.Where(p => p.ParkingSizeID == sizeID).Select(p => p.Spot).ToArrayAsync();

            // I am looking for a departure which doesn't have a value and a parking spot that isn't occupied, and we take the first one.
            // find: ID that's un-used by checking all entries without departure
            int[] unavailableOccupancies = await context.Occupancies.Where(o => !o.DepartureTime.HasValue).Select(o => o.ParkingSpotID).ToArrayAsync();

            int firstAvailableSpot = parkingSpots.FirstOrDefault(p => !unavailableOccupancies.Contains(p));

            return firstAvailableSpot;
        }

        public static async Task<int> GetAvailableParkingSpotID(SwStarship ship)
        {
            double starshipLength = ship.Length;

            await using SpaceParkContext context = new SpaceParkContext();
            ParkingSize appropriateParkingSize = await context.ParkingSizes.Where(p => p.Size > starshipLength).OrderBy(p => p.Size).FirstAsync();
            int availableParkingSpotID = await GetAvailableParkingSpotID(appropriateParkingSize.ID);

            return availableParkingSpotID;
        }

        public static async Task<int> GetNextSmallestAvailableParkingSpotID(SwStarship ship)
        {
            double starshipLength = ship.Length;

            await using SpaceParkContext context = new SpaceParkContext();
            ParkingSize[] appropriateParkingSizes = await context.ParkingSizes.Where(p => p.Size > starshipLength).OrderBy(p => p.Size).ToArrayAsync();

            foreach (var parkingSize in appropriateParkingSizes)
            {
                int availableParkingSpotID = await GetAvailableParkingSpotID(parkingSize.ID);
                if (availableParkingSpotID != 0)
                {
                    return availableParkingSpotID;
                }
            }

            return 0;
        }

        public static async Task FillOccupancy(string personName, string spaceshipName, int parkingSpotID)
        {
            if (parkingSpotID == 0)
            {
                throw new Exception("You can't park in spot 0, because it doesn't exist.");
            }

            var occupancy = new Occupancy
            {
                // If the person is in the database, then it will give me the personID, otherwise it will create a person and return their ID
                PersonID = await AddPerson(personName),
                SpaceshipID = await AddSpaceship(spaceshipName),
                ParkingSpotID = parkingSpotID,
                ArrivalTime = DateTime.Now
            };

            await using var context = new SpaceParkContext();
            context.Add(occupancy);
            context.SaveChanges();
        }

        public static async Task<Occupancy> GetOpenOccupancyByName(string name)
        {
            await using var context = new SpaceParkContext();
            int personID = await GetPersonID(name);

            return await context.Occupancies.FirstAsync(o => !o.DepartureTime.HasValue && o.PersonID == personID);
        }

        // Used to show history, not for general caching
        private static async Task<int> AddSpaceship(string name)
        {
            await using var context = new SpaceParkContext();

            // If spaceship doesn't exist in the database then add it
            if (!await context.Spaceships.AnyAsync(p => p.Name == name))
            {
                context.Add(new Spaceship { Name = name });
                context.SaveChanges();
            }
            // get spaceship ID
            return await GetSpaceshipID(name);
        }

        public static async Task<int> GetSpaceshipID(string name)
        {
            await using var context = new SpaceParkContext();
            var spaceship = await context.Spaceships.FirstOrDefaultAsync(s => s.Name == name);
            int spaceshipID = spaceship != null ? spaceship.ID : 0;
            return spaceshipID;
        }

        // Used to show history, not for general caching
        public static async Task<int> AddPerson(string name)
        {
            await using var context = new SpaceParkContext();

            // If person doesn't exist in the database then add them
            if (!await context.Persons.AnyAsync(p => p.Name == name))
            {
                context.Add(new Person { Name = name });
                context.SaveChanges();
            }
            // get person ID
            return await GetPersonID(name);
        }

        public static async Task<int> GetPersonID(string name)
        {
            await using var context = new SpaceParkContext();
            var person = await context.Persons.FirstOrDefaultAsync(p => p.Name == name);
            int personID = person != null ? person.ID : 0;
            return personID;
        }

        public static async Task<int> GetParkingSizeIDBySpot(int parkingSpotID)
        {
            await using var context = new SpaceParkContext();
            ParkingSpots parkingSpot = await context.ParkingSpots.FindAsync(parkingSpotID);
            return parkingSpot.ParkingSizeID;
        }

        public static async Task<decimal> GetParkingSpotPriceByID(int parkingSizeID)
        {
            await using var context = new SpaceParkContext();
            ParkingSize parkingSize = await context.ParkingSizes.FindAsync(parkingSizeID);
            return parkingSize.Price;
        }

        public static async Task AddPaymentAndDeparture(Occupancy occupancy)
        {
            occupancy.DepartureTime = DateTime.Now;
            Payment payment = new Payment()
            {
                OccupancyID = occupancy.ID,
                Amount = await CalculatePaymentAmount(occupancy)
            };

            await using var context = new SpaceParkContext();
            context.Payments.Add(payment);
            context.SaveChanges();
            await UpdateOccupancy(occupancy);
        }

        public static async Task<decimal> CalculatePaymentAmount(Occupancy occupancy)
        {
            int parkingSizeID = await GetParkingSizeIDBySpot(occupancy.ParkingSpotID);
            decimal parkingSpotPrice = await GetParkingSpotPriceByID(parkingSizeID);
            decimal billingHours = CalculateBillingHours(occupancy);
            decimal totalPrice = billingHours * parkingSpotPrice;

            return totalPrice;
        }

        public static decimal CalculateBillingHours(Occupancy occupancy)
        {
            TimeSpan parkingDuration = occupancy.DepartureTime.GetValueOrDefault() - occupancy.ArrivalTime;
            decimal billingHours = Convert.ToDecimal(Math.Ceiling(parkingDuration.TotalHours));

            return billingHours;
        }

        public static async Task<decimal> GetPaymentForOccupancy(Occupancy occupancy)
        {
            await using var context = new SpaceParkContext();
            Payment payment = await context.Payments.FirstAsync(p => p.OccupancyID == occupancy.ID);

            return payment.Amount;
        }

        public static async Task UpdateOccupancy(Occupancy occupancy)
        {
            await using var context = new SpaceParkContext();
            context.Update(occupancy);
            context.SaveChanges();
        }

        public static async Task<bool> IsCheckedIn(string name)
        {
            await using var context = new SpaceParkContext();
            int personID = await GetPersonID(name);
            return await context.Occupancies.AnyAsync(o => !o.DepartureTime.HasValue && o.PersonID == personID);
        }

        public static async Task<List<OccupancyHistory>> GetPersonalHistory(string name)
        {
            await using var context = new SpaceParkContext();
            List<OccupancyHistory> history = await context.Persons
                .Join(
                    context.Occupancies,
                    person => person.ID,
                    occupancy => occupancy.PersonID,
                    (person, occupancy) => new
                    {
                        OccupancyID = occupancy.ID,
                        PersonName = person.Name,
                        occupancy.SpaceshipID,
                        occupancy.ArrivalTime,
                        occupancy.DepartureTime
                    }
                )
                .Join(
                    context.Spaceships,
                    occupancy => occupancy.SpaceshipID,
                    spaceship => spaceship.ID,
                    (occupancy, spaceship) => new
                    {
                        occupancy.OccupancyID,
                        occupancy.PersonName,
                        SpaceshipName = spaceship.Name,
                        occupancy.ArrivalTime,
                        occupancy.DepartureTime
                    }
                )
                .Join(
                    context.Payments,
                    occupancy => occupancy.OccupancyID,
                    payment => payment.OccupancyID,
                    (occupancy, payment) => new
                    {
                        occupancy.OccupancyID,
                        occupancy.PersonName,
                        occupancy.SpaceshipName,
                        occupancy.ArrivalTime,
                        occupancy.DepartureTime,
                        AmountPaid = payment.Amount
                    }
                )
                .Where(data => data.PersonName == name &&
                               data.DepartureTime.HasValue)
                .Select(data => new OccupancyHistory
                {
                    PersonName = data.PersonName,
                    SpaceshipName = data.SpaceshipName,
                    ArrivalTime = data.ArrivalTime,
                    DepartureTime = data.DepartureTime.GetValueOrDefault(),
                    AmountPaid = data.AmountPaid
                })
                .ToListAsync();

            return history;
        }
    }
}
