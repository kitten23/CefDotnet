using CefDotnet.CefApi.Types;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// Structure that manages custom scheme registrations.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_scheme_registrar_t
{
    ///
    /// Base structure.
    ///
    cef_base_scoped_t @base;

    ///
    /// Register a custom scheme. This function should not be called for the
    /// built-in HTTP, HTTPS, FILE, FTP, ABOUT and DATA schemes.
    ///
    /// See cef_scheme_options_t for possible values for |options|.
    ///
    /// This function may be called on any thread. It should only be called once
    /// per unique |scheme_name| value. If |scheme_name| is already registered or
    /// if an error occurs this function will return false (0).
    ///
    public delegate* unmanaged<cef_scheme_registrar_t*, cef_string_t*, int, int> add_custom_scheme;
}

///
/// Structure that creates cef_resource_handler_t instances for handling scheme
/// requests. The functions of this structure will always be called on the IO
/// thread.
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_scheme_handler_factory_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Return a new resource handler instance to handle the request or an NULL
    /// reference to allow default handling of the request. |browser| and |frame|
    /// will be the browser window and frame respectively that originated the
    /// request or NULL if the request did not originate from a browser window
    /// (for example, if the request came from cef_urlrequest_t). The |request|
    /// object passed to this function cannot be modified.
    ///
    public delegate* unmanaged<cef_scheme_handler_factory_t*, cef_browser_t*, cef_frame_t*, cef_string_t*, cef_request_t*, cef_resource_handler_t*> create;
}
