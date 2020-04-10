using System.IO;

namespace AirTechFlightScheduleApp.Helpers
{
    internal class FileHelper : IFileHelper
    {
        public string ReadFileAsString(string filePath)
        {
            using var file = File.OpenRead(filePath);
            using var reader = new StreamReader(file);
            var jsonString = reader.ReadToEnd();
            return jsonString;
        }
    }
}
