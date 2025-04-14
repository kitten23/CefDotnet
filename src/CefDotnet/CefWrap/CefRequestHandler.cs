using CefDotnet.CefApi;
using CefDotnet.CefApi.Internal;
using CefDotnet.CefApi.Types;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CefDotnet.CefWrap;

unsafe public class CefRequestHandler : CefBaseRefCounted<cef_request_handler_t>
{
    public CefRequestHandler()
    {
        CefResourceRequestHandler = new CefResourceRequestHandler();

        _on_before_browse = (cef_request_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_request_t* request, int user_gesture, int is_redirect) =>
        {
            Debug.WriteLine($"CefRequestHandler|_on_before_browse url={CefString.FromCefString(request->get_url(request))}");
            Ref.Release(browser);
            Ref.Release(frame);
            Ref.Release(request);
            return 0;
        };
        _on_open_urlfrom_tab = (cef_request_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_string_t* target_url, cef_window_open_disposition_t target_disposition, int user_gesture) =>
        {
            Debug.WriteLine($"CefRequestHandler|_on_open_urlfrom_tab");
            Ref.Release(browser);
            Ref.Release(frame);
            return 0;
        };
        _get_resource_request_handler = (cef_request_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_request_t* request, int is_navigation, int is_download, cef_string_t* request_initiator, int* disable_default_handling) =>
        {
            Ref.Release(browser);
            Ref.Release(frame);
            Ref.Release(request);
            return CefResourceRequestHandler.GetRef();
        };
        _get_auth_credentials = (cef_request_handler_t* self, cef_browser_t* browser, cef_string_t* origin_url, int isProxy, cef_string_t* host, int port, cef_string_t* realm, cef_string_t* scheme, cef_auth_callback_t* callback) =>
        {
            Debug.WriteLine($"CefRequestHandler|_get_auth_credentials");
            Ref.Release(browser);
            Ref.Release(callback);
            return 0;
        };
        _on_certificate_error = (cef_request_handler_t* self, cef_browser_t* browser, cef_errorcode_t cert_error, cef_string_t* request_url, cef_sslinfo_t* ssl_info, cef_callback_t* callback) =>
        {
            Debug.WriteLine($"CefRequestHandler|_on_certificate_error");
            Ref.Release(browser);
            Ref.Release(ssl_info);
            Ref.Release(callback);
            return 0;
        };
        _on_select_client_certificate = (cef_request_handler_t* self, cef_browser_t* browser, int isProxy, cef_string_t* host, int port, nuint certificatesCount, cef_x509_certificate_t** certificates, cef_select_client_certificate_callback_t* callback) =>
        {
            Debug.WriteLine($"CefRequestHandler|_on_select_client_certificate");
            Ref.Release(browser);
            Ref.Release(callback);
            return 0;
        };
        _on_render_view_ready = (cef_request_handler_t* self, cef_browser_t* browser) =>
        {
            //Debug.WriteLine($"CefRequestHandler|_on_render_view_ready");
            Ref.Release(browser);
        };
        _on_render_process_unresponsive = (cef_request_handler_t* self, cef_browser_t* browser, cef_unresponsive_process_callback_t* callback) =>
        {
            Debug.WriteLine($"CefRequestHandler|_on_render_process_unresponsive");
            Ref.Release(browser);
            Ref.Release(callback);
            return 0;
        };
        _on_render_process_responsive = (cef_request_handler_t* self, cef_browser_t* browser) =>
        {
            Debug.WriteLine($"CefRequestHandler|_on_render_process_responsive");
            Ref.Release(browser);
        };
        _on_render_process_terminated = (cef_request_handler_t* self, cef_browser_t* browser, cef_termination_status_t status, int error_code, cef_string_t* error_string) =>
        {
            Debug.WriteLine($"CefRequestHandler|_on_render_process_terminated");
            Ref.Release(browser);
        };
        _on_document_available_in_main_frame = (cef_request_handler_t* self, cef_browser_t* browser) =>
        {
            //Debug.WriteLine($"CefRequestHandler|_on_document_available_in_main_frame");
            Ref.Release(browser);
        };

        cef_request_handler_t* p = Ptr;
        p->on_before_browse = (delegate* unmanaged<cef_request_handler_t*, cef_browser_t*, cef_frame_t*, cef_request_t*, int, int, int>)Marshal.GetFunctionPointerForDelegate(_on_before_browse);
        p->on_open_urlfrom_tab = (delegate* unmanaged<cef_request_handler_t*, cef_browser_t*, cef_frame_t*, cef_string_t*, cef_window_open_disposition_t, int, int>)Marshal.GetFunctionPointerForDelegate(_on_open_urlfrom_tab);
        p->get_resource_request_handler = (delegate* unmanaged<cef_request_handler_t*, cef_browser_t*, cef_frame_t*, cef_request_t*, int, int, cef_string_t*, int*, cef_resource_request_handler_t*>)Marshal.GetFunctionPointerForDelegate(_get_resource_request_handler);
        p->get_auth_credentials = (delegate* unmanaged<cef_request_handler_t*, cef_browser_t*, cef_string_t*, int, cef_string_t*, int, cef_string_t*, cef_string_t*, cef_auth_callback_t*, int>)Marshal.GetFunctionPointerForDelegate(_get_auth_credentials);
        p->on_certificate_error = (delegate* unmanaged<cef_request_handler_t*, cef_browser_t*, cef_errorcode_t, cef_string_t*, cef_sslinfo_t*, cef_callback_t*, int>)Marshal.GetFunctionPointerForDelegate(_on_certificate_error);
        p->on_select_client_certificate = (delegate* unmanaged<cef_request_handler_t*, cef_browser_t*, int, cef_string_t*, int, nuint, cef_x509_certificate_t**, cef_select_client_certificate_callback_t*, int>)Marshal.GetFunctionPointerForDelegate(_on_select_client_certificate);
        p->on_render_view_ready = (delegate* unmanaged<cef_request_handler_t*, cef_browser_t*, void>)Marshal.GetFunctionPointerForDelegate(_on_render_view_ready);
        p->on_render_process_unresponsive = (delegate* unmanaged<cef_request_handler_t*, cef_browser_t*, cef_unresponsive_process_callback_t*, int>)Marshal.GetFunctionPointerForDelegate(_on_render_process_unresponsive);
        p->on_render_process_responsive = (delegate* unmanaged<cef_request_handler_t*, cef_browser_t*, void>)Marshal.GetFunctionPointerForDelegate(_on_render_process_responsive);
        p->on_render_process_terminated = (delegate* unmanaged<cef_request_handler_t*, cef_browser_t*, cef_termination_status_t, int, cef_string_t*, void>)Marshal.GetFunctionPointerForDelegate(_on_render_process_terminated);
        p->on_document_available_in_main_frame = (delegate* unmanaged<cef_request_handler_t*, cef_browser_t*, void>)Marshal.GetFunctionPointerForDelegate(_on_document_available_in_main_frame);
    }

    public CefResourceRequestHandler CefResourceRequestHandler { get; private set; }

    on_before_browse _on_before_browse;
    on_open_urlfrom_tab _on_open_urlfrom_tab;
    get_resource_request_handler _get_resource_request_handler;
    get_auth_credentials _get_auth_credentials;
    on_certificate_error _on_certificate_error;
    on_select_client_certificate _on_select_client_certificate;
    on_render_view_ready _on_render_view_ready;
    on_render_process_unresponsive _on_render_process_unresponsive;
    on_render_process_responsive _on_render_process_responsive;
    on_render_process_terminated _on_render_process_terminated;
    on_document_available_in_main_frame _on_document_available_in_main_frame;

    public delegate int on_before_browse(cef_request_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_request_t* request, int user_gesture, int is_redirect);
    public delegate int on_open_urlfrom_tab(cef_request_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_string_t* target_url, cef_window_open_disposition_t target_disposition, int user_gesture);
    public delegate cef_resource_request_handler_t* get_resource_request_handler(cef_request_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_request_t* request, int is_navigation, int is_download, cef_string_t* request_initiator, int* disable_default_handling);
    public delegate int get_auth_credentials(cef_request_handler_t* self, cef_browser_t* browser, cef_string_t* origin_url, int isProxy, cef_string_t* host, int port, cef_string_t* realm, cef_string_t* scheme, cef_auth_callback_t* callback);
    public delegate int on_certificate_error(cef_request_handler_t* self, cef_browser_t* browser, cef_errorcode_t cert_error, cef_string_t* request_url, cef_sslinfo_t* ssl_info, cef_callback_t* callback);
    public delegate int on_select_client_certificate(cef_request_handler_t* self, cef_browser_t* browser, int isProxy, cef_string_t* host, int port, nuint certificatesCount, cef_x509_certificate_t** certificates, cef_select_client_certificate_callback_t* callback);
    public delegate void on_render_view_ready(cef_request_handler_t* self, cef_browser_t* browser);
    public delegate int on_render_process_unresponsive(cef_request_handler_t* self, cef_browser_t* browser, cef_unresponsive_process_callback_t* callback);
    public delegate void on_render_process_responsive(cef_request_handler_t* self, cef_browser_t* browser);
    public delegate void on_render_process_terminated(cef_request_handler_t* self, cef_browser_t* browser, cef_termination_status_t status, int error_code, cef_string_t* error_string);
    public delegate void on_document_available_in_main_frame(cef_request_handler_t* self, cef_browser_t* browser);
}
