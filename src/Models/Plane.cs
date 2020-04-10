
namespace AirTechFlightScheduleApp.Models
{
    internal class Plane
    {
        public Plane(string code, int capacity)
        {
            Code = code;
            Capacity = capacity;
        }
        public int Capacity { get; }
        public string Code { get; }
    }
}
