using CefDotnet.CefApi.Types;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// Return value types.
///
public enum cef_return_value_t
{
    ///
    /// Cancel immediately.
    ///
    RV_CANCEL = 0,

    ///
    /// Continue immediately.
    ///
    RV_CONTINUE,

    ///
    /// Continue asynchronously (usually via a callback).
    ///
    RV_CONTINUE_ASYNC,
}

///
/// Implement this structure to handle events related to browser requests. The
/// functions of this structure will be called on the IO thread unless otherwise
/// indicated.
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_resource_request_handler_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Called on the IO thread before a resource request is loaded. The |browser|
    /// and |frame| values represent the source of the request, and may be NULL
    /// for requests originating from service workers or cef_urlrequest_t. To
    /// optionally filter cookies for the request return a
    /// cef_cookie_access_filter_t object. The |request| object cannot not be
    /// modified in this callback.
    ///
    public delegate* unmanaged<cef_resource_request_handler_t*, cef_browser_t*, cef_frame_t*, cef_request_t*, cef_cookie_access_filter_t*> get_cookie_access_filter;

    ///
    /// Called on the IO thread before a resource request is loaded. The |browser|
    /// and |frame| values represent the source of the request, and may be NULL
    /// for requests originating from service workers or cef_urlrequest_t. To
    /// redirect or change the resource load optionally modify |request|.
    /// Modification of the request URL will be treated as a redirect. Return
    /// RV_CONTINUE to continue the request immediately. Return RV_CONTINUE_ASYNC
    /// and call cef_callback_t functions at a later time to continue or cancel
    /// the request asynchronously. Return RV_CANCEL to cancel the request
    /// immediately.
    ///
    public delegate* unmanaged<cef_resource_request_handler_t*, cef_browser_t*, cef_frame_t*, cef_request_t*, cef_callback_t*, cef_return_value_t> on_before_resource_load;

    ///
    /// Called on the IO thread before a resource is loaded. The |browser| and
    /// |frame| values represent the source of the request, and may be NULL for
    /// requests originating from service workers or cef_urlrequest_t. To allow
    /// the resource to load using the default network loader return NULL. To
    /// specify a handler for the resource return a cef_resource_handler_t object.
    /// The |request| object cannot not be modified in this callback.
    ///
    public delegate* unmanaged<cef_resource_request_handler_t*, cef_browser_t*, cef_frame_t*, cef_request_t*, cef_resource_handler_t*> get_resource_handler;

    ///
    /// Called on the IO thread when a resource load is redirected. The |browser|
    /// and |frame| values represent the source of the request, and may be NULL
    /// for requests originating from service workers or cef_urlrequest_t. The
    /// |request| parameter will contain the old URL and other request-related
    /// information. The |response| parameter will contain the response that
    /// resulted in the redirect. The |new_url| parameter will contain the new URL
    /// and can be changed if desired. The |request| and |response| objects cannot
    /// be modified in this callback.
    ///
    public delegate* unmanaged<cef_resource_request_handler_t*, cef_browser_t*, cef_frame_t*, cef_request_t*, cef_response_t*, cef_string_t*, void> on_resource_redirect;

    ///
    /// Called on the IO thread when a resource response is received. The
    /// |browser| and |frame| values represent the source of the request, and may
    /// be NULL for requests originating from service workers or cef_urlrequest_t.
    /// To allow the resource load to proceed without modification return false
    /// (0). To redirect or retry the resource load optionally modify |request|
    /// and return true (1). Modification of the request URL will be treated as a
    /// redirect. Requests handled using the default network loader cannot be
    /// redirected in this callback. The |response| object cannot be modified in
    /// this callback.
    ///
    /// WARNING: Redirecting using this function is deprecated. Use
    /// OnBeforeResourceLoad or GetResourceHandler to perform redirects.
    ///
    public delegate* unmanaged<cef_resource_request_handler_t*, cef_browser_t*, cef_frame_t*, cef_request_t*, cef_response_t*, int> on_resource_response;

    ///
    /// Called on the IO thread to optionally filter resource response content.
    /// The |browser| and |frame| values represent the source of the request, and
    /// may be NULL for requests originating from service workers or
    /// cef_urlrequest_t. |request| and |response| represent the request and
    /// response respectively and cannot be modified in this callback.
    ///
    public delegate* unmanaged<cef_resource_request_handler_t*, cef_browser_t*, cef_frame_t*, cef_request_t*, cef_response_t*, cef_response_filter_t*> get_resource_response_filter;

    ///
    /// Called on the IO thread when a resource load has completed. The |browser|
    /// and |frame| values represent the source of the request, and may be NULL
    /// for requests originating from service workers or cef_urlrequest_t.
    /// |request| and |response| represent the request and response respectively
    /// and cannot be modified in this callback. |status| indicates the load
    /// completion status. |received_content_length| is the number of response
    /// bytes actually read. This function will be called for all requests,
    /// including requests that are aborted due to CEF shutdown or destruction of
    /// the associated browser. In cases where the associated browser is destroyed
    /// this callback may arrive after the cef_life_span_handler_t::OnBeforeClose
    /// callback for that browser. The cef_frame_t::IsValid function can be used
    /// to test for this situation, and care should be taken not to call |browser|
    /// or |frame| functions that modify state (like LoadURL, SendProcessMessage,
    /// etc.) if the frame is invalid.
    ///
    public delegate* unmanaged<cef_resource_request_handler_t*, cef_browser_t*, cef_frame_t*, cef_request_t*, cef_response_t*, cef_urlrequest_status_t, long, void> on_resource_load_complete;

    ///
    /// Called on the IO thread to handle requests for URLs with an unknown
    /// protocol component. The |browser| and |frame| values represent the source
    /// of the request, and may be NULL for requests originating from service
    /// workers or cef_urlrequest_t. |request| cannot be modified in this
    /// callback. Set |allow_os_execution| to true (1) to attempt execution via
    /// the registered OS protocol handler, if any. SECURITY WARNING: YOU SHOULD
    /// USE THIS METHOD TO ENFORCE RESTRICTIONS BASED ON SCHEME, HOST OR OTHER URL
    /// ANALYSIS BEFORE ALLOWING OS EXECUTION.
    ///
    public delegate* unmanaged<cef_resource_request_handler_t*, cef_browser_t*, cef_frame_t*, cef_request_t*, int*, void> on_protocol_execution;
}

///
/// Implement this structure to filter cookies that may be sent or received from
/// resource requests. The functions of this structure will be called on the IO
/// thread unless otherwise indicated.
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_cookie_access_filter_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Called on the IO thread before a resource request is sent. The |browser|
    /// and |frame| values represent the source of the request, and may be NULL
    /// for requests originating from service workers or cef_urlrequest_t.
    /// |request| cannot be modified in this callback. Return true (1) if the
    /// specified cookie can be sent with the request or false (0) otherwise.
    ///
    public delegate* unmanaged<cef_cookie_access_filter_t*, cef_browser_t*, cef_frame_t*, cef_request_t*, cef_cookie_t*, int> can_send_cookie;

    ///
    /// Called on the IO thread after a resource response is received. The
    /// |browser| and |frame| values represent the source of the request, and may
    /// be NULL for requests originating from service workers or cef_urlrequest_t.
    /// |request| cannot be modified in this callback. Return true (1) if the
    /// specified cookie returned with the response can be saved or false (0)
    /// otherwise.
    ///
    public delegate* unmanaged<cef_cookie_access_filter_t*, cef_browser_t*, cef_frame_t*, cef_request_t*, cef_response_t*, cef_cookie_t*, int> can_save_cookie;
}
