using StyletIoC;

namespace hello.winrt.Pages.Wifi
{
    public class WifiModule : StyletIoCModule
    {
        protected override void Load()
        {
            Bind<IFeatureTab>().To<WifiViewModel>();
            Bind<IWifiService>().To<WifiService>();
        }
    }
}
