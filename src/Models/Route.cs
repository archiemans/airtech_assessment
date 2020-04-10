namespace AirTechFlightScheduleApp.Models
{
    internal class Route
    {
        public string Departure { get; }
        public string Arrival { get; }

        public Route(string departure, string arrival)
        {
            Departure = departure;
            Arrival = arrival;
        }
    }
}
