using System;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace hello.winrt.Pages.Geo
{
    internal class GeoService : IGeoService
    {
        public async Task<GeoPoint> Locate()
        {
            var locator = new Geolocator();
            var location = await locator.GetGeopositionAsync();
            var position = location.Coordinate.Point.Position;
            return ToPoint(position);
        }

        private static GeoPoint ToPoint(BasicGeoposition position)
        {
            return new GeoPoint
            {
                Longitude = position.Longitude,
                Latitude = position.Latitude,
                Altitude = position.Altitude
            };
        }
    }
}
