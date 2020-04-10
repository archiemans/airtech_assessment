using AirTechFlightScheduleApp.Helpers;
using AirTechFlightScheduleApp.Interfaces;
using AirTechFlightScheduleApp.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace AirTechFlightScheduleApp.Managers
{
    internal class FlightManager : IFlightManager
    {
        private List<Flight> _flights;
        private IPlaneRepository _planeRepository;
        private IFileHelper _fileHelper;

        public FlightManager(IPlaneRepository planeRepository, IFileHelper fileHelper)
        {
            _flights = new List<Flight>();
            _planeRepository = planeRepository;
            _fileHelper = fileHelper;
        }

        public IReadOnlyList<Flight> GetFlights()
        {
            return _flights.AsReadOnly();
        }

        public IEnumerable<FlightItem> GetFlightsAsFlat()
        {
            return _flights.Select(f => new FlightItem(f.FlightNumber, f.Route.Departure, f.Route.Arrival, f.Day));
        }

        public void LoadFlights()
        {

            var flightJson = _fileHelper.ReadFileAsString("./Data/flights.json");
            _flights = JsonConvert.DeserializeObject<IEnumerable<Flight>>(flightJson).ToList();

        }

        public void RandomlyAssignPlanesToFlights()
        {
            var planes = _planeRepository.GetPlanes();

            foreach (var plane in planes)
            {
                var days = _flights.Select(f => f.Day).Distinct();
                foreach (var day in days)
                {
                    var flight = _flights.FirstOrDefault(f => f.Day == day && f.AssignedPlane == null);
                    if (flight != default)
                    {
                        flight.AssignedPlane = plane;
                    }
                }
            }
        }
    }
}
