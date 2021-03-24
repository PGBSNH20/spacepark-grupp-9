using SpaceParkModel.Data;
using SpaceParkModel.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceParkModel
{
    public static class DBQuery
    {
        public static List<ParkingSize> GetParkingSizes()
        {
            using (var context = new SpaceParkContext())
            {
                List<ParkingSize> parkingSizes = context.ParkingSizes.ToList();

                return parkingSizes;
            }
        }

        public static int GetAvailableParking(int sizeID)
        {
            using (var context = new SpaceParkContext())
            {
                // we are filling an array with parkingSpot id's where the sizeID is equal to the parkingSizeID in the database table
                int[] parkingSpots = context.ParkingSpots.Where(p => p.ParkingSizeID == sizeID).Select(p => p.Spot).ToArray();

                // we are looking for a departure which doesn't have a value and a parking spot that isnt occupied, and we take the first one.
                // find: id that's unused by checking all entries without departure

                int[] unavailableOccupancies = context.Occupancies.Where(o => !o.DepartureTime.HasValue).Select(o => o.ParkingSpotID).ToArray();

                int firstAvailableSpot = parkingSpots.Where(p => !unavailableOccupancies.Contains(p)).FirstOrDefault();

                return firstAvailableSpot;
            }
        }

        public static void FillOccupancy(string personName, string spaceshipName, int parkingSpotID)
        {
            var occupancy = new Occupancy
            {
                // if the person is in the database, then it will give me the personÌD, otherwise it will create an ID
                PersonID = AddPerson(personName),
                SpaceshipID = AddSpaceship(spaceshipName),
                ParkingSpotID = parkingSpotID,
                ArrivalTime = DateTime.Now
            };

            using (var context = new SpaceParkContext())
            {
                context.Add(occupancy);
                context.SaveChanges();
            }
        }

        public static Occupancy GetOpenOccupancyByName(string personName)
        {
            using (var context = new SpaceParkContext())
            {
                int personID = GetPersonID(personName);

                return context.Occupancies.Where(o => !o.DepartureTime.HasValue && o.PersonID == personID).First();
            }
        }

        // Used to show history, not for general caching
        private static int AddSpaceship(string spaceshipName)
        {
            using (var context = new SpaceParkContext())
            {
                int spaceshipID = 0;
                // If spaceship doesn't exist in the database then add them
                if (!context.Spaceships.Any(p => p.Name.Equals(spaceshipName)))
                {
                    var spaceship = new Spaceship { Name = spaceshipName };
                    context.Add(spaceship);
                    context.SaveChanges();
                }
                // get spaceship id
                spaceshipID = context.Spaceships.Where(p => p.Name.Equals(spaceshipName)).First().ID;
                return spaceshipID;
            }
        }

        // Used to show history, not for general caching
        public static int AddPerson(string personName)
        {
            using (var context = new SpaceParkContext())
            {
                int personID = 0;
                // If person doesn't exist in the database then add them
                if (!context.Persons.Any(p => p.Name.Equals(personName)))
                {
                    var person = new Person { Name = personName };
                    context.Add(person);
                    context.SaveChanges();
                }
                // get person id
                //personID = context.Persons.Where(p => p.Name.Equals(personName)).First().ID;
                personID = GetPersonID(personName);
                return personID;
            }
        }

        public static int GetPersonID(string name)
        {
            using (var context = new SpaceParkContext())
            {
                var person = context.Persons.Where(p => p.Name.Equals(name)).FirstOrDefault();
                int personID = person != null ? person.ID : 0;
                return personID;
                //return context.Persons.Where(p => p.Name.Equals(name)).First().ID;
            }
        }

        public static int GetParkingSizeIDBySpot(int parkingSpotID)
        {
            using (var context = new SpaceParkContext())
            {
                return context.ParkingSpots.Find(parkingSpotID).ParkingSizeID;
            }
        }

        public static decimal GetParkingSpotPriceByID(int parkingSizeID)
        {
            using (var context = new SpaceParkContext())
            {
                return context.ParkingSizes.Find(parkingSizeID).Price;
            }
        }

        public static void UpdateOccupancy(Occupancy occupancy)
        {
            using (var context = new SpaceParkContext())
            {
                context.Update(occupancy);
                context.SaveChanges();
            }
        }

        public static bool IsCheckedIn(string name)
        {
            using (var context = new SpaceParkContext())
            {
                return context.Occupancies.Where(o => !o.DepartureTime.HasValue && o.PersonID == GetPersonID(name)).Count() > 0;
            }
        }

        public static List<OccupancyHistory> GetPersonalHistory(string name)
        {
            using (var context = new SpaceParkContext())
            {
                List<OccupancyHistory> history = context.Persons
                    .Join(
                        context.Occupancies,
                        person => person.ID,
                        occupancy => occupancy.PersonID,
                        (person, occupancy) => new
                        {
                            PersonName = person.Name,
                            SpaceshipID = occupancy.SpaceshipID,
                            ArrivalTime = occupancy.ArrivalTime,
                            DepartureTime = occupancy.DepartureTime
                        }
                    )
                    .Join(
                        context.Spaceships,
                        occupancy => occupancy.SpaceshipID,
                        spaceship => spaceship.ID,
                        (occupancy, spaceship) => new
                        {
                            PersonName = occupancy.PersonName,
                            SpaceshipName = spaceship.Name,
                            ArrivalTime = occupancy.ArrivalTime,
                            DepartureTime = occupancy.DepartureTime
                        }
                    )
                    .Where(data => data.PersonName == name &&
                                   data.DepartureTime.HasValue)
                    .Select(data => new OccupancyHistory
                    {
                        PersonName = data.PersonName,
                        SpaceshipName = data.SpaceshipName,
                        ArrivalTime = data.ArrivalTime,
                        DepartureTime = data.DepartureTime.GetValueOrDefault()
                    })
                    .ToList();

                return history;
            }
        }
    }
}
