using AirTechFlightScheduleApp.Models;
using System.Collections.Generic;

namespace AirTechFlightScheduleApp.Interfaces
{
    internal interface IPlaneRepository
    {
        Plane GetPlane(string code);
        IReadOnlyList<Plane> GetPlanes(bool force = false);
    }
}