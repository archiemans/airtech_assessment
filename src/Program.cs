using AirTechFlightScheduleApp.Helpers;
using AirTechFlightScheduleApp.Interfaces;
using AirTechFlightScheduleApp.Managers;
using AirTechFlightScheduleApp.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;

namespace AirTechFlightScheduleApp
{
    class Program
    {
        private static IFlightManager FlightManager;
        private static IPlaneRepository PlaneRepository;
        private static IAirportRepository AirportRepository;
        private static IOrderRepository OrderRepository;
        private static IOrderManager OrderManager;

        static void PseudoDIStartUp()
        {
            IFileHelper fileHelper = new FileHelper();
            PlaneRepository = new PlaneRepository(fileHelper);
            AirportRepository = new AirportRepository(fileHelper);
            FlightManager = new FlightManager(PlaneRepository, fileHelper);
            FlightManager.LoadFlights();
            FlightManager.RandomlyAssignPlanesToFlights();
            OrderRepository = new OrderRepository(fileHelper);
            OrderManager = new OrderManager(FlightManager, OrderRepository);
        }

        static void Main(string[] args)
        {
            try
            {

                // Mimicking Dependency Injection
                PseudoDIStartUp();

                var loopActive = true;
                var invalidChoice = false;

                // Interactive menu
                do
                {

                    #region UI Decoration
                    var bufferWidth = Console.BufferWidth;
                    Console.Clear();
                    Console.WriteLine(new String('=', bufferWidth));
                    for (var iLine = 1; iLine < 7; iLine++)
                    { Console.WriteLine($"|{new String(' ', bufferWidth - 2)}|"); }

                    Console.SetCursorPosition(2, 2);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("COMPANY:");
                    Console.SetCursorPosition(15, 2);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("TRANSPORT.LY");
                    Console.SetCursorPosition(2, 4);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("INVENTORY:");
                    Console.SetCursorPosition(3, 5);
                    Console.Write("- PLANES:");
                    Console.SetCursorPosition(15, 5);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(PlaneRepository.GetPlanes().Count);

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    var dateString = DateTime.Now.ToLongDateString();
                    var timeString = DateTime.Now.ToLongTimeString();
                    Console.SetCursorPosition(bufferWidth - (dateString.Length + timeString.Length + 5), 2);
                    Console.Write(dateString);
                    Console.SetCursorPosition(bufferWidth - (timeString.Length + 2), 2);
                    Console.Write(timeString);

                    Console.ResetColor();
                    Console.SetCursorPosition(0, 7);
                    Console.WriteLine(new String('=', bufferWidth));
                    #endregion

                    if (invalidChoice)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(0, 8);
                        Console.Write("Invalid  choice. Please try again");
                        Console.ResetColor();
                    }

                    Console.SetCursorPosition(0, 9);
                    Console.Write("Please type the number, 0 for flight Table, 1 or 2 are for user story # or type Q to Exit? ");
                    var choice = Console.ReadKey();
                    invalidChoice = false;
                    switch (choice.Key)
                    {
                        case ConsoleKey.D0:
                        case ConsoleKey.NumPad0:
                            ShowFlightTable();
                            break;
                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1:
                            RunUserStoryOne();
                            break;
                        case ConsoleKey.D2:
                        case ConsoleKey.NumPad2:
                            RunUserStoryTwo();
                            break;
                        case ConsoleKey.Q:
                            loopActive = false;
                            break;
                        default:
                            invalidChoice = true;
                            break;
                    }

                } while (loopActive);

                Console.Clear();
                Console.SetCursorPosition(Console.BufferWidth / 2 - 15, Console.WindowHeight / 2);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Thank You and Have a nice day.");
                Console.ResetColor();
                Console.SetCursorPosition(0, Console.WindowHeight - 5);
            }
            catch (Exception ex)
            {
                // Global hanlder in case we have errors
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error ({ex.GetType().ToString()}): {ex.Message}");
                Console.ResetColor();
            }

        }

        private static void ShowFlightTable()
        {
            // UI decorations
            #region UI Decoration
            var bufferWidth = Console.BufferWidth;

            Console.SetCursorPosition(0, 8);

            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.DarkMagenta;

            for (var iLine = 0; iLine < 3; iLine++)
            { Console.WriteLine(new String(' ', bufferWidth)); }
            Console.ResetColor();
            Console.WriteLine(new String('=', bufferWidth));

            Console.SetCursorPosition(Console.WindowWidth / 2 - 11, 9);
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"FLIGHT TABLE FOR {FlightManager.GetFlights().GroupBy(f => f.Day).Count()} DAYS");
            Console.SetCursorPosition(0, 12);
            Console.ResetColor();
            Console.WriteLine();

            #endregion

            // Retrieve flights 
            var flights = FlightManager.GetFlights();

            var day = 0;
            foreach (var flight in flights)
            {
                if (!flight.Day.Equals(day))
                {
                    day = flight.Day;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Day: {day}");
                    Console.ResetColor();
                }

                var departureAirport = AirportRepository.GetAirport(flight.Route.Departure);
                var arrivalAirport = AirportRepository.GetAirport(flight.Route.Arrival);

                Console.WriteLine($"Flight {flight.FlightNumber}: " +
                    $"{departureAirport.Name} airport ({departureAirport.Code}) to " +
                    $"{arrivalAirport.Name} airport ({arrivalAirport.Code})");
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\r\n To continue press any key...");
            Console.ResetColor();
            Console.ReadKey();

        }

        private static void RunUserStoryOne()
        {
            // UI decorations
            #region UI Decoration
            var bufferWidth = Console.BufferWidth;

            Console.SetCursorPosition(0, 8);

            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.DarkMagenta;

            for (var iLine = 0; iLine < 3; iLine++)
            { Console.WriteLine(new String(' ', bufferWidth)); }
            Console.ResetColor();
            Console.WriteLine(new String('=', bufferWidth));

            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(2, 8);
            Console.Write("USER STORY #1");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 11, 9);
            Console.Write($"FLIGHT TABLE FOR {FlightManager.GetFlights().GroupBy(f => f.Day).Count()} DAYS");
            Console.SetCursorPosition(0, 12);
            Console.ResetColor();
            Console.WriteLine();

            #endregion

            // Retrieve flights 
            var flights = FlightManager.GetFlights();

            foreach (var flight in flights)
            {
                Console.WriteLine($"Flight: {flight.FlightNumber}, departure: " +
                    $"{flight.Route.Departure}, " +
                    $"arrival: {flight.Route.Arrival}, day: {flight.Day}");
            }

            Console.WriteLine("\r\n Now As JSON:\r\n");

            var flightItems = FlightManager.GetFlightsAsFlat();
            var flightJson = JsonConvert.SerializeObject(flightItems,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
            Console.WriteLine(flightJson);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\r\n To continue press any key...");
            Console.ResetColor();
            Console.ReadKey();

        }

        private static void RunUserStoryTwo()
        {
            var orders = OrderManager.DistributeOrdersToFlights();
            var flights = FlightManager.GetFlights();

            #region UI Decoration
            var bufferWidth = Console.BufferWidth;

            Console.SetCursorPosition(0, 8);

            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.DarkMagenta;

            for (var iLine = 0; iLine < 5; iLine++)
            { Console.WriteLine(new String(' ', bufferWidth)); }
            Console.ResetColor();
            Console.WriteLine(new String('=', bufferWidth));

            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(2, 8);
            Console.Write("USER STORY #2");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 11, 9);
            Console.Write($"DISTRIBUTED ORDERS (BOXES): {orders.Count(o => o.FlightNumber.HasValue)}");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 15, 10);
            Console.Write($"NOT DISTRIBUTED ORDERS (BOXES): {orders.Count(o => !o.FlightNumber.HasValue)}");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 5, 11);
            Console.Write($"TOTAL ORDERS (BOXES): {orders.Count()}");

            Console.SetCursorPosition(0, 14);

            Console.ResetColor();
            Console.WriteLine();

            #endregion

            foreach (var order in orders)
            {
                if (order.FlightNumber.HasValue)
                {
                    var flight = flights.First(f => f.FlightNumber.Equals(order.FlightNumber.Value));
                    var departureAirport = AirportRepository.GetAirport(flight.Route.Departure);
                    var arrivalAirport = AirportRepository.GetAirport(flight.Route.Arrival);
                    Console.WriteLine($"order: {order.Code}, " +
                        $"flightNumber: {order.FlightNumber.Value}," +
                        $"departure: {departureAirport.Name}," +
                        $"arrival: {arrivalAirport.Name}," +
                        $"day: {flight.Day}");

                }
                else
                {
                    Console.WriteLine($"order: {order.Code}, flightNumber: not scheduled");

                }
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\r\n To continue press any key...");
            Console.ResetColor();
            Console.ReadKey();
        }

    }
}
