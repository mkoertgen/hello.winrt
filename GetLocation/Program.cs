using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace GetLocation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var program = new Program(args);
            await program.Locate();
        }

        public Program(params string[] args)
        {
        }

        public async Task Locate()
        {
            Console.WriteLine("Requesting location access...");
            var status = await Geolocator.RequestAccessAsync(); //.Completed += RequestAccessCompleted;
            Console.WriteLine($"Access '{status}'.");

            var sw = Stopwatch.StartNew();
            Console.WriteLine("Locating...");
            var locator = new Geolocator();
            var location = await locator.GetGeopositionAsync();

            var point = location.Coordinate.Point.Position;

            Console.WriteLine($"Location: Lon={point.Longitude}, Lat={point.Latitude} (took {sw.Elapsed})");
        }
    }
}
