using System;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Stylet;

namespace hello.winrt.Pages.Geo
{
    public class GeoViewModel : Screen, IFeatureTab
    {
        private readonly IWindowManager _windowManager;
        private readonly IGeoService _geoService;
        public Geopoint Location { get; set; }

        public string FormattedLocation =>
            $"Lon: {Location?.Position.Longitude}, Lat: {Location?.Position.Latitude}";

        public GeoViewModel(IWindowManager windowManager, IGeoService geoService)
        {
            _windowManager = windowManager ?? throw new ArgumentNullException(nameof(windowManager));
            _geoService = geoService ?? throw new ArgumentNullException(nameof(geoService));
            DisplayName = "Geo";
        }

        public bool CanLocate => _geoService.CanLocate;
        public async void Locate()
        {
            await _windowManager.GuardedAsync(async () =>
            {
                var position = await _geoService.Locate();
                Location = new Geopoint(position);
                await SetMap();
            });
        }

        public async Task SetMap()
        {
            var view = (GeoView)View;
            await view.MapControl.TrySetViewAsync(Location);
        }
    }
}
