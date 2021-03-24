using System.ComponentModel.DataAnnotations;

namespace SpaceParkModel.Data
{
    public class Spaceship
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
