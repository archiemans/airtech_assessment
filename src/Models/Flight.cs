using AirTechFlightScheduleApp.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace AirTechFlightScheduleApp.Models
{
    internal class Flight
    {
        private IDictionary<string, Order> orders;
        private Plane _assignedPlane = null;

        public int FlightNumber { get; }
        public Plane AssignedPlane { get { return _assignedPlane; }
            set 
            { 
                if (orders.Count > 0)
                {
                    throw new OrdersAlreadyLoadedException();
                }
                _assignedPlane = value;
            } 
        }
        public Route Route { get; }
        public int Day { get; }

        public Flight(int flightNumber, Route route, int day)
        {
            FlightNumber = flightNumber;
            Route = route;
            Day = day;
            orders = new Dictionary<string, Order>();
        }

        public bool LoadOrder(Order order)
        {
            if (_assignedPlane == null)
            {
                throw new PlaneIsNotAssignedException(FlightNumber);
            }

            if (orders.Count >= _assignedPlane.Capacity)
            {
                throw new PlaneIsFullException(_assignedPlane.Capacity);
            }

            return orders.TryAdd(order.Code.ToLower(), order);
        }

        public Order UnloadOrder(string orderCode)
        {
            var key = orderCode.ToLower();

            if (orders.TryGetValue(key, out var order))
            {
                orders.Remove(key);
                return order;
            }

            throw new OrderIsNotOnThePlaneException(orderCode);
        }

        public IReadOnlyList<Order> UnloadOrders()
        {
            var ordersList = orders.Values.ToList().AsReadOnly();
            orders.Clear();
            return ordersList;
        }

        public int RemainingCapacity
        {
            get
            {
                if (_assignedPlane == default)
                {
                    return 0;
                }

                return _assignedPlane.Capacity - orders.Count();
            }
        }

    }
}
