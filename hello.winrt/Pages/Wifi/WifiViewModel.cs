using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Stylet;

namespace hello.winrt.Pages.Wifi
{
    public class WifiViewModel : Screen, IFeatureTab
    {
        private readonly IWindowManager _windowManager;
        private readonly IWifiService _wifiService;
        public IEnumerable<string> WifiNetworks { get; private set; }
        public string SelectedWifiNetwork { get; set; }


        public WifiViewModel(IWindowManager windowManager, IWifiService wifiService)
        {
            _windowManager = windowManager ?? throw new ArgumentNullException(nameof(windowManager));
            _wifiService = wifiService ?? throw new ArgumentNullException(nameof(wifiService));
            DisplayName = "Wifi";
        }


        public async void Scan()
        {
            await _windowManager.GuardedAsync(async () =>
            {
                await _wifiService.ScanAsync();
                WifiNetworks = _wifiService.WifiNetworks;
                SelectedWifiNetwork = WifiNetworks.FirstOrDefault();
            });
        }

        public bool CanConnect => !string.IsNullOrWhiteSpace(SelectedWifiNetwork);
        public async void Connect()
        {
            await _windowManager.GuardedAsync(async () =>
            {
                await _wifiService.ConnectAsync(SelectedWifiNetwork);
                var message = $"Connected to: '{SelectedWifiNetwork}'";
                _windowManager.ShowMessageBox(message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            });
        }
    }
}
