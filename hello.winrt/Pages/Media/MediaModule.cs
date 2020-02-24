using StyletIoC;

namespace hello.winrt.Pages.Media
{
    internal class MediaModule : StyletIoCModule
    {
        protected override void Load()
        {
            Bind<IFeatureTab>().To<MediaViewModel>();
        }
    }
}
