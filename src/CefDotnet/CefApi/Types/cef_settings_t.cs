using System.Runtime.InteropServices;

namespace CefDotnet.CefApi.Types;

///
/// Initialization settings. Specify NULL or 0 to get the recommended default
/// values. Many of these and other settings can also configured using command-
/// line switches.
///
[StructLayout(LayoutKind.Sequential)]
unsafe public ref struct cef_settings_t
{
    public cef_settings_t()
    {
        size = (nuint)sizeof(cef_settings_t);
    }

    ///
    /// Size of this structure.
    ///
    public nuint size;

    ///
    /// Set to true (1) to disable the sandbox for sub-processes. See
    /// cef_sandbox_win.h for requirements to enable the sandbox on Windows. Also
    /// configurable using the "no-sandbox" command-line switch.
    ///
    public int no_sandbox;

    ///
    /// The path to a separate executable that will be launched for sub-processes.
    /// If this value is empty on Windows or Linux then the main process
    /// executable will be used. If this value is empty on macOS then a helper
    /// executable must exist at "Contents/Frameworks/<app>
    /// Helper.app/Contents/MacOS/<app> Helper" in the top-level app bundle. See
    /// the comments on CefExecuteProcess() for details. If this value is
    /// non-empty then it must be an absolute path. Also configurable using the
    /// "browser-subprocess-path" command-line switch.
    ///
    public cef_string_t browser_subprocess_path;

    ///
    /// The path to the CEF framework directory on macOS. If this value is empty
    /// then the framework must exist at "Contents/Frameworks/Chromium Embedded
    /// Framework.framework" in the top-level app bundle. If this value is
    /// non-empty then it must be an absolute path. Also configurable using the
    /// "framework-dir-path" command-line switch.
    ///
    public cef_string_t framework_dir_path;

    ///
    /// The path to the main bundle on macOS. If this value is empty then it
    /// defaults to the top-level app bundle. If this value is non-empty then it
    /// must be an absolute path. Also configurable using the "main-bundle-path"
    /// command-line switch.
    ///
    public cef_string_t main_bundle_path;

    ///
    /// Set to true (1) to have the browser process message loop run in a separate
    /// thread. If false (0) then the CefDoMessageLoopWork() function must be
    /// called from your application message loop. This option is only supported
    /// on Windows and Linux.
    ///
    public int multi_threaded_message_loop;

    ///
    /// Set to true (1) to control browser process main (UI) thread message pump
    /// scheduling via the CefBrowserProcessHandler::OnScheduleMessagePumpWork()
    /// callback. This option is recommended for use in combination with the
    /// CefDoMessageLoopWork() function in cases where the CEF message loop must
    /// be integrated into an existing application message loop (see additional
    /// comments and warnings on CefDoMessageLoopWork). Enabling this option is
    /// not recommended for most users; leave this option disabled and use either
    /// the CefRunMessageLoop() function or multi_threaded_message_loop if
    /// possible.
    ///
    public int external_message_pump;

    ///
    /// Set to true (1) to enable windowless (off-screen) rendering support. Do
    /// not enable this value if the application does not use windowless rendering
    /// as it may reduce rendering performance on some systems.
    ///
    public int windowless_rendering_enabled;

    ///
    /// Set to true (1) to disable configuration of browser process features using
    /// standard CEF and Chromium command-line arguments. Configuration can still
    /// be specified using CEF data structures or via the
    /// CefApp::OnBeforeCommandLineProcessing() method.
    ///
    public int command_line_args_disabled;

    ///
    /// The directory where data for the global browser cache will be stored on
    /// disk. If this value is non-empty then it must be an absolute path that is
    /// either equal to or a child directory of CefSettings.root_cache_path. If
    /// this value is empty then browsers will be created in "incognito mode"
    /// where in-memory caches are used for storage and no profile-specific data
    /// is persisted to disk (installation-specific data will still be persisted
    /// in root_cache_path). HTML5 databases such as localStorage will only
    /// persist across sessions if a cache path is specified. Can be overridden
    /// for individual CefRequestContext instances via the
    /// CefRequestContextSettings.cache_path value. Any child directory value will
    /// be ignored and the "default" profile (also a child directory) will be used
    /// instead.
    ///
    public cef_string_t cache_path;

    ///
    /// The root directory for installation-specific data and the parent directory
    /// for profile-specific data. All CefSettings.cache_path and
    /// CefRequestContextSettings.cache_path values must have this parent
    /// directory in common. If this value is empty and CefSettings.cache_path is
    /// non-empty then it will default to the CefSettings.cache_path value. Any
    /// non-empty value must be an absolute path. If both values are empty then
    /// the default platform-specific directory will be used
    /// ("~/.config/cef_user_data" directory on Linux, "~/Library/Application
    /// Support/CEF/User Data" directory on MacOS, "AppData\Local\CEF\User Data"
    /// directory under the user profile directory on Windows). Use of the default
    /// directory is not recommended in production applications (see below).
    ///
    /// Multiple application instances writing to the same root_cache_path
    /// directory could result in data corruption. A process singleton lock based
    /// on the root_cache_path value is therefore used to protect against this.
    /// This singleton behavior applies to all CEF-based applications using
    /// version 120 or newer. You should customize root_cache_path for your
    /// application and implement CefBrowserProcessHandler::
    /// OnAlreadyRunningAppRelaunch, which will then be called on any app relaunch
    /// with the same root_cache_path value.
    ///
    /// Failure to set the root_cache_path value correctly may result in startup
    /// crashes or other unexpected behaviors (for example, the sandbox blocking
    /// read/write access to certain files).
    ///
    public cef_string_t root_cache_path;

    ///
    /// To persist session cookies (cookies without an expiry date or validity
    /// interval) by default when using the global cookie manager set this value
    /// to true (1). Session cookies are generally intended to be transient and
    /// most Web browsers do not persist them. A |cache_path| value must also be
    /// specified to enable this feature. Also configurable using the
    /// "persist-session-cookies" command-line switch. Can be overridden for
    /// individual CefRequestContext instances via the
    /// CefRequestContextSettings.persist_session_cookies value.
    ///
    public int persist_session_cookies;

    ///
    /// Value that will be returned as the User-Agent HTTP header. If empty the
    /// default User-Agent string will be used. Also configurable using the
    /// "user-agent" command-line switch.
    ///
    public cef_string_t user_agent;

    ///
    /// Value that will be inserted as the product portion of the default
    /// User-Agent string. If empty the Chromium product version will be used. If
    /// |userAgent| is specified this value will be ignored. Also configurable
    /// using the "user-agent-product" command-line switch.
    ///
    public cef_string_t user_agent_product;

    ///
    /// The locale string that will be passed to WebKit. If empty the default
    /// locale of "en-US" will be used. This value is ignored on Linux where
    /// locale is determined using environment variable parsing with the
    /// precedence order: LANGUAGE, LC_ALL, LC_MESSAGES and LANG. Also
    /// configurable using the "lang" command-line switch.
    ///
    public cef_string_t locale;

    ///
    /// The directory and file name to use for the debug log. If empty a default
    /// log file name and location will be used. On Windows and Linux a
    /// "debug.log" file will be written in the main executable directory. On
    /// MacOS a "~/Library/Logs/[app name]_debug.log" file will be written where
    /// [app name] is the name of the main app executable. Also configurable using
    /// the "log-file" command-line switch.
    ///
    public cef_string_t log_file;

    ///
    /// The log severity. Only messages of this severity level or higher will be
    /// logged. When set to DISABLE no messages will be written to the log file,
    /// but FATAL messages will still be output to stderr. Also configurable using
    /// the "log-severity" command-line switch with a value of "verbose", "info",
    /// "warning", "error", "fatal" or "disable".
    ///
    public cef_log_severity_t log_severity;

    ///
    /// The log items prepended to each log line. If not set the default log items
    /// will be used. Also configurable using the "log-items" command-line switch
    /// with a value of "none" for no log items, or a comma-delimited list of
    /// values "pid", "tid", "timestamp" or "tickcount" for custom log items.
    ///
    public cef_log_items_t log_items;

    ///
    /// Custom flags that will be used when initializing the V8 JavaScript engine.
    /// The consequences of using custom flags may not be well tested. Also
    /// configurable using the "js-flags" command-line switch.
    ///
    public cef_string_t javascript_flags;

    ///
    /// The fully qualified path for the resources directory. If this value is
    /// empty the *.pak files must be located in the module directory on
    /// Windows/Linux or the app bundle Resources directory on MacOS. If this
    /// value is non-empty then it must be an absolute path. Also configurable
    /// using the "resources-dir-path" command-line switch.
    ///
    public cef_string_t resources_dir_path;

    ///
    /// The fully qualified path for the locales directory. If this value is empty
    /// the locales directory must be located in the module directory. If this
    /// value is non-empty then it must be an absolute path. This value is ignored
    /// on MacOS where pack files are always loaded from the app bundle Resources
    /// directory. Also configurable using the "locales-dir-path" command-line
    /// switch.
    ///
    public cef_string_t locales_dir_path;

    ///
    /// Set to a value between 1024 and 65535 to enable remote debugging on the
    /// specified port. Also configurable using the "remote-debugging-port"
    /// command-line switch. Specifying 0 via the command-line switch will result
    /// in the selection of an ephemeral port and the port number will be printed
    /// as part of the WebSocket endpoint URL to stderr. If a cache directory path
    /// is provided the port will also be written to the
    /// <cache-dir>/DevToolsActivePort file. Remote debugging can be accessed by
    /// loading the chrome://inspect page in Google Chrome. Port numbers 9222 and
    /// 9229 are discoverable by default. Other port numbers may need to be
    /// configured via "Discover network targets" on the Devices tab.
    ///
    public int remote_debugging_port;

    ///
    /// The number of stack trace frames to capture for uncaught exceptions.
    /// Specify a positive value to enable the
    /// CefRenderProcessHandler::OnUncaughtException() callback. Specify 0
    /// (default value) and OnUncaughtException() will not be called. Also
    /// configurable using the "uncaught-exception-stack-size" command-line
    /// switch.
    ///
    public int uncaught_exception_stack_size;

    ///
    /// Background color used for the browser before a document is loaded and when
    /// no document color is specified. The alpha component must be either fully
    /// opaque (0xFF) or fully transparent (0x00). If the alpha component is fully
    /// opaque then the RGB components will be used as the background color. If
    /// the alpha component is fully transparent for a windowed browser then the
    /// default value of opaque white be used. If the alpha component is fully
    /// transparent for a windowless (off-screen) browser then transparent
    /// painting will be enabled.
    ///
    public uint background_color;

    ///
    /// Comma delimited ordered list of language codes without any whitespace that
    /// will be used in the "Accept-Language" HTTP request header and
    /// "navigator.language" JS attribute. Can be overridden for individual
    /// CefRequestContext instances via the
    /// CefRequestContextSettings.accept_language_list value.
    ///
    public cef_string_t accept_language_list;

    ///
    /// Comma delimited list of schemes supported by the associated
    /// CefCookieManager. If |cookieable_schemes_exclude_defaults| is false (0)
    /// the default schemes ("http", "https", "ws" and "wss") will also be
    /// supported. Not specifying a |cookieable_schemes_list| value and setting
    /// |cookieable_schemes_exclude_defaults| to true (1) will disable all loading
    /// and saving of cookies. These settings will only impact the global
    /// CefRequestContext. Individual CefRequestContext instances can be
    /// configured via the CefRequestContextSettings.cookieable_schemes_list and
    /// CefRequestContextSettings.cookieable_schemes_exclude_defaults values.
    ///
    public cef_string_t cookieable_schemes_list;
    public int cookieable_schemes_exclude_defaults;

    ///
    /// Specify an ID to enable Chrome policy management via Platform and OS-user
    /// policies. On Windows, this is a registry key like
    /// "SOFTWARE\\Policies\\Google\\Chrome". On MacOS, this is a bundle ID like
    /// "com.google.Chrome". On Linux, this is an absolute directory path like
    /// "/etc/opt/chrome/policies". Only supported with Chrome style. See
    /// https://support.google.com/chrome/a/answer/9037717 for details.
    ///
    /// Chrome Browser Cloud Management integration, when enabled via the
    /// "enable-chrome-browser-cloud-management" command-line flag, will also use
    /// the specified ID. See https://support.google.com/chrome/a/answer/9116814
    /// for details.
    ///
    public cef_string_t chrome_policy_id;

    ///
    /// Specify an ID for an ICON resource that can be loaded from the main
    /// executable and used when creating default Chrome windows such as DevTools
    /// and Task Manager. If unspecified the default Chromium ICON (IDR_MAINFRAME
    /// [101]) will be loaded from libcef.dll. Only supported with Chrome style on
    /// Windows.
    ///
    public int chrome_app_icon_id;

    ///
    /// Specify whether signal handlers must be disabled on POSIX systems.
    ///
    public int disable_signal_handlers;
}
