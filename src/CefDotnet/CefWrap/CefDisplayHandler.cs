using CefDotnet.CefApi;
using CefDotnet.CefApi.Internal;
using CefDotnet.CefApi.Types;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CefDotnet.CefWrap;

unsafe public class CefDisplayHandler : CefBaseRefCounted<cef_display_handler_t>
{
    public CefDisplayHandler()
    {
        _on_address_change = (cef_display_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_string_t* url) =>
        {
            //Debug.WriteLine($"CefDisplayHandler|on_address_change, url={CefString.FromCefString(url)},");            
            Ref.Release(browser);
            Ref.Release(frame);
        };
        _on_title_change = (cef_display_handler_t* self, cef_browser_t* browser, cef_string_t* title) =>
        {
            //Debug.WriteLine("CefDisplayHandler|on_title_change");
            OnTitleChange(browser, CefString.FromCefString(title));
            Ref.Release(browser);
        };
        _on_favicon_urlchange = (cef_display_handler_t* self, cef_browser_t* browser, nint iconUrls) =>
        {
            //Debug.WriteLine("CefDisplayHandler|on_favicon_url_change");
            OnFaviconUrlChange(browser, CefString.FromCefStringList(iconUrls));
            Ref.Release(browser);
        };
        _on_fullscreen_mode_change = (cef_display_handler_t* self, cef_browser_t* browser, int fullscreen) =>
        {
            Debug.WriteLine("CefDisplayHandler|on_fullscreen_mode_change");
            Ref.Release(browser);
        };
        _on_tooltip = (cef_display_handler_t* self, cef_browser_t* browser, cef_string_t* text) =>
        {
            //Debug.WriteLine("CefDisplayHandler|on_tooltip");
            Ref.Release(browser);
            return 0;
        };
        _on_status_message = (cef_display_handler_t* self, cef_browser_t* browser, cef_string_t* value) =>
        {
            //Debug.WriteLine($"CefDisplayHandler|on_status_message : {CefString.FromCefString(value)}");
            Ref.Release(browser);
        };
        _on_console_message = (cef_display_handler_t* self, cef_browser_t* browser, cef_log_severity_t level, cef_string_t* message, cef_string_t* source, int line) =>
        {
            //Debug.WriteLine($"CefDisplayHandler|on_console_message {cef_string_t.ToString(message)}");
            Ref.Release(browser);
            return 0;
        };
        _on_auto_resize = (cef_display_handler_t* self, cef_browser_t* browser, cef_size_t* new_size) =>
        {
            Debug.WriteLine("CefDisplayHandler|on_auto_resize");
            Ref.Release(browser);
            return 0;
        };
        _on_loading_progress_change = (cef_display_handler_t* self, cef_browser_t* browser, double progress) =>
        {
            //Debug.WriteLine("CefDisplayHandler|on_loading_progress_change");
            Ref.Release(browser);
        };
        _on_cursor_change = (cef_display_handler_t* self, cef_browser_t* browser, nint cursor, cef_cursor_type_t type, cef_cursor_info_t* custom_cursor_info) =>
        {
            //Debug.WriteLine("CefDisplayHandler|on_cursor_change");
            Ref.Release(browser);
            return 0;
        };
        _on_media_access_change = (cef_display_handler_t* self, cef_browser_t* browser, int has_video_access, int has_audio_access) =>
        {
            Debug.WriteLine("CefDisplayHandler|on_media_access_change");
            Ref.Release(browser);
        };

        cef_display_handler_t* p = Ptr;
        p->on_address_change = (delegate* unmanaged<cef_display_handler_t*, cef_browser_t*, cef_frame_t*, cef_string_t*, void>)Marshal.GetFunctionPointerForDelegate(_on_address_change);
        p->on_title_change = (delegate* unmanaged<cef_display_handler_t*, cef_browser_t*, cef_string_t*, void>)Marshal.GetFunctionPointerForDelegate(_on_title_change);
        p->on_favicon_urlchange = (delegate* unmanaged<cef_display_handler_t*, cef_browser_t*, nint, void>)Marshal.GetFunctionPointerForDelegate(_on_favicon_urlchange);
        p->on_fullscreen_mode_change = (delegate* unmanaged<cef_display_handler_t*, cef_browser_t*, int, void>)Marshal.GetFunctionPointerForDelegate(_on_fullscreen_mode_change);
        p->on_tooltip = (delegate* unmanaged<cef_display_handler_t*, cef_browser_t*, cef_string_t*, int>)Marshal.GetFunctionPointerForDelegate(_on_tooltip);
        p->on_status_message = (delegate* unmanaged<cef_display_handler_t*, cef_browser_t*, cef_string_t*, void>)Marshal.GetFunctionPointerForDelegate(_on_status_message);
        p->on_console_message = (delegate* unmanaged<cef_display_handler_t*, cef_browser_t*, cef_log_severity_t, cef_string_t*, cef_string_t*, int, int>)Marshal.GetFunctionPointerForDelegate(_on_console_message);
        p->on_auto_resize = (delegate* unmanaged<cef_display_handler_t*, cef_browser_t*, cef_size_t*, int>)Marshal.GetFunctionPointerForDelegate(_on_auto_resize);
        p->on_loading_progress_change = (delegate* unmanaged<cef_display_handler_t*, cef_browser_t*, double, void>)Marshal.GetFunctionPointerForDelegate(_on_loading_progress_change);
        p->on_cursor_change = (delegate* unmanaged<cef_display_handler_t*, cef_browser_t*, nint, cef_cursor_type_t, cef_cursor_info_t*, int>)Marshal.GetFunctionPointerForDelegate(_on_cursor_change);
        p->on_media_access_change = (delegate* unmanaged<cef_display_handler_t*, cef_browser_t*, int, int, void>)Marshal.GetFunctionPointerForDelegate(_on_media_access_change);
    }

    public delegate void DelegateOnTitleChange(cef_browser_t* browser, string title);
    public delegate void DelegateOnFaviconUrlChange(cef_browser_t* browser, string[] urls);
    public DelegateOnTitleChange OnTitleChange { get; set; } = (_, _) => { };
    public DelegateOnFaviconUrlChange OnFaviconUrlChange { get; set; } = (_, _) => { };

    OnAddressChange _on_address_change;
    on_title_change _on_title_change;
    on_favicon_urlchange _on_favicon_urlchange;
    OnFullscreenModeChange _on_fullscreen_mode_change;
    OnTooltip _on_tooltip;
    OnStatusMessage _on_status_message;
    OnConsoleMessage _on_console_message;
    OnAutoResize _on_auto_resize;
    OnLoadingProgressChange _on_loading_progress_change;
    OnCursorChange _on_cursor_change;
    OnMediaAccessChange _on_media_access_change;

    public delegate void OnAddressChange(cef_display_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_string_t* url);
    public delegate void on_title_change(cef_display_handler_t* self, cef_browser_t* browser, cef_string_t* title);
    public delegate void on_favicon_urlchange(cef_display_handler_t* self, cef_browser_t* browser, nint iconUrls);
    public delegate void OnFullscreenModeChange(cef_display_handler_t* self, cef_browser_t* browser, int fullscreen);
    public delegate int OnTooltip(cef_display_handler_t* self, cef_browser_t* browser, cef_string_t* text);
    public delegate void OnStatusMessage(cef_display_handler_t* self, cef_browser_t* browser, cef_string_t* value);
    public delegate int OnConsoleMessage(cef_display_handler_t* self, cef_browser_t* browser, cef_log_severity_t level, cef_string_t* message, cef_string_t* source, int line);
    public delegate int OnAutoResize(cef_display_handler_t* self, cef_browser_t* browser, cef_size_t* new_size);
    public delegate void OnLoadingProgressChange(cef_display_handler_t* self, cef_browser_t* browser, double progress);
    public delegate int OnCursorChange(cef_display_handler_t* self, cef_browser_t* browser, nint cursor, cef_cursor_type_t type, cef_cursor_info_t* custom_cursor_info);
    public delegate void OnMediaAccessChange(cef_display_handler_t* self, cef_browser_t* browser, int has_video_access, int has_audio_access);
}
