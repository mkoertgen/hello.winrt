using System;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;

namespace hello.winrt.Pages.Geo
{
    internal class GeoService : IGeoService
    {
        public bool CanLocate { get; private set; }

        public GeoService()
        {
            Geolocator.RequestAccessAsync().Completed += RequestAccessCompleted;
        }

        public async Task<BasicGeoposition> Locate()
        {
            var locator = new Geolocator();
            var location = await locator.GetGeopositionAsync();
            return location.Coordinate.Point.Position;
        }

        private void RequestAccessCompleted(IAsyncOperation<GeolocationAccessStatus> asyncInfo, AsyncStatus asyncStatus)
        {
            CanLocate = asyncStatus == AsyncStatus.Completed &&
                        asyncInfo.Status == AsyncStatus.Completed &&
                        asyncInfo.GetResults() == GeolocationAccessStatus.Allowed;
        }
    }
}
