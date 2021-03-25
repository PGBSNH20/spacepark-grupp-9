using System.Collections.Generic;

namespace SpaceParkModel.SwApi
{
    public class SwResource<T>
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public List<T> Results { get; set; }
    }
}