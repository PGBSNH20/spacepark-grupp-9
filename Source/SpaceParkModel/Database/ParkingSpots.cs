using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
