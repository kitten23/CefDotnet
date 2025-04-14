using CefDotnet.CefApi.Types;
using System.Runtime;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// Policy for how the Referrer HTTP header value will be sent during
/// navigation. If the `--no-referrers` command-line flag is specified then the
/// policy value will be ignored and the Referrer value will never be sent. Must
/// be kept synchronized with net::URLRequest::ReferrerPolicy from Chromium.
///
public enum cef_referrer_policy_t : int
{
    ///
    /// Clear the referrer header if the header value is HTTPS but the request
    /// destination is HTTP. This is the default behavior.
    ///
    REFERRER_POLICY_CLEAR_REFERRER_ON_TRANSITION_FROM_SECURE_TO_INSECURE,
    REFERRER_POLICY_DEFAULT =
        REFERRER_POLICY_CLEAR_REFERRER_ON_TRANSITION_FROM_SECURE_TO_INSECURE,

    ///
    /// A slight variant on CLEAR_REFERRER_ON_TRANSITION_FROM_SECURE_TO_INSECURE:
    /// If the request destination is HTTP, an HTTPS referrer will be cleared. If
    /// the request's destination is cross-origin with the referrer (but does not
    /// downgrade), the referrer's granularity will be stripped down to an origin
    /// rather than a full URL. Same-origin requests will send the full referrer.
    ///
    REFERRER_POLICY_REDUCE_REFERRER_GRANULARITY_ON_TRANSITION_CROSS_ORIGIN,

    ///
    /// Strip the referrer down to an origin when the origin of the referrer is
    /// different from the destination's origin.
    ///
    REFERRER_POLICY_ORIGIN_ONLY_ON_TRANSITION_CROSS_ORIGIN,

    ///
    /// Never change the referrer.
    ///
    REFERRER_POLICY_NEVER_CLEAR_REFERRER,

    ///
    /// Strip the referrer down to the origin regardless of the redirect location.
    ///
    REFERRER_POLICY_ORIGIN,

    ///
    /// Clear the referrer when the request's referrer is cross-origin with the
    /// request's destination.
    ///
    REFERRER_POLICY_CLEAR_REFERRER_ON_TRANSITION_CROSS_ORIGIN,

    ///
    /// Strip the referrer down to the origin, but clear it entirely if the
    /// referrer value is HTTPS and the destination is HTTP.
    ///
    REFERRER_POLICY_ORIGIN_CLEAR_ON_TRANSITION_FROM_SECURE_TO_INSECURE,

    ///
    /// Always clear the referrer regardless of the request destination.
    ///
    REFERRER_POLICY_NO_REFERRER,

    /// Always the last value in this enumeration.
    REFERRER_POLICY_NUM_VALUES,
}

///
/// Resource type for a request. These constants match their equivalents in
/// Chromium's ResourceType and should not be renumbered.
///
public enum cef_resource_type_t : int
{
    ///
    /// Top level page.
    ///
    RT_MAIN_FRAME = 0,

    ///
    /// Frame or iframe.
    ///
    RT_SUB_FRAME,

    ///
    /// CSS stylesheet.
    ///
    RT_STYLESHEET,

    ///
    /// External script.
    ///
    RT_SCRIPT,

    ///
    /// Image (jpg/gif/png/etc).
    ///
    RT_IMAGE,

    ///
    /// Font.
    ///
    RT_FONT_RESOURCE,

    ///
    /// Some other subresource. This is the default type if the actual type is
    /// unknown.
    ///
    RT_SUB_RESOURCE,

    ///
    /// Object (or embed) tag for a plugin, or a resource that a plugin requested.
    ///
    RT_OBJECT,

    ///
    /// Media resource.
    ///
    RT_MEDIA,

    ///
    /// Main resource of a dedicated worker.
    ///
    RT_WORKER,

    ///
    /// Main resource of a shared worker.
    ///
    RT_SHARED_WORKER,

    ///
    /// Explicitly requested prefetch.
    ///
    RT_PREFETCH,

    ///
    /// Favicon.
    ///
    RT_FAVICON,

    ///
    /// XMLHttpRequest.
    ///
    RT_XHR,

    ///
    /// A request for a "<ping>".
    ///
    RT_PING,

    ///
    /// Main resource of a service worker.
    ///
    RT_SERVICE_WORKER,

    ///
    /// A report of Content Security Policy violations.
    ///
    RT_CSP_REPORT,

    ///
    /// A resource that a plugin requested.
    ///
    RT_PLUGIN_RESOURCE,

    ///
    /// A main-frame service worker navigation preload request.
    ///
    RT_NAVIGATION_PRELOAD_MAIN_FRAME = 19,

    ///
    /// A sub-frame service worker navigation preload request.
    ///
    RT_NAVIGATION_PRELOAD_SUB_FRAME,

    RT_NUM_VALUES,
}

///
/// Post data elements may represent either bytes or files.
///
public enum cef_postdataelement_type_t : int
{
    PDE_TYPE_EMPTY = 0,
    PDE_TYPE_BYTES,
    PDE_TYPE_FILE,

    PDF_TYPE_NUM_VALUES,
}

///
/// Structure used to represent a web request. The functions of this structure
/// may be called on any thread.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_request_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Returns true (1) if this object is read-only.
    ///
    public delegate* unmanaged<cef_request_t*, int> is_read_only;

    ///
    /// Get the fully qualified URL.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_request_t*, cef_string_t*> get_url;

    ///
    /// Set the fully qualified URL.
    ///
    public delegate* unmanaged<cef_request_t*, cef_string_t*, void> set_url;

    ///
    /// Get the request function type. The value will default to POST if post data
    /// is provided and GET otherwise.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_request_t*, cef_string_t*> get_method;

    ///
    /// Set the request function type.
    ///
    public delegate* unmanaged<cef_request_t*, cef_string_t*, void> set_method;

    ///
    /// Set the referrer URL and policy. If non-NULL the referrer URL must be
    /// fully qualified with an HTTP or HTTPS scheme component. Any username,
    /// password or ref component will be removed.
    ///
    public delegate* unmanaged<cef_request_t*, cef_string_t*, cef_referrer_policy_t, void> set_referrer;

    ///
    /// Get the referrer URL.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_request_t*, cef_string_t*> get_referrer_url;

    ///
    /// Get the referrer policy.
    ///
    public delegate* unmanaged<cef_request_t*, cef_referrer_policy_t> get_referrer_policy;

    ///
    /// Get the post data.
    ///
    public delegate* unmanaged<cef_request_t*, cef_post_data_t*> get_post_data;

    ///
    /// Set the post data.
    ///
    public delegate* unmanaged<cef_request_t*, cef_post_data_t*, void> set_post_data;

    ///
    /// Get the header values. Will not include the Referer value if any.
    ///
    public delegate* unmanaged<cef_request_t*, nint, void> get_header_map;

    ///
    /// Set the header values. If a Referer value exists in the header map it will
    /// be removed and ignored.
    ///
    public delegate* unmanaged<cef_request_t*, nint, void> set_header_map;

    ///
    /// Returns the first header value for |name| or an NULL string if not found.
    /// Will not return the Referer value if any. Use GetHeaderMap instead if
    /// |name| might have multiple values.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_request_t*, cef_string_t*, cef_string_t*> get_header_by_name;

    ///
    /// Set the header |name| to |value|. If |overwrite| is true (1) any existing
    /// values will be replaced with the new value. If |overwrite| is false (0)
    /// any existing values will not be overwritten. The Referer value cannot be
    /// set using this function.
    ///
    public delegate* unmanaged<cef_request_t*, cef_string_t*, cef_string_t*, int, void> set_header_by_name;

    ///
    /// Set all values at one time.
    ///
    public delegate* unmanaged<cef_request_t*, cef_string_t*, cef_string_t*, cef_post_data_t*, nint, void> set;

    ///
    /// Get the flags used in combination with cef_urlrequest_t. See
    /// cef_urlrequest_flags_t for supported values.
    ///
    public delegate* unmanaged<cef_request_t*, int> get_flags;

    ///
    /// Set the flags used in combination with cef_urlrequest_t.  See
    /// cef_urlrequest_flags_t for supported values.
    ///
    public delegate* unmanaged<cef_request_t*, int, void> set_flags;

    ///
    /// Get the URL to the first party for cookies used in combination with
    /// cef_urlrequest_t.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_request_t*, cef_string_t*> get_first_party_for_cookies;

    ///
    /// Set the URL to the first party for cookies used in combination with
    /// cef_urlrequest_t.
    ///
    public delegate* unmanaged<cef_request_t*, cef_string_t*, void> set_first_party_for_cookies;

    ///
    /// Get the resource type for this request. Only available in the browser
    /// process.
    ///
    public delegate* unmanaged<cef_request_t*, cef_resource_type_t> get_resource_type;

    ///
    /// Get the transition type for this request. Only available in the browser
    /// process and only applies to requests that represent a main frame or sub-
    /// frame navigation.
    ///
    public delegate* unmanaged<cef_request_t*, cef_transition_type_t> get_transition_type;

    ///
    /// Returns the globally unique identifier for this request or 0 if not
    /// specified. Can be used by cef_resource_request_handler_t implementations
    /// in the browser process to track a single request across multiple
    /// callbacks.
    ///
    public delegate* unmanaged<cef_request_t*, ulong> get_identifier;
}

///
/// Structure used to represent post data for a web request. The functions of
/// this structure may be called on any thread.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_post_data_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Returns true (1) if this object is read-only.
    ///
    public delegate* unmanaged<cef_post_data_t*, int> is_read_only;

    ///
    /// Returns true (1) if the underlying POST data includes elements that are
    /// not represented by this cef_post_data_t object (for example, multi-part
    /// file upload data). Modifying cef_post_data_t objects with excluded
    /// elements may result in the request failing.
    ///
    public delegate* unmanaged<cef_post_data_t*, int> has_excluded_elements;

    ///
    /// Returns the number of existing post data elements.
    ///
    public delegate* unmanaged<cef_post_data_t*, nuint> get_element_count;

    ///
    /// Retrieve the post data elements.
    ///
    public delegate* unmanaged<cef_post_data_t*, nuint*, cef_post_data_element_t**, void> get_elements;

    ///
    /// Remove the specified post data element.  Returns true (1) if the removal
    /// succeeds.
    ///
    public delegate* unmanaged<cef_post_data_t*, cef_post_data_element_t*, int> remove_element;

    ///
    /// Add the specified post data element.  Returns true (1) if the add
    /// succeeds.
    ///
    public delegate* unmanaged<cef_post_data_t*, cef_post_data_element_t*, int> add_element;

    ///
    /// Remove all existing post data elements.
    ///
    public delegate* unmanaged<cef_post_data_t*, void> remove_elements;
}

///
/// Structure used to represent a single element in the request post data. The
/// functions of this structure may be called on any thread.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_post_data_element_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Returns true (1) if this object is read-only.
    ///
    public delegate* unmanaged<cef_post_data_element_t*, int> is_read_only;

    ///
    /// Remove all contents from the post data element.
    ///
    public delegate* unmanaged<cef_post_data_element_t*, void> set_to_empty;

    ///
    /// The post data element will represent a file.
    ///
    public delegate* unmanaged<cef_post_data_element_t*, cef_string_t*, void> set_to_file;

    ///
    /// The post data element will represent bytes.  The bytes passed in will be
    /// copied.
    ///
    public delegate* unmanaged<cef_post_data_element_t*, nuint, void*, void> set_to_bytes;

    ///
    /// Return the type of this post data element.
    ///
    public delegate* unmanaged<cef_post_data_element_t*, cef_postdataelement_type_t> get_type;

    ///
    /// Return the file name.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_post_data_element_t*, cef_string_t*> get_file;

    ///
    /// Return the number of bytes.
    ///
    public delegate* unmanaged<cef_post_data_element_t*, nuint> get_bytes_count;

    ///
    /// Read up to |size| bytes into |bytes| and return the number of bytes
    /// actually read.
    ///
    public delegate* unmanaged<cef_post_data_element_t*, nuint, void*, nuint> get_bytes;
}
