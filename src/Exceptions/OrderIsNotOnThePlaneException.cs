using System;

namespace AirTechFlightScheduleApp.Exceptions
{
    internal class OrderIsNotOnThePlaneException : ApplicationException
    {
        public OrderIsNotOnThePlaneException(string orderCode)
            : base($"The order with code: '{orderCode}' is not on the plane.") { }
    }
}
