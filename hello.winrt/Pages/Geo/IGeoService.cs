using System.Threading.Tasks;

namespace hello.winrt.Pages.Geo
{
    public interface IGeoService
    {
        Task<GeoPoint> Locate();
    }
}
