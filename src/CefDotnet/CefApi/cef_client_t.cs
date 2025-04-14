using CefDotnet.CefApi.Types;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_context_menu_handler_t { }
[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_dialog_handler_t { }
[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_drag_handler_t { }
[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_find_handler_t { }
[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_focus_handler_t { }
[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_frame_handler_t { }
[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_permission_handler_t { }
[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_jsdialog_handler_t { }
[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_keyboard_handler_t { }
[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_print_handler_t { }
[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_render_handler_t { }

///
/// Implement this structure to provide handler implementations.
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_client_t
{
    ///
    /// Base structure.
    ///
    public cef_base_ref_counted_t @base;

    ///
    /// Return the handler for audio rendering events.
    ///
    public delegate* unmanaged<cef_client_t*, cef_audio_handler_t*> get_audio_handler;

    ///
    /// Return the handler for commands. If no handler is provided the default
    /// implementation will be used.
    ///
    public delegate* unmanaged<cef_client_t*, cef_command_handler_t*> get_command_handler;

    ///
    /// Return the handler for context menus. If no handler is provided the
    /// default implementation will be used.
    ///
    public delegate* unmanaged<cef_client_t*, cef_context_menu_handler_t*> get_context_menu_handler;

    ///
    /// Return the handler for dialogs. If no handler is provided the default
    /// implementation will be used.
    ///
    public delegate* unmanaged<cef_client_t*, cef_dialog_handler_t*> get_dialog_handler;

    ///
    /// Return the handler for browser display state events.
    ///
    public delegate* unmanaged<cef_client_t*, cef_display_handler_t*> get_display_handler;

    ///
    /// Return the handler for download events. If no handler is returned
    /// downloads will not be allowed.
    ///
    public delegate* unmanaged<cef_client_t*, cef_download_handler_t*> get_download_handler;

    ///
    /// Return the handler for drag events.
    ///
    public delegate* unmanaged<cef_client_t*, cef_drag_handler_t*> get_drag_handler;

    ///
    /// Return the handler for find result events.
    ///
    public delegate* unmanaged<cef_client_t*, cef_find_handler_t*> get_find_handler;

    ///
    /// Return the handler for focus events.
    ///
    public delegate* unmanaged<cef_client_t*, cef_focus_handler_t*> get_focus_handler;

    ///
    /// Return the handler for events related to cef_frame_t lifespan. This
    /// function will be called once during cef_browser_t creation and the result
    /// will be cached for performance reasons.
    ///
    public delegate* unmanaged<cef_client_t*, cef_frame_handler_t*> get_frame_handler;

    ///
    /// Return the handler for permission requests.
    ///
    public delegate* unmanaged<cef_client_t*, cef_permission_handler_t*> get_permission_handler;

    ///
    /// Return the handler for JavaScript dialogs. If no handler is provided the
    /// default implementation will be used.
    ///
    public delegate* unmanaged<cef_client_t*, cef_jsdialog_handler_t*> get_jsdialog_handler;

    ///
    /// Return the handler for keyboard events.
    ///
    public delegate* unmanaged<cef_client_t*, cef_keyboard_handler_t*> get_keyboard_handler;

    ///
    /// Return the handler for browser life span events.
    ///
    public delegate* unmanaged<cef_client_t*, cef_life_span_handler_t*> get_life_span_handler;

    ///
    /// Return the handler for browser load status events.
    ///
    public delegate* unmanaged<cef_client_t*, cef_load_handler_t*> get_load_handler;

    ///
    /// Return the handler for printing on Linux. If a print handler is not
    /// provided then printing will not be supported on the Linux platform.
    ///
    public delegate* unmanaged<cef_client_t*, cef_print_handler_t*> get_print_handler;

    ///
    /// Return the handler for off-screen rendering events.
    ///
    public delegate* unmanaged<cef_client_t*, cef_render_handler_t*> get_render_handler;

    ///
    /// Return the handler for browser request events.
    ///
    public delegate* unmanaged<cef_client_t*, cef_request_handler_t*> get_request_handler;

    ///
    /// Called when a new message is received from a different process. Return
    /// true (1) if the message was handled or false (0) otherwise.  It is safe to
    /// keep a reference to |message| outside of this callback.
    ///
    public delegate* unmanaged<cef_client_t*, cef_client_t*, cef_browser_t*, cef_frame_t*, cef_process_id_t, cef_preference_manager_t*, int> on_process_message_received;
}