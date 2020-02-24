using StyletIoC;

namespace hello.winrt.Pages.Web
{
    internal class WebModule : StyletIoCModule
    {
        protected override void Load()
        {
            Bind<IFeatureTab>().To<WebViewModel>();
        }
    }
}
