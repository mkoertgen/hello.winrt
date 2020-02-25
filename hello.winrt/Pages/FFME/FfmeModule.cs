using System;
using System.Diagnostics;
using System.IO;
using StyletIoC;

namespace hello.winrt.Pages.FFME
{
    internal class FfmeModule : StyletIoCModule
    {
        protected override void Load()
        {
            try
            {
                Unosquare.FFME.Library.FFmpegDirectory = FindFFMpeg();
                Bind<IFeatureTab>().To<FfmeViewModel>();
            }
            catch (InvalidOperationException ex)
            {
                Trace.TraceError("Could not register FFME Module: {0}", ex);
            }
        }

        private static string FindFFMpeg()
        {
            var searchPaths = new[]
            {
                @"c:\ffmpeg",
                @"%ChocolateyInstall%\lib\ffmpeg\tools\ffmpeg\bin",
            };

            foreach (var searchPath in searchPaths)
            {
                var path = Environment.ExpandEnvironmentVariables(searchPath);
                var ffmpegPath = Path.Combine(path, "ffmpeg.exe");
                if (File.Exists(ffmpegPath))
                    return path;
            }
            throw  new InvalidOperationException("Could not find FFMPEG");
        }
    }
}
