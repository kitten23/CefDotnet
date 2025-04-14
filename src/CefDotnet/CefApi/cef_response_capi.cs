using CefDotnet.CefApi.Types;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// Structure used to represent a web response. The functions of this structure
/// may be called on any thread.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_response_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

  ///
  /// Returns true (1) if this object is read-only.
  ///
  public delegate* unmanaged<cef_response_t*, int> is_read_only;

    ///
    /// Get the response error code. Returns ERR_NONE if there was no error.
    ///
    public delegate* unmanaged<cef_response_t*, cef_errorcode_t> get_error;

    ///
    /// Set the response error code. This can be used by custom scheme handlers to
    /// return errors during initial request processing.
    ///
    public delegate* unmanaged<cef_response_t*, cef_errorcode_t, void> set_error;

    ///
    /// Get the response status code.
    ///
    public delegate* unmanaged<cef_response_t*, int> get_status;

    ///
    /// Set the response status code.
    ///
    public delegate* unmanaged<cef_response_t*, int, void> set_status;

    ///
    /// Get the response status text.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_response_t*, cef_string_t*> get_status_text;

    ///
    /// Set the response status text.
    ///
    public delegate* unmanaged<cef_response_t*, cef_string_t*, void> set_status_text;

    ///
    /// Get the response mime type.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_response_t*, cef_string_t*> get_mime_type;

    ///
    /// Set the response mime type.
    ///
    public delegate* unmanaged<cef_response_t*, cef_string_t*, void> set_mime_type;

    ///
    /// Get the response charset.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_response_t*, cef_string_t*> get_charset;

    ///
    /// Set the response charset.
    ///
    public delegate* unmanaged<cef_response_t*, cef_string_t*, void> set_charset;

    ///
    /// Get the value for the specified response header field.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_response_t*, cef_string_t*, cef_string_t*> get_header_by_name;

    ///
    /// Set the header |name| to |value|. If |overwrite| is true (1) any existing
    /// values will be replaced with the new value. If |overwrite| is false (0)
    /// any existing values will not be overwritten.
    ///
    public delegate* unmanaged<cef_response_t*, cef_string_t*, cef_string_t*, int, void> set_header_by_name;

    ///
    /// Get all response header fields.
    ///
    public delegate* unmanaged<cef_response_t*, nint> get_header_map;

    ///
    /// Set all response header fields.
    ///
    public delegate* unmanaged<cef_response_t*, nint, void> set_header_map;

    ///
    /// Get the resolved URL after redirects or changed as a result of HSTS.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_response_t*, cef_string_t*> get_url;

    ///
    /// Set the resolved URL after redirects or changed as a result of HSTS.
    ///
    public delegate* unmanaged<cef_response_t*, cef_string_t*, void> set_url;
}