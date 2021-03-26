using System.ComponentModel.DataAnnotations;

namespace SpaceParkModel.Database
{
    public class Person
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
