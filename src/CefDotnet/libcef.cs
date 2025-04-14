using CefDotnet.CefApi.Internal;
using CefDotnet.CefApi.Types;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

unsafe public partial class libcef
{
    const string LibName = "libcef";
    public const int CEF_API_VERSION = 999999;

    /// Returns CEF version information for the libcef library. The |entry|
    /// parameter describes which version component will be returned:
    ///
    /// 0 - CEF_VERSION_MAJOR
    /// 1 - CEF_VERSION_MINOR
    /// 2 - CEF_VERSION_PATCH
    /// 3 - CEF_COMMIT_NUMBER
    /// 4 - CHROME_VERSION_MAJOR
    /// 5 - CHROME_VERSION_MINOR
    /// 6 - CHROME_VERSION_BUILD
    /// 7 - CHROME_VERSION_PATCH
    [LibraryImport(LibName)]
    public static partial int cef_version_info(int entry);

    //cef_app_capi===============================================================
    /// Configures the CEF API version and returns API hashes for the libcef
    /// library. The returned string is owned by the library and should not be
    /// freed. The |version| parameter should be CEF_API_VERSION and any changes to
    /// this value will be ignored after the first call to this method. The |entry|
    /// parameter describes which hash value will be returned:
    ///
    /// 0 - CEF_API_HASH_PLATFORM
    /// 1 - CEF_API_HASH_UNIVERSAL
    /// 2 - CEF_COMMIT_HASH (from cef_version.h)
    [LibraryImport(LibName)]
    public static partial nint cef_api_hash(int version, int entry);

    /// This function should be called on the main application thread to initialize
    /// the CEF browser process. The |application| parameter may be NULL. Returns
    /// true (1) if initialization succeeds. Returns false (0) if initialization
    /// fails or if early exit is desired (for example, due to process singleton
    /// relaunch behavior). If this function returns false (0) then the application
    /// should exit immediately without calling any other CEF functions except,
    /// optionally, CefGetErrorCode. The |windows_sandbox_info| parameter is only
    /// used on Windows and may be NULL (see cef_sandbox_win.h for details).
    [LibraryImport(LibName)]
    public static partial int cef_initialize(cef_main_args_t* args, cef_settings_t* settings, cef_app_t* application, nint windows_sandbox_info);

    ///
    /// Run the CEF message loop. Use this function instead of an application-
    /// provided message loop to get the best balance between performance and CPU
    /// usage. This function should only be called on the main application thread
    /// and only if cef_initialize() is called with a
    /// cef_settings_t.multi_threaded_message_loop value of false (0). This function
    /// will block until a quit message is received by the system.
    ///
    [LibraryImport(LibName)]
    public static partial void cef_run_message_loop();

    /// This function should be called on the main application thread to shut down
    /// the CEF browser process before the application exits. Do not call any other
    /// CEF functions after calling this function.
    [LibraryImport(LibName)]
    public static partial void cef_shutdown();

    /// This function should be called from the application entry point function to
    /// execute a secondary process. It can be used to run secondary processes from
    /// the browser client executable (default behavior) or from a separate
    /// executable specified by the cef_settings_t.browser_subprocess_path value. If
    /// called for the browser process (identified by no "type" command-line value)
    /// it will return immediately with a value of -1. If called for a recognized
    /// secondary process it will block until the process should exit and then
    /// return the process exit code. The |application| parameter may be NULL. The
    /// |windows_sandbox_info| parameter is only used on Windows and may be NULL
    /// (see cef_sandbox_win.h for details).
    [LibraryImport(LibName)]
    public static partial int cef_execute_process(cef_main_args_t* args, cef_app_t* application, nint windows_sandbox_info);

    ///
    /// Perform a single iteration of CEF message loop processing. This function is
    /// provided for cases where the CEF message loop must be integrated into an
    /// existing application message loop. Use of this function is not recommended
    /// for most users; use either the cef_run_message_loop() function or
    /// cef_settings_t.multi_threaded_message_loop if possible. When using this
    /// function care must be taken to balance performance against excessive CPU
    /// usage. It is recommended to enable the cef_settings_t.external_message_pump
    /// option when using this function so that
    /// cef_browser_process_handler_t::on_schedule_message_pump_work() callbacks can
    /// facilitate the scheduling process. This function should only be called on
    /// the main application thread and only if cef_initialize() is called with a
    /// cef_settings_t.multi_threaded_message_loop value of false (0). This function
    /// will not block.
    ///
    [LibraryImport(LibName)]
    public static partial void cef_do_message_loop_work();

    /// Create a new browser using the window parameters specified by |windowInfo|.
    /// All values will be copied internally and the actual window (if any) will be
    /// created on the UI thread. If |request_context| is NULL the global request
    /// context will be used. This function can be called on any browser process
    /// thread and will not block. The optional |extra_info| parameter provides an
    /// opportunity to specify extra information specific to the created browser
    /// that will be passed to cef_render_process_handler_t::on_browser_created() in
    /// the render process.
    [LibraryImport(LibName)]
    public static partial int cef_browser_host_create_browser(cef_window_info_t* windowInfo, cef_client_t* client, cef_string_t* url, cef_browser_settings_t* settings, cef_dictionary_value_t* extra_info, cef_request_context_t* request_context);

    //cef_string_types===========================================================
    [LibraryImport(LibName)]
    public static partial void cef_string_userfree_utf16_free(cef_string_t* str);

    [LibraryImport(LibName)]
    public static partial nint cef_string_list_alloc();
    [LibraryImport(LibName)]
    public static partial void cef_string_list_free(nint list);

    [LibraryImport(LibName)]
    public static partial nuint cef_string_list_size(nint list);

    [LibraryImport(LibName)]
    public static partial int cef_string_list_value(nint list, nuint index, cef_string_t* value);

    //cef_scheme_capi============================================================
    ///
    /// Register a scheme handler factory with the global request context. An NULL
    /// |domain_name| value for a standard scheme will cause the factory to match
    /// all domain names. The |domain_name| value will be ignored for non-standard
    /// schemes. If |scheme_name| is a built-in scheme and no handler is returned by
    /// |factory| then the built-in scheme handler factory will be called. If
    /// |scheme_name| is a custom scheme then you must also implement the
    /// cef_app_t::on_register_custom_schemes() function in all processes. This
    /// function may be called multiple times to change or remove the factory that
    /// matches the specified |scheme_name| and optional |domain_name|. Returns
    /// false (0) if an error occurs. This function may be called on any thread in
    /// the browser process. Using this function is equivalent to calling cef_reques
    /// t_context_t::cef_request_context_get_global_context()-
    /// >register_scheme_handler_factory().
    ///
    [LibraryImport(LibName)]
    public static partial int cef_register_scheme_handler_factory(cef_string_t* scheme_name, cef_string_t* domain_name, cef_scheme_handler_factory_t* factory);

    ///
    /// Clear all scheme handler factories registered with the global request
    /// context. Returns false (0) on error. This function may be called on any
    /// thread in the browser process. Using this function is equivalent to calling
    /// cef_request_context_t::cef_request_context_get_global_context()-
    /// >clear_scheme_handler_factories().
    ///
    [LibraryImport(LibName)]
    public static partial int cef_clear_scheme_handler_factories();

    //cef_request_capi===========================================================
    [LibraryImport(LibName)]
    public static partial cef_request_t* cef_request_create();

    [LibraryImport(LibName)]
    public static partial cef_post_data_t* cef_post_data_create();

    //cef_request_context_capi===================================================
    [LibraryImport(LibName)]
    public static partial cef_request_context_t* cef_request_context_get_global_context();

    //cef_values_capi============================================================
    [LibraryImport(LibName)]
    public static partial cef_value_t* cef_value_create();

    [LibraryImport(LibName)]
    public static partial cef_dictionary_value_t* cef_dictionary_value_create();

    //cef_task_capi=============================================================
    [LibraryImport(LibName)]
    public static partial cef_task_runner_t* cef_task_runner_get_for_thread(cef_thread_id_t threadId);

    //cef_parser_capi
    ///
    /// Escapes characters in |text| which are unsuitable for use as a query
    /// parameter value. Everything except alphanumerics and -_.!~*'() will be
    /// converted to "%XX". If |use_plus| is true (1) spaces will change to "+". The
    /// result is basically the same as encodeURIComponent in Javacript.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    [LibraryImport(LibName)]
    public static partial cef_string_t* cef_uriencode(cef_string_t* text, int use_plus);
}
