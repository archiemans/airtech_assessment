﻿namespace AirTechFlightScheduleApp.Models
{
    internal class Airport
    {
        public string Code { get; }
        public string Name { get; }

        public Airport(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
