using AirTechFlightScheduleApp.Models;
using System.Collections.Generic;

namespace AirTechFlightScheduleApp.Interfaces
{
    internal interface IAirportRepository
    {
        Airport GetAirport(string code);
        IReadOnlyList<Airport> GetAirports();
    }
}