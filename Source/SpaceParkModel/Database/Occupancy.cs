using System;
using System.ComponentModel.DataAnnotations;

namespace SpaceParkModel.Data
{
    public class Occupancy
    {
        [Key]
        public int ID { get; set; }
        public int PersonID { get; set; }
        public int SpaceshipID { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime? DepartureTime { get; set; }
        public int ParkingSpotID { get; set; }
    }
}
