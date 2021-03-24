using System.ComponentModel.DataAnnotations;

namespace SpaceParkModel.Data
{
    public class Person
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
