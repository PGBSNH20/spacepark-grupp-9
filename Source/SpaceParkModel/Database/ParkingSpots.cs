using System.ComponentModel.DataAnnotations;

namespace SpaceParkModel.Database
{
    public class ParkingSpots
    {
        [Key]
        public int Spot { get; set; }
        public int ParkingSizeID { get; set; }
    }
}
