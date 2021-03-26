using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceParkModel.Database
{
    public class Payment
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Occupancy")]
        public int OccupancyID { get; set; }
        public decimal Amount { get; set; }
    }
}
