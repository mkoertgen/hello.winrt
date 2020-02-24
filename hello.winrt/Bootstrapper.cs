using hello.winrt.Pages;
using hello.winrt.Pages.Geo;
using hello.winrt.Pages.Media;
using hello.winrt.Pages.Wifi;
using Stylet;
using StyletIoC;

namespace hello.winrt
{
    public class Bootstrapper : Bootstrapper<ShellViewModel>
    {
        protected override void ConfigureIoC(IStyletIoCBuilder builder)
        {
            builder.Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
            builder.Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();

            builder.Autobind();
            builder.AddModules(
                new WifiModule()
                , new GeoModule()
                , new MediaModule()
                );
        }
    }
}
