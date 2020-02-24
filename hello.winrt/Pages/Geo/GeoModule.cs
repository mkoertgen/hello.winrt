using StyletIoC;

namespace hello.winrt.Pages.Geo
{
    public class GeoModule : StyletIoCModule
    {
        protected override void Load()
        {
            Bind<IFeatureTab>().To<GeoViewModel>();
            Bind<IGeoService>().To<GeoService>();
        }
    }
}
