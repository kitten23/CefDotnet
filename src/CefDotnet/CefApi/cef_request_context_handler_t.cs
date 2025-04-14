using CefDotnet.CefApi.Types;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// Implement this structure to provide handler implementations. The handler
/// instance will not be released until all objects related to the context have
/// been destroyed.
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_request_context_handler_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Called on the browser process UI thread immediately after the request
    /// context has been initialized.
    ///
    public delegate* unmanaged<cef_request_context_handler_t*, cef_request_context_t*, void> on_request_context_initialized;

    ///
    /// Called on the browser process IO thread before a resource request is
    /// initiated. The |browser| and |frame| values represent the source of the
    /// request, and may be NULL for requests originating from service workers or
    /// cef_urlrequest_t. |request| represents the request contents and cannot be
    /// modified in this callback. |is_navigation| will be true (1) if the
    /// resource request is a navigation. |is_download| will be true (1) if the
    /// resource request is a download. |request_initiator| is the origin (scheme
    /// + domain) of the page that initiated the request. Set
    /// |disable_default_handling| to true (1) to disable default handling of the
    /// request, in which case it will need to be handled via
    /// cef_resource_request_handler_t::GetResourceHandler or it will be canceled.
    /// To allow the resource load to proceed with default handling return NULL.
    /// To specify a handler for the resource return a
    /// cef_resource_request_handler_t object. This function will not be called if
    /// the client associated with |browser| returns a non-NULL value from
    /// cef_request_handler_t::GetResourceRequestHandler for the same request
    /// (identified by cef_request_t::GetIdentifier).
    ///
    public delegate* unmanaged<cef_request_context_handler_t*, cef_browser_t*, cef_frame_t*, cef_request_t*, int, int, cef_string_t*, int*, cef_resource_request_handler_t*> on_before_resource_load;    
}
