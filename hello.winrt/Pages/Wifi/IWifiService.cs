using System.Collections.Generic;
using System.Threading.Tasks;

namespace hello.winrt.Pages.Wifi
{
    public interface IWifiService
    {
        Task ScanAsync();
        IEnumerable<string> WifiNetworks { get; }
        Task ConnectAsync(string ssid);
    }
}
