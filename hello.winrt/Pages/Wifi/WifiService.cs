using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.WiFi;

namespace hello.winrt.Pages.Wifi
{
    internal class WifiService : IWifiService
    {
        private WiFiAdapter _wifiAdapter;
        public async Task ScanAsync()
        {
            var wifiAdapter = await GetWifiAdapter();
            await wifiAdapter.ScanAsync();
        }

        public IEnumerable<string> WifiNetworks => _wifiAdapter?.NetworkReport.AvailableNetworks
            .Select(n => n.Ssid)
            .Distinct()
            .ToList();

        public async Task ConnectAsync(string ssid)
        {
            var wifiAdapter = await GetWifiAdapter();
            var connectedProfile = await wifiAdapter.NetworkAdapter.GetConnectedProfileAsync();
            if (connectedProfile.WlanConnectionProfileDetails.GetConnectedSsid() == ssid)
            {
                Trace.TraceInformation($"Already connected to SSID='{ssid}'");
                return;
            }

            var networks = wifiAdapter.NetworkReport.AvailableNetworks
                .Where(n => n.Ssid == ssid)
                .ToList();
            var status = WiFiConnectionStatus.NetworkNotAvailable;
            foreach (var network in networks)
            {
                var result = await wifiAdapter.ConnectAsync(network,
                    WiFiReconnectionKind.Automatic);
                status = result.ConnectionStatus;
                if (status == WiFiConnectionStatus.Success) return;
            }
            throw new InvalidOperationException($"Could not connect to '{ssid}': {status}");
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
    }
}
