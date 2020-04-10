namespace AirTechFlightScheduleApp.Models
{
    internal class FlightItem
    {
        public int Flight { get; }
        public string Departure { get; }
        public string Arrival { get; }
        public int Day { get; }

        public FlightItem(int flight, string departure, string arrival, int day)
        {
            Flight = flight;
            Departure = departure;
            Arrival = arrival;
            Day = day;
        }
    }
}
