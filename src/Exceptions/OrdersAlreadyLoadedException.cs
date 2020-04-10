using System;
using System.Collections.Generic;
using System.Text;

namespace AirTechFlightScheduleApp.Exceptions
{
    internal class OrdersAlreadyLoadedException : ApplicationException
    {
        public OrdersAlreadyLoadedException() 
            : base("Cannot change the plane as orders already loaded. Please unload orders before changing the plane.")
        {

        }
    }
}
