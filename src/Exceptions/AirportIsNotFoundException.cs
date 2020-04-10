using System;

namespace AirTechFlightScheduleApp.Exceptions
{
    internal class AirportIsNotFoundException : ApplicationException
    {
        public AirportIsNotFoundException(string code)
            : base($"Airport with code: '{code}' can not be found.")
        {

        }
    }
}
