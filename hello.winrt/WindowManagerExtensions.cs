using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using Stylet;

namespace hello.winrt
{
    internal static class WindowManagerExtensions
    {
        public static async Task GuardedAsync(this IWindowManager windowManager,
            Func<Task> action, [CallerMemberName] string actionName = null)
        {
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                var message = $"Could not {actionName}: {ex}";
                windowManager.ShowMessageBox(message,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
