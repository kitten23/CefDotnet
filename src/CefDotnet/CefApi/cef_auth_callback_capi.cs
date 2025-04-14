using CefDotnet.CefApi.Types;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;
///
/// Callback structure used for asynchronous continuation of authentication
/// requests.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_auth_callback_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Continue the authentication request.
    ///
    public delegate* unmanaged<cef_auth_callback_t*, cef_string_t*, cef_string_t*, void> cont;

    ///
    /// Cancel the authentication request.
    ///
    public delegate* unmanaged<cef_auth_callback_t*, void> cancel;
}
