using AirTechFlightScheduleApp.Exceptions;
using AirTechFlightScheduleApp.Helpers;
using AirTechFlightScheduleApp.Interfaces;
using AirTechFlightScheduleApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AirTechFlightScheduleApp.Repositories
{
    internal class AirportRepository : IAirportRepository
    {
        private IEnumerable<Airport> _airports;
        private IFileHelper _fileHelper;

        public AirportRepository(IFileHelper fileHelper)
        {
            _airports = new List<Airport>();
            _fileHelper = fileHelper;
        }
        public IReadOnlyList<Airport> GetAirports()
        {
            if (_airports.Any())
            {
                return _airports.ToList().AsReadOnly();
            }

            var airportsJson = _fileHelper.ReadFileAsString("./Data/airports.json");
            _airports = JsonConvert.DeserializeObject<IEnumerable<Airport>>(airportsJson);

            return _airports.ToList().AsReadOnly();
        }

        public Airport GetAirport(string code)
        {
            var airport = GetAirports().FirstOrDefault(a => a.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
            if (airport == default)
            {
                throw new AirportIsNotFoundException(code);
            }

            return airport;
        }
    }
}
