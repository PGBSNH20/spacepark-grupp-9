using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceParkModel.Database
{
    public class ParkingSpots
    {
        [Key]
        public int Spot { get; set; }
        [ForeignKey("ParkingSize")]
        public int ParkingSizeID { get; set; }
    }
}
