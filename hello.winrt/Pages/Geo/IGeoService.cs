using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace hello.winrt.Pages.Geo
{
    public interface IGeoService
    {
        Task<BasicGeoposition> Locate();
        bool CanLocate { get; }
    }
}
