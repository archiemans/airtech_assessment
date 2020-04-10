namespace AirTechFlightScheduleApp.Models
{
    internal class Order
    {
        public Order (string code, string destination)
        {
            Code = code;
            Destination = destination;
        }
        public string Code { get; }
        public string Destination { get; }
        public int? FlightNumber { get; set; }
    }
}
