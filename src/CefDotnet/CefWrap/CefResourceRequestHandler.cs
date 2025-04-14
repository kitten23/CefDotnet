using CefDotnet.CefApi;
using CefDotnet.CefApi.Internal;
using CefDotnet.CefApi.Types;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CefDotnet.CefWrap;

unsafe public class CefResourceRequestHandler : CefBaseRefCounted<cef_resource_request_handler_t>
{
    public CefResourceRequestHandler()
    {
        _get_cookie_access_filter = (cef_resource_request_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_request_t* request) =>
        {
            //Debug.WriteLine("CefResourceRequestHandler|_get_cookie_access_filter");
            Ref.Release(browser);
            Ref.Release(frame);
            Ref.Release(request);
            return null;
        };
        _on_before_resource_load = (cef_resource_request_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_request_t* request, cef_callback_t* callback) =>
        {
            //Debug.WriteLine($"CefResourceRequestHandler|_on_before_resource_load {CefString.FromCefString(request->get_url(request))}");
            Ref.Release(browser);
            Ref.Release(frame);
            Ref.Release(request);
            Ref.Release(callback);
            return cef_return_value_t.RV_CONTINUE;
        };
        _get_resource_handler = (cef_resource_request_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_request_t* request) =>
        {
            //Debug.WriteLine("CefResourceRequestHandler|_get_resource_handler");
            Ref.Release(browser);
            Ref.Release(frame);
            Ref.Release(request);
            return null;
        };
        _on_resource_redirect = (cef_resource_request_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_request_t* request, cef_response_t* response, cef_string_t* new_url) =>
        {
            Debug.WriteLine("CefResourceRequestHandler|_on_resource_redirect");
            Ref.Release(browser);
            Ref.Release(frame);
            Ref.Release(request);
            Ref.Release(response);
        };
        _on_resource_response = (cef_resource_request_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_request_t* request, cef_response_t* response) =>
        {
            //Debug.WriteLine("CefResourceRequestHandler|_on_resource_response");
            Ref.Release(browser);
            Ref.Release(frame);
            Ref.Release(request);
            Ref.Release(response);
            return 0;
        };
        _get_resource_response_filter = (cef_resource_request_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_request_t* request, cef_response_t* response) =>
        {
            //Debug.WriteLine("CefResourceRequestHandler|_get_resource_response_filter");
            Ref.Release(browser);
            Ref.Release(frame);
            Ref.Release(request);
            Ref.Release(response);
            return null;
        };
        _on_resource_load_complete = (cef_resource_request_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_request_t* request, cef_response_t* response, cef_urlrequest_status_t status, long received_content_length) =>
        {
            //Debug.WriteLine("CefResourceRequestHandler|_on_resource_load_complete");
            Ref.Release(browser);
            Ref.Release(frame);
            Ref.Release(request);
            Ref.Release(response);
        };
        _on_protocol_execution = (cef_resource_request_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_request_t* request, int* allow_os_execution) =>
        {
            Debug.WriteLine("CefResourceRequestHandler|_on_protocol_execution");
            Ref.Release(browser);
            Ref.Release(frame);
            Ref.Release(request);
        };

        cef_resource_request_handler_t* p = Ptr;
        p->get_cookie_access_filter = (delegate* unmanaged<cef_resource_request_handler_t*, cef_browser_t*, cef_frame_t*, cef_request_t*, cef_cookie_access_filter_t*>)Marshal.GetFunctionPointerForDelegate(_get_cookie_access_filter);
        p->on_before_resource_load = (delegate* unmanaged<cef_resource_request_handler_t*, cef_browser_t*, cef_frame_t*, cef_request_t*, cef_callback_t*, cef_return_value_t>)Marshal.GetFunctionPointerForDelegate(_on_before_resource_load);
        p->get_resource_handler = (delegate* unmanaged<cef_resource_request_handler_t*, cef_browser_t*, cef_frame_t*, cef_request_t*, cef_resource_handler_t*>)Marshal.GetFunctionPointerForDelegate(_get_resource_handler);
        p->on_resource_redirect = (delegate* unmanaged<cef_resource_request_handler_t*, cef_browser_t*, cef_frame_t*, cef_request_t*, cef_response_t*, cef_string_t*, void>)Marshal.GetFunctionPointerForDelegate(_on_resource_redirect);
        p->on_resource_response = (delegate* unmanaged<cef_resource_request_handler_t*, cef_browser_t*, cef_frame_t*, cef_request_t*, cef_response_t*, int>)Marshal.GetFunctionPointerForDelegate(_on_resource_response);
        p->get_resource_response_filter = (delegate* unmanaged<cef_resource_request_handler_t*, cef_browser_t*, cef_frame_t*, cef_request_t*, cef_response_t*, cef_response_filter_t*>)Marshal.GetFunctionPointerForDelegate(_get_resource_response_filter);
        p->on_resource_load_complete = (delegate* unmanaged<cef_resource_request_handler_t*, cef_browser_t*, cef_frame_t*, cef_request_t*, cef_response_t*, cef_urlrequest_status_t, long, void>)Marshal.GetFunctionPointerForDelegate(_on_resource_load_complete);
        p->on_protocol_execution = (delegate* unmanaged<cef_resource_request_handler_t*, cef_browser_t*, cef_frame_t*, cef_request_t*, int*, void>)Marshal.GetFunctionPointerForDelegate(_on_protocol_execution);
    }

    get_cookie_access_filter _get_cookie_access_filter;
    on_before_resource_load _on_before_resource_load;
    get_resource_handler _get_resource_handler;
    on_resource_redirect _on_resource_redirect;
    on_resource_response _on_resource_response;
    get_resource_response_filter _get_resource_response_filter;
    on_resource_load_complete _on_resource_load_complete;
    on_protocol_execution _on_protocol_execution;

    public delegate cef_cookie_access_filter_t* get_cookie_access_filter(cef_resource_request_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_request_t* request);
    public delegate cef_return_value_t on_before_resource_load(cef_resource_request_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_request_t* request, cef_callback_t* callback);
    public delegate cef_resource_handler_t* get_resource_handler(cef_resource_request_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_request_t* request);
    public delegate void on_resource_redirect(cef_resource_request_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_request_t* request, cef_response_t* response, cef_string_t* new_url);
    public delegate int on_resource_response(cef_resource_request_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_request_t* request, cef_response_t* response);
    public delegate cef_response_filter_t* get_resource_response_filter(cef_resource_request_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_request_t* request, cef_response_t* response);
    public delegate void on_resource_load_complete(cef_resource_request_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_request_t* request, cef_response_t* response, cef_urlrequest_status_t status, long received_content_length);
    public delegate void on_protocol_execution(cef_resource_request_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_request_t* request, int* allow_os_execution);
}
