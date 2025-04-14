using CefDotnet.CefApi.Internal;
using CefDotnet.CefApi.Types;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// Structure used to represent an entry in navigation history.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_navigation_entry_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Returns true (1) if this object is valid. Do not call any other functions
    /// if this function returns false (0).
    ///
    public delegate* unmanaged<cef_navigation_entry_t*, int> is_valid;

    ///
    /// Returns the actual URL of the page. For some pages this may be data: URL
    /// or similar. Use get_display_url() to return a display-friendly version.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_navigation_entry_t*, cef_string_t*> get_url;

    ///
    /// Returns a display-friendly version of the URL.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_navigation_entry_t*, cef_string_t*> get_display_url;

    ///
    /// Returns the original URL that was entered by the user before any
    /// redirects.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_navigation_entry_t*, cef_string_t*> get_original_url;

    ///
    /// Returns the title set by the page. This value may be NULL.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_navigation_entry_t*, cef_string_t*> get_title;

    ///
    /// Returns the transition type which indicates what the user did to move to
    /// this page from the previous page.
    ///
    public delegate* unmanaged<cef_navigation_entry_t*, cef_transition_type_t> get_transition_type;

    ///
    /// Returns true (1) if this navigation includes post data.
    ///
    public delegate* unmanaged<cef_navigation_entry_t*, int> has_post_data;

    ///
    /// Returns the time for the last known successful navigation completion. A
    /// navigation may be completed more than once if the page is reloaded. May be
    /// 0 if the navigation has not yet completed.
    ///
    public delegate* unmanaged<cef_navigation_entry_t*, cef_basetime_t> get_completion_time;

    ///
    /// Returns the HTTP status code for the last known successful navigation
    /// response. May be 0 if the response has not yet been received or if the
    /// navigation has not yet completed.
    ///
    public delegate* unmanaged<cef_navigation_entry_t*, int> get_http_status_code;

    ///
    /// Returns the SSL information for this navigation entry.
    ///
    public delegate* unmanaged<cef_navigation_entry_t*, cef_sslstatus_t*> get_sslstatus;
}
