using System;

namespace AirTechFlightScheduleApp.Exceptions
{
    internal class PlaneIsNotFoundException : ApplicationException
    {
        public PlaneIsNotFoundException(string code)
            : base($"Plane with code: '{code}' is not found in repository.")
        {
            
        }
    }
}
