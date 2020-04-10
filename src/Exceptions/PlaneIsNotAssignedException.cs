using System;

namespace AirTechFlightScheduleApp.Exceptions
{
    internal class PlaneIsNotAssignedException : ApplicationException
    {
        public PlaneIsNotAssignedException(int flightNumber)
            : base($"Cannot load orders. Please assign a plane to the flight: {flightNumber} before loading any orders.")
        {

        }
    }
}
