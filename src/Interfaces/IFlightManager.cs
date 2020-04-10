using AirTechFlightScheduleApp.Models;
using System.Collections.Generic;

namespace AirTechFlightScheduleApp.Interfaces
{
    internal interface IFlightManager
    {
        IReadOnlyList<Flight> GetFlights();
        IEnumerable<FlightItem> GetFlightsAsFlat();
        void LoadFlights();
        void RandomlyAssignPlanesToFlights();
    }
}