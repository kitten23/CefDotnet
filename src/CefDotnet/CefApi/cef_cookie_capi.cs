using CefDotnet.CefApi.Internal;
using CefDotnet.CefApi.Types;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// Cookie priority values.
///
public enum cef_cookie_priority_t : int
{
    CEF_COOKIE_PRIORITY_LOW = -1,
    CEF_COOKIE_PRIORITY_MEDIUM = 0,
    CEF_COOKIE_PRIORITY_HIGH = 1,
}

///
/// Cookie same site values.
///
public enum cef_cookie_same_site_t : int
{
    CEF_COOKIE_SAME_SITE_UNSPECIFIED,
    CEF_COOKIE_SAME_SITE_NO_RESTRICTION,
    CEF_COOKIE_SAME_SITE_LAX_MODE,
    CEF_COOKIE_SAME_SITE_STRICT_MODE,
    CEF_COOKIE_SAME_SITE_NUM_VALUES,
}

///
/// Cookie information.
///
[StructLayout(LayoutKind.Sequential)]
unsafe public ref struct cef_cookie_t
{
    public cef_cookie_t()
    {
        size = (nuint)sizeof(cef_cookie_t);
    }

    ///
    /// Size of this structure.
    ///
    nuint size;

    ///
    /// The cookie name.
    ///
    public cef_string_t name;

    ///
    /// The cookie value.
    ///
    public cef_string_t value;

    ///
    /// If |domain| is empty a host cookie will be created instead of a domain
    /// cookie. Domain cookies are stored with a leading "." and are visible to
    /// sub-domains whereas host cookies are not.
    ///
    public cef_string_t domain;

    ///
    /// If |path| is non-empty only URLs at or below the path will get the cookie
    /// value.
    ///
    public cef_string_t path;

    ///
    /// If |secure| is true the cookie will only be sent for HTTPS requests.
    ///
    public int secure;

    ///
    /// If |httponly| is true the cookie will only be sent for HTTP requests.
    ///
    public int httponly;

    ///
    /// The cookie creation date. This is automatically populated by the system on
    /// cookie creation.
    ///
    public cef_basetime_t creation;

    ///
    /// The cookie last access date. This is automatically populated by the system
    /// on access.
    ///
    public cef_basetime_t last_access;

    ///
    /// The cookie expiration date is only valid if |has_expires| is true.
    ///
    int has_expires;
    public cef_basetime_t expires;

    ///
    /// Same site.
    ///
    public cef_cookie_same_site_t same_site;

    ///
    /// Priority.
    ///
    public cef_cookie_priority_t priority;
}

///
/// Structure used for managing cookies. The functions of this structure may be
/// called on any thread unless otherwise indicated.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_cookie_manager_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Visit all cookies on the UI thread. The returned cookies are ordered by
    /// longest path, then by earliest creation date. Returns false (0) if cookies
    /// cannot be accessed.
    ///
    public delegate* unmanaged<cef_cookie_manager_t*, cef_cookie_visitor_t*, int> visit_all_cookies;

    ///
    /// Visit a subset of cookies on the UI thread. The results are filtered by
    /// the given url scheme, host, domain and path. If |includeHttpOnly| is true
    /// (1) HTTP-only cookies will also be included in the results. The returned
    /// cookies are ordered by longest path, then by earliest creation date.
    /// Returns false (0) if cookies cannot be accessed.
    ///
    public delegate* unmanaged<cef_cookie_manager_t*, cef_string_t*, int, cef_cookie_visitor_t*, int> visit_url_cookies;

    ///
    /// Sets a cookie given a valid URL and explicit user-provided cookie
    /// attributes. This function expects each attribute to be well-formed. It
    /// will check for disallowed characters (e.g. the ';' character is disallowed
    /// within the cookie value attribute) and fail without setting the cookie if
    /// such characters are found. If |callback| is non-NULL it will be executed
    /// asnychronously on the UI thread after the cookie has been set. Returns
    /// false (0) if an invalid URL is specified or if cookies cannot be accessed.
    ///
    public delegate* unmanaged<cef_cookie_manager_t*, cef_string_t*, cef_cookie_t*, cef_set_cookie_callback_t*, int> set_cookie;

    ///
    /// Delete all cookies that match the specified parameters. If both |url| and
    /// |cookie_name| values are specified all host and domain cookies matching
    /// both will be deleted. If only |url| is specified all host cookies (but not
    /// domain cookies) irrespective of path will be deleted. If |url| is NULL all
    /// cookies for all hosts and domains will be deleted. If |callback| is non-
    /// NULL it will be executed asnychronously on the UI thread after the cookies
    /// have been deleted. Returns false (0) if a non-NULL invalid URL is
    /// specified or if cookies cannot be accessed. Cookies can alternately be
    /// deleted using the Visit*Cookies() functions.
    ///
    public delegate* unmanaged<cef_cookie_manager_t*, cef_string_t*, cef_string_t*, cef_delete_cookies_callback_t*, int> delete_cookies;

    ///
    /// Flush the backing store (if any) to disk. If |callback| is non-NULL it
    /// will be executed asnychronously on the UI thread after the flush is
    /// complete. Returns false (0) if cookies cannot be accessed.
    ///
    public delegate* unmanaged<cef_cookie_manager_t*, cef_completion_callback_t*, int> flush_store;
}

///
/// Structure to implement for visiting cookie values. The functions of this
/// structure will always be called on the UI thread.
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_cookie_visitor_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Method that will be called once for each cookie. |count| is the 0-based
    /// index for the current cookie. |total| is the total number of cookies. Set
    /// |deleteCookie| to true (1) to delete the cookie currently being visited.
    /// Return false (0) to stop visiting cookies. This function may never be
    /// called if no cookies are found.
    ///
    public delegate* unmanaged<cef_cookie_visitor_t*, cef_cookie_t*, int, int, int*, int> visit;
}

///
/// Structure to implement to be notified of asynchronous completion via
/// cef_cookie_manager_t::set_cookie().
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_set_cookie_callback_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Method that will be called upon completion. |success| will be true (1) if
    /// the cookie was set successfully.
    ///
    public delegate* unmanaged<cef_set_cookie_callback_t*, int, void> on_complete;
}

///
/// Structure to implement to be notified of asynchronous completion via
/// cef_cookie_manager_t::delete_cookies().
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_delete_cookies_callback_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Method that will be called upon completion. |num_deleted| will be the
    /// number of cookies that were deleted.
    ///
    public delegate* unmanaged<cef_delete_cookies_callback_t*, int, void> on_complete;
}
