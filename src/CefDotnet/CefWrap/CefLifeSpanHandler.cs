using CefDotnet.CefApi;
using CefDotnet.CefApi.Internal;
using CefDotnet.CefApi.Types;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CefDotnet.CefWrap;

unsafe public class CefLifeSpanHandler : CefBaseRefCounted<cef_life_span_handler_t>
{
    public CefLifeSpanHandler()
    {
        _on_before_popup = (
            cef_life_span_handler_t* self,
            cef_browser_t* browser,
            cef_frame_t* frame,
            int popup_id,
            cef_string_t* target_url,
            cef_string_t* target_frame_name,
            cef_window_open_disposition_t target_disposition,
            int user_gesture,
            nint popupFeatures,
            cef_window_info_t* windowInfo,
            cef_client_t** client,
            cef_browser_settings_t* settings,
            nint extra_info,
            int* no_javascript_access) =>
        {
            Debug.WriteLine("CefLifeSpanHandler|_on_before_popup");
            var url = CefString.FromCefString(target_url);
            var frame_name = CefString.FromCefString(target_frame_name);
            frame->load_url(frame, target_url);
            Ref.Release(browser);
            Ref.Release(frame);
            Ref.Release(*client);
            return 1;
        };
        _on_before_popup_aborted = (cef_life_span_handler_t* self, cef_browser_t* browser, int popup_id) =>
        {
            Debug.WriteLine("CefLifeSpanHandler|_on_before_popup_aborted");
            Ref.Release(browser);
        };
        _on_before_dev_tools_popup = (
            cef_life_span_handler_t* self,
            cef_browser_t* browser,
            cef_window_info_t* windowInfo,
            cef_client_t** client,
            cef_browser_settings_t* settings,
            nint extra_info,
            int* use_default_window) =>
        {//TODO client引用计数，寻找已有的CefClient封装类
            Debug.WriteLine("CefLifeSpanHandler|_on_before_dev_tools_popup");
            Ref.Release(browser);
            Ref.Release(*client);
        };
        _on_after_created = (cef_life_span_handler_t* self, cef_browser_t* browser) =>
        {
            Debug.WriteLine("CefLifeSpanHandler|_on_after_created");
            OnAfterCreated(browser);
            Ref.Release(browser);
        };
        _do_close = (cef_life_span_handler_t* self, cef_browser_t* browser) =>
        {
            Debug.WriteLine("CefLifeSpanHandler|_do_close");
            Ref.Release(browser);
            return 0;
        };
        _on_before_close = (cef_life_span_handler_t* self, cef_browser_t* browser) =>
        {
            Debug.WriteLine("CefLifeSpanHandler|_on_before_close");
            OnBeforeClose(browser);
            Ref.Release(browser);
        };

        cef_life_span_handler_t* p = Ptr;
        p->on_before_popup = (delegate* unmanaged<cef_life_span_handler_t*, cef_browser_t*, cef_frame_t*, int, cef_string_t*, cef_string_t*, cef_window_open_disposition_t, int, cef_popup_features_t*, cef_window_info_t*, cef_client_t**, cef_browser_settings_t*, cef_dictionary_value_t**, int*, int>)Marshal.GetFunctionPointerForDelegate(_on_before_popup);
        p->on_before_popup_aborted = (delegate* unmanaged<cef_life_span_handler_t*, cef_browser_t*, int, void>)Marshal.GetFunctionPointerForDelegate(_on_before_popup_aborted);
        p->on_before_dev_tools_popup = (delegate* unmanaged<cef_life_span_handler_t*, cef_browser_t*, cef_window_info_t*, cef_client_t**, cef_browser_settings_t*, cef_dictionary_value_t**, int*, void>)Marshal.GetFunctionPointerForDelegate(_on_before_dev_tools_popup);
        p->on_after_created = (delegate* unmanaged<cef_life_span_handler_t*, cef_browser_t*, void>)Marshal.GetFunctionPointerForDelegate(_on_after_created);
        p->do_close = (delegate* unmanaged<cef_life_span_handler_t*, cef_browser_t*, int>)Marshal.GetFunctionPointerForDelegate(_do_close);
        p->on_before_close = (delegate* unmanaged<cef_life_span_handler_t*, cef_browser_t*, void>)Marshal.GetFunctionPointerForDelegate(_on_before_close);
    }

    public delegate void DelegateOnAfterCreated(cef_browser_t* browser);
    public delegate void DelegateOnBeforeClose(cef_browser_t* browser);

    public DelegateOnAfterCreated OnAfterCreated { get; set; } = (_) => { };
    public DelegateOnBeforeClose OnBeforeClose { get; set; } = (_) => { };

    on_before_popup _on_before_popup;
    on_before_popup_aborted _on_before_popup_aborted;
    on_before_dev_tools_popup _on_before_dev_tools_popup;
    internal on_after_created _on_after_created;
    do_close _do_close;
    internal on_before_close _on_before_close;

    public delegate int on_before_popup(
        cef_life_span_handler_t* self,
        cef_browser_t* browser,
        cef_frame_t* frame,
        int popup_id,
        cef_string_t* target_url,
        cef_string_t* target_frame_name,
        cef_window_open_disposition_t target_disposition,
        int user_gesture,
        nint popupFeatures,
        cef_window_info_t* windowInfo,
        cef_client_t** client,
        cef_browser_settings_t* settings,
        nint extra_info,
        int* no_javascript_access);
    public delegate void on_before_popup_aborted(cef_life_span_handler_t* self, cef_browser_t* browser, int popup_id);
    public delegate void on_before_dev_tools_popup(cef_life_span_handler_t* self, cef_browser_t* browser, cef_window_info_t* windowInfo, cef_client_t** client, cef_browser_settings_t* settings, nint extra_info, int* use_default_window);
    public delegate void on_after_created(cef_life_span_handler_t* self, cef_browser_t* browser);
    public delegate int do_close(cef_life_span_handler_t* self, cef_browser_t* browser);
    public delegate void on_before_close(cef_life_span_handler_t* self, cef_browser_t* browser);
}
