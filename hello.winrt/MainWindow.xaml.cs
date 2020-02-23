using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using Windows.Devices.Geolocation;
using Windows.Devices.WiFi;
using hello.winrt.Annotations;

namespace hello.winrt
{
    public partial class MainWindow : INotifyPropertyChanged
    {
        private WiFiAdapter _wifiAdapter;
        private string _selectedWifiNetwork;
        private readonly List<string> _wifiNetworks = new List<string>();

        public List<string> WifiNetworks
        {
            get
            {
                return _wifiNetworks;
            }
            private set
            {
                _wifiNetworks.Clear();
                _wifiNetworks.AddRange(value);
                OnPropertyChangedAsync();
                SelectedWifiNetwork = _wifiNetworks.FirstOrDefault();
            }
        }

        public string SelectedWifiNetwork
        {
            get => _selectedWifiNetwork;
            set
            {
                if (value == _selectedWifiNetwork) return;
                _selectedWifiNetwork = value;
                OnPropertyChangedAsync();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private async void GetLocation(object sender, RoutedEventArgs e)
        {
            await SafeExecute(async () =>
            {
                //var accessStatus = await Geolocator.RequestAccessAsync();
                var locator = new Geolocator();
                var location = await locator.GetGeopositionAsync();
                var position = location.Coordinate.Point.Position;
                var message = $"lat:{position.Latitude}, long:{position.Longitude}";
                MessageBox.Show(message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            });
        }


        private async void ScanWifiNetworks(object sender, RoutedEventArgs e)
        {
            await SafeExecute(async () =>
            {
                var wifiAdapter = await GetWifiAdapter();
                await wifiAdapter.ScanAsync();
                WifiNetworks = wifiAdapter.NetworkReport.AvailableNetworks
                    .Select(n => n.Ssid)
                    .Distinct()
                    .ToList();
            });
        }

        private async void ConnectWifi(object sender, RoutedEventArgs e)
        {
            await SafeExecute(async () =>
            {
                var wifiAdapter = await GetWifiAdapter();
                await wifiAdapter.ScanAsync();
                var networks = wifiAdapter.NetworkReport.AvailableNetworks;
                var wifiNetwork = networks.First(n => n.Ssid == SelectedWifiNetwork);
                var result = await wifiAdapter.ConnectAsync(wifiNetwork, WiFiReconnectionKind.Automatic);
                var message = $"WiFi connection status: '{SelectedWifiNetwork}': {result.ConnectionStatus}";
                MessageBox.Show(message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            });
        }

        private static async Task SafeExecute(Func<Task> action, [CallerMemberName] string actionName = null)
        {
            try { await action(); }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not '{actionName}': {ex}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task<WiFiAdapter> GetWifiAdapter()
        {
            if (_wifiAdapter != null) return _wifiAdapter;
            //var accessStatus = await WiFiAdapter.RequestAccessAsync();
            var devices =
                await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(WiFiAdapter.GetDeviceSelector());
            _wifiAdapter = await WiFiAdapter.FromIdAsync(devices[0].Id);
            return _wifiAdapter;

        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChangedAsync([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
