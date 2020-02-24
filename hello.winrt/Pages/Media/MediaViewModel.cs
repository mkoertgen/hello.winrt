using System;
using Windows.Media.Core;
using Stylet;

namespace hello.winrt.Pages.Media
{
    internal class MediaViewModel : Screen, IFeatureTab
    {
        private readonly IWindowManager _windowManager;

        public MediaViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager ?? throw new ArgumentNullException(nameof(windowManager));
            DisplayName = "Media";
            MediaUrl = "https://archive.org/download/Wildlife_20160527/Wildlife.mp4";
        }

        public string MediaUrl { get; set; }

        public bool CanPlay => !string.IsNullOrWhiteSpace(MediaUrl);
        public void Play()
        {
            _windowManager.Guarded(() =>
            {
                var uri = new Uri(MediaUrl);
                var view = (MediaView)View;
                var source = MediaSource.CreateFromUri(uri);
                view.MediaPlayer.MediaPlayer.Source = source;
            });
        }
    }
}
