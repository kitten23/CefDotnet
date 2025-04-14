using CefDotnet.CefApi.Internal;
using CefDotnet.CefApi.Types;
using CefDotnet.CefWrap;
using CefDotnet.Win;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static CefDotnet.CefApi.libcef;

namespace CefDotnet;

public class Cef
{
    static int _HasTryInit = 0;
    public static bool IsInitOk { get; private set; } = false;
    static ConcurrentDictionary<nint, RefCefBrowser> _BrowsrDict = [];

    unsafe public static bool Init(CefApp app, string local = "en-US")
    {
        try
        {
            var hasTryInit = Interlocked.Exchange(ref _HasTryInit, 1);
            if (hasTryInit == 1)
            {
                return IsInitOk;
            }

            CheckVersion();

            cef_main_args_t mainArgs = new()
            {
                instance = kernel32.GetModuleHandleW(null)
            };

            string cachePath = Path.Combine(AppContext.BaseDirectory, "cache");

            fixed (char* pCachePath = cachePath, pLocal = local)
            {
                cef_settings_t settings = new cef_settings_t()
                {
                    multi_threaded_message_loop = 1,
                    no_sandbox = 1,
                };

                settings.root_cache_path.str = pCachePath;
                settings.root_cache_path.length = (nuint)cachePath.Length;
                settings.locale.str = pLocal;
                settings.locale.length = (nuint)local.Length;

                var res = cef_initialize(&mainArgs, &settings, app.GetRef(), IntPtr.Zero);
                Debug.WriteLine($"cef_initialize {(res == 1 ? "ok" : "fail")}");
                IsInitOk = res == 1;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }

        return IsInitOk;
    }

    unsafe public static void CheckVersion()
    {
        string version = $"{cef_version_info(0)}.{cef_version_info(1)}.{cef_version_info(2)}.{cef_version_info(3)}";
        Debug.WriteLine($"cef version = {version}");

        string chromiumVersion = $"{cef_version_info(4)}.{cef_version_info(5)}.{cef_version_info(6)}.{cef_version_info(7)}";
        Debug.WriteLine($"chromium version = {chromiumVersion}");

        var pApiHash = cef_api_hash(CEF_API_VERSION, 0);
        if (pApiHash == nint.Zero)
        {
            Debug.WriteLine("cef_api_hash fail");
        }

        string? apiHash = Marshal.PtrToStringUTF8(pApiHash);
        Debug.WriteLine($"cef api hash = {apiHash},");
    }

    public unsafe static void Shutdown()
    {
        //Debug.WriteLine($"cef_shutdown [{Thread.CurrentThread.ManagedThreadId}]");
        cef_shutdown();
        Debug.WriteLine($"cef_shutdown [{Thread.CurrentThread.ManagedThreadId}] done");
    }

    public unsafe static int CreateBrowser(CefClient client, nint parent, string url = "")
    {
        cef_window_info_t windowInfo = new cef_window_info_t();
        cef_rect_t rect = new cef_rect_t() { x = 0, y = 0, width = 0, height = 0 };
        windowInfo.SetAsChild(parent, &rect);
        cef_browser_settings_t settings = new()
        {
        };

        fixed (char* p = url)
        {
            cef_string_t u = new cef_string_t() { str = p, length = (nuint)url.Length, };
            var result = cef_browser_host_create_browser(&windowInfo, client.GetRef(), &u, &settings, null, null);
            Debug.WriteLine($"cef_browser_host_create_browser = {result}");
            return result;
        }
    }

    public unsafe static int RunAsSubProcess()
    {
        cef_main_args_t mainArgs = new()
        {
            instance = kernel32.GetModuleHandleW(null)
        };

        //Debug.WriteLine("cef_execute_process");
        return cef_execute_process(&mainArgs, null, IntPtr.Zero);
    }

#if DEBUG
    public unsafe static void ShutdownCloseBrowser()
    {
        Debug.WriteLine($"Cef|ShutdownCloseBrowser");
        Task.WhenAny([CheckBrowserRefs(), Task.Delay(2000)]).Wait();

        var browserRefs = _BrowsrDict.Values.ToArray();
        Debug.WriteLine($"Cef|ShutdownCloseBrowser ref count={browserRefs.Length}");
        if (browserRefs.Length > 0)
        {
            foreach (var browserRef in browserRefs)
            {
                if (browserRef.IsValid)
                {
                    using var host = browserRef.GetHost();
                    host.Ptr->close_browser(host.Ptr, 1);
                }
                browserRef.Dispose();
            }
            Task.Delay(2000).Wait();
        }

        cef_shutdown();
    }

    static async Task CheckBrowserRefs()
    {
        var browserRefs = _BrowsrDict.Values.ToArray();
        while (browserRefs.Length > 0)
        {
            await Task.Delay(100);
            browserRefs = _BrowsrDict.Values.ToArray();
        }

        Debug.WriteLine($"CheckBrowserRefs done");
    }

    internal unsafe static void AddBrowserRef(RefCefBrowser refCefBrowser)
    {
        _BrowsrDict.TryAdd((nint)refCefBrowser.Ptr, refCefBrowser);

    }

    internal unsafe static void RemoveBrowserRef(RefCefBrowser refCefBrowser)
    {
        _BrowsrDict.TryRemove((nint)refCefBrowser.Ptr, out var browserRef);
    }
#endif
}

