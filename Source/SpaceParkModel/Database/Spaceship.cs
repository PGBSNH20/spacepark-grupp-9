using System.ComponentModel.DataAnnotations;

namespace SpaceParkModel.Database
{
    public class Spaceship
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
