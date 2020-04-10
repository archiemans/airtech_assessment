using AirTechFlightScheduleApp.Interfaces;
using AirTechFlightScheduleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirTechFlightScheduleApp.Managers
{
    internal class OrderManager : IOrderManager
    {
        private IFlightManager _flightManager;
        private IOrderRepository _orderRepository;

        public OrderManager(IFlightManager flightManager, IOrderRepository orderRepository)
        {
            _flightManager = flightManager;
            _orderRepository = orderRepository;
        }


        public IEnumerable<Order> DistributeOrdersToFlights()
        {
            var orders = _orderRepository.GetOrders();
            var flights = _flightManager.GetFlights();

            foreach (var flight in flights)
            {
                if (flight.RemainingCapacity > 0)
                {
                    var flightOrders = orders
                        .Where(o => o.Destination.Equals(flight.Route.Arrival, StringComparison.OrdinalIgnoreCase) && !o.FlightNumber.HasValue)
                        .OrderBy(o => o.Code)
                        .Take(flight.RemainingCapacity);

                    foreach (var order in flightOrders)
                    {
                        if (flight.LoadOrder(order))
                        {
                            order.FlightNumber = flight.FlightNumber;
                        }
                    }
                }
            }

            return orders;
        }
    }
}
