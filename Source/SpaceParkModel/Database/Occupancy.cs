using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceParkModel.Database
{
    public class Occupancy
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Person")]
        public int PersonID { get; set; }
        [ForeignKey("Spaceship")]
        public int SpaceshipID { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime? DepartureTime { get; set; }
        [ForeignKey("ParkingSpots")]
        public int ParkingSpotID { get; set; }
    }
}
