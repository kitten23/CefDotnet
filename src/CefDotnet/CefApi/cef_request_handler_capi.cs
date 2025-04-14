using CefDotnet.CefApi.Types;
using System.Runtime;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// Callback structure used to select a client certificate for authentication.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_select_client_certificate_callback_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Chooses the specified certificate for client certificate authentication.
    /// NULL value means that no client certificate should be used.
    ///
    public delegate* unmanaged<cef_select_client_certificate_callback_t*, cef_x509_certificate_t*, void> select;
}

///
/// Implement this structure to handle events related to browser requests. The
/// functions of this structure will be called on the thread indicated.
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_request_handler_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Called on the UI thread before browser navigation. Return true (1) to
    /// cancel the navigation or false (0) to allow the navigation to proceed. The
    /// |request| object cannot be modified in this callback.
    /// cef_load_handler_t::OnLoadingStateChange will be called twice in all
    /// cases. If the navigation is allowed cef_load_handler_t::OnLoadStart and
    /// cef_load_handler_t::OnLoadEnd will be called. If the navigation is
    /// canceled cef_load_handler_t::OnLoadError will be called with an
    /// |errorCode| value of ERR_ABORTED. The |user_gesture| value will be true
    /// (1) if the browser navigated via explicit user gesture (e.g. clicking a
    /// link) or false (0) if it navigated automatically (e.g. via the
    /// DomContentLoaded event).
    ///
    public delegate* unmanaged<cef_request_handler_t*, cef_browser_t*, cef_frame_t*, cef_request_t*, int, int, int> on_before_browse;

    ///
    /// Called on the UI thread before OnBeforeBrowse in certain limited cases
    /// where navigating a new or different browser might be desirable. This
    /// includes user-initiated navigation that might open in a special way (e.g.
    /// links clicked via middle-click or ctrl + left-click) and certain types of
    /// cross-origin navigation initiated from the renderer process (e.g.
    /// navigating the top-level frame to/from a file URL). The |browser| and
    /// |frame| values represent the source of the navigation. The
    /// |target_disposition| value indicates where the user intended to navigate
    /// the browser based on standard Chromium behaviors (e.g. current tab, new
    /// tab, etc). The |user_gesture| value will be true (1) if the browser
    /// navigated via explicit user gesture (e.g. clicking a link) or false (0) if
    /// it navigated automatically (e.g. via the DomContentLoaded event). Return
    /// true (1) to cancel the navigation or false (0) to allow the navigation to
    /// proceed in the source browser's top-level frame.
    ///
    public delegate* unmanaged<cef_request_handler_t*, cef_browser_t*, cef_frame_t*, cef_string_t*, cef_window_open_disposition_t, int, int> on_open_urlfrom_tab;

    ///
    /// Called on the browser process IO thread before a resource request is
    /// initiated. The |browser| and |frame| values represent the source of the
    /// request. |request| represents the request contents and cannot be modified
    /// in this callback. |is_navigation| will be true (1) if the resource request
    /// is a navigation. |is_download| will be true (1) if the resource request is
    /// a download. |request_initiator| is the origin (scheme + domain) of the
    /// page that initiated the request. Set |disable_default_handling| to true
    /// (1) to disable default handling of the request, in which case it will need
    /// to be handled via cef_resource_request_handler_t::GetResourceHandler or it
    /// will be canceled. To allow the resource load to proceed with default
    /// handling return NULL. To specify a handler for the resource return a
    /// cef_resource_request_handler_t object. If this callback returns NULL the
    /// same function will be called on the associated
    /// cef_request_context_handler_t, if any.
    ///
    public delegate* unmanaged<cef_request_handler_t*, cef_browser_t*, cef_frame_t*, cef_request_t*, int, int, cef_string_t*, int*, cef_resource_request_handler_t*> get_resource_request_handler;

    ///
    /// Called on the IO thread when the browser needs credentials from the user.
    /// |origin_url| is the origin making this authentication request. |isProxy|
    /// indicates whether the host is a proxy server. |host| contains the hostname
    /// and |port| contains the port number. |realm| is the realm of the challenge
    /// and may be NULL. |scheme| is the authentication scheme used, such as
    /// "basic" or "digest", and will be NULL if the source of the request is an
    /// FTP server. Return true (1) to continue the request and call
    /// cef_auth_callback_t::cont() either in this function or at a later time
    /// when the authentication information is available. Return false (0) to
    /// cancel the request immediately.
    ///
    public delegate* unmanaged<cef_request_handler_t*, cef_browser_t*, cef_string_t*, int, cef_string_t*, int, cef_string_t*, cef_string_t*, cef_auth_callback_t*, int> get_auth_credentials;

    ///
    /// Called on the UI thread to handle requests for URLs with an invalid SSL
    /// certificate. Return true (1) and call cef_callback_t functions either in
    /// this function or at a later time to continue or cancel the request. Return
    /// false (0) to cancel the request immediately. If
    /// cef_settings_t.ignore_certificate_errors is set all invalid certificates
    /// will be accepted without calling this function.
    ///
    public delegate* unmanaged<cef_request_handler_t*, cef_browser_t*, cef_errorcode_t, cef_string_t*, cef_sslinfo_t*, cef_callback_t*, int> on_certificate_error;

    ///
    /// Called on the UI thread when a client certificate is being requested for
    /// authentication. Return false (0) to use the default behavior.  If the
    /// |certificates| list is not NULL the default behavior will be to display a
    /// dialog for certificate selection. If the |certificates| list is NULL then
    /// the default behavior will be not to show a dialog and it will continue
    /// without using any certificate. Return true (1) and call
    /// cef_select_client_certificate_callback_t::Select either in this function
    /// or at a later time to select a certificate. Do not call Select or call it
    /// with NULL to continue without using any certificate. |isProxy| indicates
    /// whether the host is an HTTPS proxy or the origin server. |host| and |port|
    /// contains the hostname and port of the SSL server. |certificates| is the
    /// list of certificates to choose from; this list has already been pruned by
    /// Chromium so that it only contains certificates from issuers that the
    /// server trusts.
    ///
    public delegate* unmanaged<cef_request_handler_t*, cef_browser_t*, int, cef_string_t*, int, nuint, cef_x509_certificate_t**, cef_select_client_certificate_callback_t*, int> on_select_client_certificate;

    ///
    /// Called on the browser process UI thread when the render view associated
    /// with |browser| is ready to receive/handle IPC messages in the render
    /// process.
    ///
    public delegate* unmanaged<cef_request_handler_t*, cef_browser_t*, void> on_render_view_ready;

    ///
    /// Called on the browser process UI thread when the render process is
    /// unresponsive as indicated by a lack of input event processing for at least
    /// 15 seconds. Return false (0) for the default behavior which is an
    /// indefinite wait with Alloy style or display of the "Page unresponsive"
    /// dialog with Chrome style. Return true (1) and don't execute the callback
    /// for an indefinite wait without display of the Chrome style dialog. Return
    /// true (1) and call cef_unresponsive_process_callback_t::Wait either in this
    /// function or at a later time to reset the wait timer, potentially
    /// triggering another call to this function if the process remains
    /// unresponsive. Return true (1) and call
    /// cef_unresponsive_process_callback_t:: Terminate either in this function or
    /// at a later time to terminate the unresponsive process, resulting in a call
    /// to OnRenderProcessTerminated. OnRenderProcessResponsive will be called if
    /// the process becomes responsive after this function is called. This
    /// functionality depends on the hang monitor which can be disabled by passing
    /// the `--disable-hang-monitor` command-line flag.
    ///
    public delegate* unmanaged<cef_request_handler_t*, cef_browser_t*, cef_unresponsive_process_callback_t*, int> on_render_process_unresponsive;

    ///
    /// Called on the browser process UI thread when the render process becomes
    /// responsive after previously being unresponsive. See documentation on
    /// OnRenderProcessUnresponsive.
    ///
    public delegate* unmanaged<cef_request_handler_t*, cef_browser_t*, void> on_render_process_responsive;

    ///
    /// Called on the browser process UI thread when the render process terminates
    /// unexpectedly. |status| indicates how the process terminated. |error_code|
    /// and |error_string| represent the error that would be displayed in Chrome's
    /// "Aw, Snap!" view. Possible |error_code| values include cef_resultcode_t
    /// non-normal exit values and platform-specific crash values (for example, a
    /// Posix signal or Windows hardware exception).
    ///
    public delegate* unmanaged<cef_request_handler_t*, cef_browser_t*, cef_termination_status_t, int, cef_string_t*, void> on_render_process_terminated;

    ///
    /// Called on the browser process UI thread when the window.document object of
    /// the main frame has been created.
    ///
    public delegate* unmanaged<cef_request_handler_t*, cef_browser_t*, void> on_document_available_in_main_frame;
}

///
/// Structure representing SSL information.
///
/// NOTE: This struct is allocated DLL-side.
///

[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_sslinfo_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Returns a bitmask containing any and all problems verifying the server
    /// certificate.
    ///
    public delegate* unmanaged<cef_sslinfo_t*, cef_cert_status_t> get_cert_status;
    //cef_cert_status_t(CEF_CALLBACK* get_cert_status)(struct _cef_sslinfo_t* self);

    ///
    /// Returns the X.509 certificate.
    ///
    public delegate* unmanaged<cef_sslinfo_t*, cef_x509_certificate_t*> get_x509_certificate;
    //struct _cef_x509_certificate_t*(CEF_CALLBACK* get_x509_certificate) (
    //  struct _cef_sslinfo_t* self);
}

///
/// Callback structure for asynchronous handling of an unresponsive process.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_unresponsive_process_callback_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Reset the timeout for the unresponsive process.
    ///
    public delegate* unmanaged<cef_unresponsive_process_callback_t*, void> wait;
    //void (CEF_CALLBACK* wait) (struct _cef_unresponsive_process_callback_t* self);

    ///
    /// Terminate the unresponsive process.
    ///
    public delegate* unmanaged<cef_unresponsive_process_callback_t*, void> terminate;
    //void (CEF_CALLBACK* terminate) (
    //  struct _cef_unresponsive_process_callback_t* self);
}

///
/// Process termination status values.
///
public enum cef_termination_status_t
{
    ///
    /// Non-zero exit status.
    ///
    TS_ABNORMAL_TERMINATION,

    ///
    /// SIGKILL or task manager kill.
    ///
    TS_PROCESS_WAS_KILLED,

    ///
    /// Segmentation fault.
    ///
    TS_PROCESS_CRASHED,

    ///
    /// Out of memory. Some platforms may use TS_PROCESS_CRASHED instead.
    ///
    TS_PROCESS_OOM,

    ///
    /// Child process never launched.
    ///
    TS_LAUNCH_FAILED,

    ///
    /// On Windows, the OS terminated the process due to code integrity failure.
    ///
    TS_INTEGRITY_FAILURE,

    TS_NUM_VALUES,
}
