using AirTechFlightScheduleApp.Exceptions;
using AirTechFlightScheduleApp.Helpers;
using AirTechFlightScheduleApp.Interfaces;
using AirTechFlightScheduleApp.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace AirTechFlightScheduleApp.Repositories
{
    internal class PlaneRepository : IPlaneRepository
    {
        private IFileHelper _fileHelper;
        private IEnumerable<Plane> _planes;

        public PlaneRepository(IFileHelper fileHelper)
        {
            _fileHelper = fileHelper;
            _planes = new List<Plane>();
        }

        public IReadOnlyList<Plane> GetPlanes(bool force = false)
        {
            if (_planes.Any() && !force)
            {
                return _planes.ToList().AsReadOnly();
            }
            var planesJson = _fileHelper.ReadFileAsString("./Data/planes.json");
            _planes = JsonConvert.DeserializeObject<IEnumerable<Plane>>(planesJson);

            return _planes.ToList().AsReadOnly();
        }

        public Plane GetPlane(string code)
        {
            var plane = GetPlanes().FirstOrDefault(p => p.Code.Equals(code, System.StringComparison.OrdinalIgnoreCase));
            if (plane == default)
            {
                throw new PlaneIsNotFoundException(code);
            }
            return plane;
        }
    }
}
