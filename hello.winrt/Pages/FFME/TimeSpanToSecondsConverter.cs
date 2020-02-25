using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace hello.winrt.Pages.FFME
{
    internal class TimeSpanToSecondsConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case TimeSpan span:
                    return span.TotalSeconds;
                case Duration duration:
                    return duration.HasTimeSpan ? duration.TimeSpan.TotalSeconds : 0d;
                default:
                    return 0d;
            }
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double == false) return 0d;
            var result = TimeSpan.FromTicks(System.Convert.ToInt64(TimeSpan.TicksPerSecond * (double)value));

            // Do the conversion from visibility to bool
            if (targetType == typeof(TimeSpan)) return result;
            return targetType == typeof(Duration) ?
                new Duration(result) : Activator.CreateInstance(targetType);
        }
    }
}
