using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using Stylet;
using Unosquare.FFME;

namespace hello.winrt.Pages.FFME
{
    internal class FfmeViewModel : Screen, IFeatureTab

    {
        private readonly IWindowManager _windowManager;

        public FfmeViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager ?? throw new ArgumentNullException(nameof(windowManager));
            DisplayName = "FFme";
            MediaUrl = "https://archive.org/download/Wildlife_20160527/Wildlife.mp4";
        }

        public string MediaUrl { get; set; }
        public string MediaInformation { get; set; }

        public bool CanOpen => !string.IsNullOrWhiteSpace(MediaUrl);
        public async void Open()
        {
            await MediaCommand(async m =>
            {
                var ok = await m.Open(new Uri(MediaUrl));
                if (ok) MediaInformation = GetMediaInformation(m);
                return ok;
            });

        }

        public async void Play()
        {
            await MediaCommand(async m => await m.Play());
        }

        public async void Pause()
        {
            await MediaCommand(async m => await m.Pause());
        }

        public void ShowInfo()
        {
            _windowManager.ShowMessageBox(MediaInformation, "Information",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private async Task MediaCommand(Func<MediaElement, Task<bool>> action,
            [CallerMemberName]string actionName = null)
        {
            await _windowManager.GuardedAsync(async () =>
            {
                var view = (FfmeView)View;
                var ok = await action(view.MediaPlayer);
                if (!ok) throw new InvalidOperationException($"Could not '{actionName}'.");
            });
        }

        private static string GetMediaInformation(MediaElement m)
        {
            var info = new
            {
                MediaFormat = m.MediaFormat.Replace(',', ';'),
                m.MediaStreamSize,
                m.BitRate,
                m.NaturalDuration,
                m.IsLiveStream,
                m.DecodingBitRate,
                m.HasVideo,
                m.HasAudio,
                m.VideoStreamIndex,
                m.VideoCodec,
                m.VideoHardwareDecoder,
                m.VideoBitRate,
                m.VideoRotation,
                m.NaturalVideoWidth,
                m.NaturalVideoHeight,
                m.VideoFrameRate,
                m.VideoSmtpeTimeCode,
                m.VideoAspectRatio,
                m.AudioStreamIndex,
                m.AudioCodec,
                m.AudioBitRate,
                m.AudioChannels,
                m.AudioSampleRate,
                m.AudioBitsPerSample,
                m.HasSubtitles,
                m.SubtitleStreamIndex,
            };
            return info.ToString()
                .Trim('{', '}')
                .Replace(',', '\n');
        }
    }
}
