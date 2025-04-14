using Avalonia.Controls;
using Avalonia.Platform;
using Avalonia.Threading;
using CefDotnet;
using CefDotnet.CefApi;
using CefDotnet.CefApi.Internal;
using CefDotnet.CefApi.Types;
using CefDotnet.CefWrap;
using CefDotnet.Win;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AvaloniaDemo
{
    internal class CefControl : Control
    {
        RefCefBrowser? _CefBrowser;

        unsafe public CefControl()
        {
            Loaded += (o, a) =>
            {
                var handle = TopLevel.GetTopLevel(this)?.TryGetPlatformHandle();
                if (handle == null)
                {
                    Debug.WriteLine("CefControl| TryGetPlatformHandle false");
                    return;
                }

                DetachedFromLogicalTree += (o, a) =>
                {
                    if (_CefBrowser == null)
                    {
                        return;
                    }

                    using var host = _CefBrowser.GetHost();
                    host.Ptr->close_browser(host.Ptr, 0);
                };

                _CefClient = new CefClient();
                _CefClient.CefLifeSpanHandler.OnAfterCreated = (browser) =>
                {
                    if (_CefBrowser != null)
                    {//dev tool
                        return;
                    }

                    _CefBrowser = new RefCefBrowser(browser);
                    using var host = _CefBrowser.GetHost();
                    var wnd = host.Ptr->get_window_handle(host.Ptr);

                    Dispatcher.UIThread.Post(() =>
                    {
                        _BrowserHost = new BrowserHost(wnd);
                        VisualChildren.Add(_BrowserHost);
                        InvalidateMeasure();
                    });
                };
                _CefClient.CefLifeSpanHandler.OnBeforeClose = (browser) =>
                {
                    if (true != _CefBrowser?.IsSame(browser))
                    {
                        return;
                    }

                    _CefBrowser.Dispose();
                    _CefBrowser = null;
                };

                string str = "www.bing.com";
                var ret = Cef.CreateBrowser(_CefClient, handle.Handle, str);
                Debug.WriteLine($"CreateBrowser ret={ret}");
            };
        }

        nint Handle;
        CefClient? _CefClient;
        BrowserHost? _BrowserHost;

        public unsafe void Navigate(string? url)
        {
            if (_CefBrowser == null || string.IsNullOrEmpty(url))
            {
                return;
            }

            using var frame = _CefBrowser.GetMainFrame();
            url = url.Trim();
            fixed (char* p = url)
            {
                cef_string_t str = new cef_string_t() { str = p, length = (nuint)url.Length, };
                frame.Ptr->load_url(frame.Ptr, &str);
            }
        }

        public unsafe void Reload()
        {
            if (_CefBrowser == null)
            {
                return;
            }

            _CefBrowser.Ptr->reload(_CefBrowser.Ptr);
        }
    }
}
