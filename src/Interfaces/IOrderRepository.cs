using AirTechFlightScheduleApp.Models;
using System.Collections.Generic;

namespace AirTechFlightScheduleApp.Interfaces
{
    internal interface IOrderRepository
    {
        IEnumerable<Order> GetOrders();
    }
}