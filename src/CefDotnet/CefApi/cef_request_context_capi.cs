using CefDotnet.CefApi.Types;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// Callback structure for cef_request_context_t::ResolveHost.
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_resolve_callback_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Called on the UI thread after the ResolveHost request has completed.
    /// |result| will be the result code. |resolved_ips| will be the list of
    /// resolved IP addresses or NULL if the resolution failed.
    ///
    public delegate* unmanaged<cef_resolve_callback_t*, cef_errorcode_t, nint, void> on_resolve_completed;
}

///
/// A request context provides request handling for a set of related browser or
/// URL request objects. A request context can be specified when creating a new
/// browser via the cef_browser_host_t static factory functions or when creating
/// a new URL request via the cef_urlrequest_t static factory functions. Browser
/// objects with different request contexts will never be hosted in the same
/// render process. Browser objects with the same request context may or may not
/// be hosted in the same render process depending on the process model. Browser
/// objects created indirectly via the JavaScript window.open function or
/// targeted links will share the same render process and the same request
/// context as the source browser. When running in single-process mode there is
/// only a single render process (the main process) and so all browsers created
/// in single-process mode will share the same request context. This will be the
/// first request context passed into a cef_browser_host_t static factory
/// function and all other request context objects will be ignored.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_request_context_t
{
    ///
    /// Base structure.
    ///
    public cef_preference_manager_t @base;

    ///
    /// Returns true (1) if this object is pointing to the same context as |that|
    /// object.
    ///
    public delegate* unmanaged<cef_request_context_t*, cef_request_context_t*, int> is_same;

    ///
    /// Returns true (1) if this object is sharing the same storage as |that|
    /// object.
    ///
    public delegate* unmanaged<cef_request_context_t*, cef_request_context_t*, int> is_sharing_with;

    ///
    /// Returns true (1) if this object is the global context. The global context
    /// is used by default when creating a browser or URL request with a NULL
    /// context argument.
    ///
    public delegate* unmanaged<cef_request_context_t*, int> is_global;

    ///
    /// Returns the handler for this context if any.
    ///
    public delegate* unmanaged<cef_request_context_t*, cef_request_context_handler_t*> get_handler;

    ///
    /// Returns the cache path for this object. If NULL an "incognito mode" in-
    /// memory cache is being used.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_request_context_t*, cef_string_t*> get_cache_path;

    ///
    /// Returns the cookie manager for this object. If |callback| is non-NULL it
    /// will be executed asnychronously on the UI thread after the manager's
    /// storage has been initialized.
    ///
    public delegate* unmanaged<cef_request_context_t*, cef_completion_callback_t*, cef_cookie_manager_t*> get_cookie_manager;

    ///
    /// Register a scheme handler factory for the specified |scheme_name| and
    /// optional |domain_name|. An NULL |domain_name| value for a standard scheme
    /// will cause the factory to match all domain names. The |domain_name| value
    /// will be ignored for non-standard schemes. If |scheme_name| is a built-in
    /// scheme and no handler is returned by |factory| then the built-in scheme
    /// handler factory will be called. If |scheme_name| is a custom scheme then
    /// you must also implement the cef_app_t::on_register_custom_schemes()
    /// function in all processes. This function may be called multiple times to
    /// change or remove the factory that matches the specified |scheme_name| and
    /// optional |domain_name|. Returns false (0) if an error occurs. This
    /// function may be called on any thread in the browser process.
    ///
    public delegate* unmanaged<cef_request_context_t*, cef_string_t*, cef_string_t*, cef_scheme_handler_factory_t*, int> register_scheme_handler_factory;

    ///
    /// Clear all registered scheme handler factories. Returns false (0) on error.
    /// This function may be called on any thread in the browser process.
    ///
    public delegate* unmanaged<cef_request_context_t*, int> clear_scheme_handler_factories;

    ///
    /// Clears all certificate exceptions that were added as part of handling
    /// cef_request_handler_t::on_certificate_error(). If you call this it is
    /// recommended that you also call close_all_connections() or you risk not
    /// being prompted again for server certificates if you reconnect quickly. If
    /// |callback| is non-NULL it will be executed on the UI thread after
    /// completion.
    ///
    public delegate* unmanaged<cef_request_context_t*, cef_completion_callback_t*, void> clear_certificate_exceptions;

    ///
    /// Clears all HTTP authentication credentials that were added as part of
    /// handling GetAuthCredentials. If |callback| is non-NULL it will be executed
    /// on the UI thread after completion.
    ///
    public delegate* unmanaged<cef_request_context_t*, cef_completion_callback_t*, void> clear_http_auth_credentials;

    ///
    /// Clears all active and idle connections that Chromium currently has. This
    /// is only recommended if you have released all other CEF objects but don't
    /// yet want to call cef_shutdown(). If |callback| is non-NULL it will be
    /// executed on the UI thread after completion.
    ///
    public delegate* unmanaged<cef_request_context_t*, cef_completion_callback_t*, void> close_all_connections;

    ///
    /// Attempts to resolve |origin| to a list of associated IP addresses.
    /// |callback| will be executed on the UI thread after completion.
    ///
    public delegate* unmanaged<cef_request_context_t*, cef_string_t*, cef_resolve_callback_t*, void> resolve_host;

    ///
    /// Returns the MediaRouter object associated with this context.  If
    /// |callback| is non-NULL it will be executed asnychronously on the UI thread
    /// after the manager's context has been initialized.
    ///
    public delegate* unmanaged<cef_request_context_t*, cef_completion_callback_t*, cef_media_router_t*> get_media_router;

    ///
    /// Returns the current value for |content_type| that applies for the
    /// specified URLs. If both URLs are NULL the default value will be returned.
    /// Returns nullptr if no value is configured. Must be called on the browser
    /// process UI thread.
    ///
    public delegate* unmanaged<cef_request_context_t*, cef_string_t*, cef_string_t*, cef_content_setting_types_t, cef_value_t*> get_website_setting;

    ///
    /// Sets the current value for |content_type| for the specified URLs in the
    /// default scope. If both URLs are NULL, and the context is not incognito,
    /// the default value will be set. Pass nullptr for |value| to remove the
    /// default value for this content type.
    ///
    /// WARNING: Incorrect usage of this function may cause instability or
    /// security issues in Chromium. Make sure that you first understand the
    /// potential impact of any changes to |content_type| by reviewing the related
    /// source code in Chromium. For example, if you plan to modify
    /// CEF_CONTENT_SETTING_TYPE_POPUPS, first review and understand the usage of
    /// ContentSettingsType::POPUPS in Chromium:
    /// https://source.chromium.org/search?q=ContentSettingsType::POPUPS
    ///
    public delegate* unmanaged<cef_request_context_t*, cef_string_t*, cef_string_t*, cef_content_setting_types_t, cef_value_t*, void> set_website_setting;

    ///
    /// Returns the current value for |content_type| that applies for the
    /// specified URLs. If both URLs are NULL the default value will be returned.
    /// Returns CEF_CONTENT_SETTING_VALUE_DEFAULT if no value is configured. Must
    /// be called on the browser process UI thread.
    ///
    public delegate* unmanaged<cef_request_context_t*, cef_string_t*, cef_string_t*, cef_content_setting_types_t, cef_content_setting_values_t> get_content_setting;

    ///
    /// Sets the current value for |content_type| for the specified URLs in the
    /// default scope. If both URLs are NULL, and the context is not incognito,
    /// the default value will be set. Pass CEF_CONTENT_SETTING_VALUE_DEFAULT for
    /// |value| to use the default value for this content type.
    ///
    /// WARNING: Incorrect usage of this function may cause instability or
    /// security issues in Chromium. Make sure that you first understand the
    /// potential impact of any changes to |content_type| by reviewing the related
    /// source code in Chromium. For example, if you plan to modify
    /// CEF_CONTENT_SETTING_TYPE_POPUPS, first review and understand the usage of
    /// ContentSettingsType::POPUPS in Chromium:
    /// https://source.chromium.org/search?q=ContentSettingsType::POPUPS
    ///
    public delegate* unmanaged<cef_request_context_t*, cef_string_t*, cef_string_t*, cef_content_setting_types_t, cef_content_setting_values_t, void> set_content_setting;

    ///
    /// Sets the Chrome color scheme for all browsers that share this request
    /// context. |variant| values of SYSTEM, LIGHT and DARK change the underlying
    /// color mode (e.g. light vs dark). Other |variant| values determine how
    /// |user_color| will be applied in the current color mode. If |user_color| is
    /// transparent (0) the default color will be used.
    ///
    public delegate* unmanaged<cef_request_context_t*, cef_color_variant_t, uint, void> set_chrome_color_scheme;

    ///
    /// Returns the current Chrome color scheme mode (SYSTEM, LIGHT or DARK). Must
    /// be called on the browser process UI thread.
    ///
    public delegate* unmanaged<cef_request_context_t*, cef_color_variant_t> get_chrome_color_scheme_mode;

    ///
    /// Returns the current Chrome color scheme color, or transparent (0) for the
    /// default color. Must be called on the browser process UI thread.
    ///
    public delegate* unmanaged<cef_request_context_t*, uint> get_chrome_color_scheme_color;

    ///
    /// Returns the current Chrome color scheme variant. Must be called on the
    /// browser process UI thread.
    ///
    public delegate* unmanaged<cef_request_context_t*, cef_color_variant_t> get_chrome_color_scheme_variant;
}
