using AirTechFlightScheduleApp.Helpers;
using AirTechFlightScheduleApp.Interfaces;
using AirTechFlightScheduleApp.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace AirTechFlightScheduleApp.Repositories
{
    internal class OrderRepository : IOrderRepository
    {
        private IFileHelper _fileHelper;
        private List<Order> _orders;


        public OrderRepository(IFileHelper fileHelper)
        {
            _fileHelper = fileHelper;
            _orders = new List<Order>();
        }

        public IEnumerable<Order> GetOrders()
        {
            if (_orders.Any())
            {
                return _orders;
            }

            var ordersJson = _fileHelper.ReadFileAsString("./Data/coding-assigment-orders.json");
            var jobj = JObject.Parse(ordersJson);
            foreach (var jProp in jobj.Properties())
            {
                _orders.Add( new Order(jProp.Name, jProp.Value["destination"].ToString()));
            }

            return _orders;
        }
    }
}
