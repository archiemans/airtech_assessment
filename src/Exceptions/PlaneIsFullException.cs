using System;

namespace AirTechFlightScheduleApp.Exceptions
{
    internal class PlaneIsFullException : ApplicationException
    {
        public PlaneIsFullException(int planeTotaCapacity) 
            : base($"Cannot add the order. The plane is fully loaded. Plane's capacity is: {planeTotaCapacity}")
        {
        }

    }
}
