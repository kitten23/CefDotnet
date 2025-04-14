using CefDotnet.CefApi.Types;
using System;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// Structure used to implement browser process callbacks. The functions of this
/// structure will be called on the browser process main thread unless otherwise
/// indicated.
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_browser_process_handler_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Provides an opportunity to register custom preferences prior to global and
    /// request context initialization.
    ///
    /// If |type| is CEF_PREFERENCES_TYPE_GLOBAL the registered preferences can be
    /// accessed via cef_preference_manager_t::GetGlobalPreferences after
    /// OnContextInitialized is called. Global preferences are registered a single
    /// time at application startup. See related cef_settings_t.cache_path
    /// configuration.
    ///
    /// If |type| is CEF_PREFERENCES_TYPE_REQUEST_CONTEXT the preferences can be
    /// accessed via the cef_request_context_t after
    /// cef_request_context_handler_t::OnRequestContextInitialized is called.
    /// Request context preferences are registered each time a new
    /// cef_request_context_t is created. It is intended but not required that all
    /// request contexts have the same registered preferences. See related
    /// cef_request_context_settings_t.cache_path configuration.
    ///
    /// Do not keep a reference to the |registrar| object. This function is called
    /// on the browser process UI thread.
    ///
    public delegate* unmanaged<cef_browser_process_handler_t*, cef_preferences_type_t, cef_preference_registrar_t*, void> on_register_custom_preferences;

    ///
    /// Called on the browser process UI thread immediately after the CEF context
    /// has been initialized.
    ///
    public delegate* unmanaged<cef_browser_process_handler_t*, void> on_context_initialized;

    ///
    /// Called before a child process is launched. Will be called on the browser
    /// process UI thread when launching a render process and on the browser
    /// process IO thread when launching a GPU process. Provides an opportunity to
    /// modify the child process command line. Do not keep a reference to
    /// |command_line| outside of this function.
    ///
    public delegate* unmanaged<cef_browser_process_handler_t*, cef_command_line_t*, void> on_before_child_process_launch;

    ///
    /// Implement this function to provide app-specific behavior when an already
    /// running app is relaunched with the same CefSettings.root_cache_path value.
    /// For example, activate an existing app window or create a new app window.
    /// |command_line| will be read-only. Do not keep a reference to
    /// |command_line| outside of this function. Return true (1) if the relaunch
    /// is handled or false (0) for default relaunch behavior. Default behavior
    /// will create a new default styled Chrome window.
    ///
    /// To avoid cache corruption only a single app instance is allowed to run for
    /// a given CefSettings.root_cache_path value. On relaunch the app checks a
    /// process singleton lock and then forwards the new launch arguments to the
    /// already running app process before exiting early. Client apps should
    /// therefore check the cef_initialize() return value for early exit before
    /// proceeding.
    ///
    /// This function will be called on the browser process UI thread.
    ///
    public delegate* unmanaged<cef_browser_process_handler_t*, cef_command_line_t*, cef_string_t*, int> on_already_running_app_relaunch;

    ///
    /// Called from any thread when work has been scheduled for the browser
    /// process main (UI) thread. This callback is used in combination with
    /// cef_settings_t.external_message_pump and cef_do_message_loop_work() in
    /// cases where the CEF message loop must be integrated into an existing
    /// application message loop (see additional comments and warnings on
    /// CefDoMessageLoopWork). This callback should schedule a
    /// cef_do_message_loop_work() call to happen on the main (UI) thread.
    /// |delay_ms| is the requested delay in milliseconds. If |delay_ms| is <= 0
    /// then the call should happen reasonably soon. If |delay_ms| is > 0 then the
    /// call should be scheduled to happen after the specified delay and any
    /// currently pending scheduled call should be cancelled.
    ///
    public delegate* unmanaged<cef_browser_process_handler_t*, long, void> on_schedule_message_pump_work;

    ///
    /// Return the default client for use with a newly created browser window
    /// (cef_browser_t object). If null is returned the cef_browser_t will be
    /// unmanaged (no callbacks will be executed for that cef_browser_t) and
    /// application shutdown will be blocked until the browser window is closed
    /// manually. This function is currently only used with Chrome style when
    /// creating new browser windows via Chrome UI.
    ///
    public delegate* unmanaged<cef_browser_process_handler_t*, cef_client_t*> get_default_client;

    ///
    /// Return the default handler for use with a new user or incognito profile
    /// (cef_request_context_t object). If null is returned the
    /// cef_request_context_t will be unmanaged (no callbacks will be executed for
    /// that cef_request_context_t). This function is currently only used with
    /// Chrome style when creating new browser windows via Chrome UI.
    ///
    public delegate* unmanaged<cef_browser_process_handler_t*, cef_request_context_handler_t*> get_default_request_context_handler;
}