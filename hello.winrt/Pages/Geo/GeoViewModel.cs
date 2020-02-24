using System;
using hello.winrt.Pages.Wifi;
using Stylet;

namespace hello.winrt.Pages.Geo
{
    public class GeoViewModel : Screen, IFeatureTab
    {
        private readonly IWindowManager _windowManager;
        private readonly IGeoService _geoService;
        private GeoPoint _geoPoint = new GeoPoint();

        public double Longitude => _geoPoint.Longitude;
        public double Latitude => _geoPoint.Latitude;

        public GeoViewModel(IWindowManager windowManager, IGeoService geoService)
        {
            _windowManager = windowManager ?? throw new ArgumentNullException(nameof(windowManager));
            _geoService = geoService ?? throw new ArgumentNullException(nameof(geoService));
            DisplayName = "Geo";
        }

        public async void Locate()
        {
            await _windowManager.GuardedAsync(async () =>
            {
                _geoPoint = await _geoService.Locate();
            });
        }

    }
}
