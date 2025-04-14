using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using CefDotnet;
using CefDotnet.CefWrap;
using System.Threading.Tasks;

namespace AvaloniaDemo
{
    public partial class App : Application
    {
        public static Window? MainWindow => (Current as App)?._Desktop?.MainWindow;
        IClassicDesktopStyleApplicationLifetime? _Desktop;
        CefApp _CefApp = new CefApp();


        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                _Desktop = desktop;
                desktop.MainWindow = new MainWindow();
                desktop.Exit += (o, a) =>
                {
                    Task.Run(() =>
                    {
                        Cef.Shutdown();
                    });
                };
                Cef.Init(_CefApp);
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}