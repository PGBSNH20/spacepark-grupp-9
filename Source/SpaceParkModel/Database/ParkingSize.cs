using System.ComponentModel.DataAnnotations;

namespace SpaceParkModel.Database
{
    public class ParkingSize
    {
        [Key]
        public int ID { get; set; }
        public int Size { get; set; }
        public decimal Price { get; set; }
    }
}
