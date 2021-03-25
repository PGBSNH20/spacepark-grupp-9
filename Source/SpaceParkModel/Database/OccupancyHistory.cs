using System;

namespace SpaceParkModel.Database
{
    public class OccupancyHistory
    {
        public string PersonName { get; set; }
        public string SpaceshipName { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }
    }
}
