using CefDotnet.CefApi;
using CefDotnet.CefApi.Types;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CefDotnet.CefWrap;

unsafe public class CefLoadHandler : CefBaseRefCounted<cef_load_handler_t>
{
    public CefLoadHandler()
    {
        on_loading_state_change = (cef_load_handler_t* self, cef_browser_t* browser, int isLoading, int canGoBack, int canGoForward) =>
        {
            //Debug.WriteLine("CefLoadHandler|on_loading_state_change");
            Ref.Release(browser);
        };
        on_load_start = (cef_load_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_transition_type_t transition_type) =>
        {
            //Debug.WriteLine("CefLoadHandler|on_load_start");
            Ref.Release(browser);
            Ref.Release(frame);
        };
        on_load_end = (cef_load_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, int httpStatusCode) =>
        {
            //Debug.WriteLine("CefLoadHandler|on_load_end");
            Ref.Release(browser);
            Ref.Release(frame);
        };
        on_load_error = (cef_load_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, uint errorCode, cef_string_t* errorText, cef_string_t* failedUrl) =>
        {
            Debug.WriteLine($"CefLoadHandler|on_load_error errorCode={errorCode} txt={CefString.FromCefString(errorText)}");
            Ref.Release(browser);
            Ref.Release(frame);
        };

        cef_load_handler_t* p = Ptr;
        p->on_loading_state_change = (delegate* unmanaged<cef_load_handler_t*, cef_browser_t*, int, int, int, void>)Marshal.GetFunctionPointerForDelegate(on_loading_state_change);
        p->on_load_start = (delegate* unmanaged<cef_load_handler_t*, cef_browser_t*, cef_frame_t*, cef_transition_type_t, void>)Marshal.GetFunctionPointerForDelegate(on_load_start);
        p->on_load_end = (delegate* unmanaged<cef_load_handler_t*, cef_browser_t*, cef_frame_t*, int, void>)Marshal.GetFunctionPointerForDelegate(on_load_end);
        p->on_load_error = (delegate* unmanaged<cef_load_handler_t*, cef_browser_t*, cef_frame_t*, cef_errorcode_t, cef_string_t*, cef_string_t*, void>)Marshal.GetFunctionPointerForDelegate(on_load_error);
    }

    OnLoadingStateChange on_loading_state_change;
    OnLoadStart on_load_start;
    OnLoadEnd on_load_end;
    OnLoadError on_load_error;

    public delegate void OnLoadingStateChange(cef_load_handler_t* self, cef_browser_t* browser, int isLoading, int canGoBack, int canGoForward);
    public delegate void OnLoadStart(cef_load_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_transition_type_t transition_type);
    public delegate void OnLoadEnd(cef_load_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, int httpStatusCode);
    public delegate void OnLoadError(cef_load_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, uint errorCode, cef_string_t* errorText, cef_string_t* failedUrl);
}
