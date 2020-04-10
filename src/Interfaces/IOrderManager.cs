using AirTechFlightScheduleApp.Models;
using System.Collections.Generic;

namespace AirTechFlightScheduleApp.Interfaces
{
    internal interface IOrderManager
    {
        IEnumerable<Order> DistributeOrdersToFlights();
    }
}