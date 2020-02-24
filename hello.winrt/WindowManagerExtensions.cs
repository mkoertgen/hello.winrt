using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using Stylet;

namespace hello.winrt
{
    internal static class WindowManagerExtensions
    {
        public static void Guarded(this IWindowManager windowManager,
            Action action, [CallerMemberName] string actionName = null)
        {
            try { action(); }
            catch (Exception ex) { windowManager.ShowException(ex, actionName); }
        }

        public static async Task GuardedAsync(this IWindowManager windowManager,
            Func<Task> action, [CallerMemberName] string actionName = null)
        {
            try { await action(); }
            catch (Exception ex) { windowManager.ShowException(ex, actionName); }
        }

        public static void ShowException(this IWindowManager windowManager, Exception ex,
            [CallerMemberName] string actionName = null)
        {
            var message = $"Could not {actionName}: {ex}";
            windowManager.ShowMessageBox(message,
                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
