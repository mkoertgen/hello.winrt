using System;
using Stylet;

namespace hello.winrt.Pages.Web
{
    internal class WebViewModel : Screen, IFeatureTab
    {
        private readonly IWindowManager _windowManager;
        public string WebUrl { get; set; } = "https://www.google.de";
        public WebViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager ?? throw new ArgumentNullException(nameof(windowManager));
            DisplayName = "Web";
        }

        public bool CanNavigate => !string.IsNullOrWhiteSpace(WebUrl);

        public void Navigate()
        {
            _windowManager.Guarded(() =>
            {
                var view = (WebView)View;
                view.Browser.Source = new Uri(WebUrl);
            });
        }
    }
}
