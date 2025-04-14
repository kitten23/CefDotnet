using CefDotnet.CefApi.Internal;
using CefDotnet.CefApi.Types;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// Cursor type values.
///
public enum cef_cursor_type_t
{
    CT_POINTER,
    CT_CROSS,
    CT_HAND,
    CT_IBEAM,
    CT_WAIT,
    CT_HELP,
    CT_EASTRESIZE,
    CT_NORTHRESIZE,
    CT_NORTHEASTRESIZE,
    CT_NORTHWESTRESIZE,
    CT_SOUTHRESIZE,
    CT_SOUTHEASTRESIZE,
    CT_SOUTHWESTRESIZE,
    CT_WESTRESIZE,
    CT_NORTHSOUTHRESIZE,
    CT_EASTWESTRESIZE,
    CT_NORTHEASTSOUTHWESTRESIZE,
    CT_NORTHWESTSOUTHEASTRESIZE,
    CT_COLUMNRESIZE,
    CT_ROWRESIZE,
    CT_MIDDLEPANNING,
    CT_EASTPANNING,
    CT_NORTHPANNING,
    CT_NORTHEASTPANNING,
    CT_NORTHWESTPANNING,
    CT_SOUTHPANNING,
    CT_SOUTHEASTPANNING,
    CT_SOUTHWESTPANNING,
    CT_WESTPANNING,
    CT_MOVE,
    CT_VERTICALTEXT,
    CT_CELL,
    CT_CONTEXTMENU,
    CT_ALIAS,
    CT_PROGRESS,
    CT_NODROP,
    CT_COPY,
    CT_NONE,
    CT_NOTALLOWED,
    CT_ZOOMIN,
    CT_ZOOMOUT,
    CT_GRAB,
    CT_GRABBING,
    CT_MIDDLE_PANNING_VERTICAL,
    CT_MIDDLE_PANNING_HORIZONTAL,
    CT_CUSTOM,
    CT_DND_NONE,
    CT_DND_MOVE,
    CT_DND_COPY,
    CT_DND_LINK,
    CT_NUM_VALUES,
}

///
/// Structure representing cursor information. |buffer| will be
/// |size.width|*|size.height|*4 bytes in size and represents a BGRA image with
/// an upper-left origin.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_cursor_info_t
{
    cef_point_t hotspot;
    float image_scale_factor;
    void* buffer;
    cef_size_t size;
}

///
/// Implement this structure to handle events related to browser display state.
/// The functions of this structure will be called on the UI thread.
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_display_handler_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Called when a frame's address has changed.
    ///
    public delegate* unmanaged<cef_display_handler_t*, cef_browser_t*, cef_frame_t*, cef_string_t*, void> on_address_change;

    ///
    /// Called when the page title changes.
    ///
    public delegate* unmanaged<cef_display_handler_t*, cef_browser_t*, cef_string_t*, void> on_title_change;

    ///
    /// Called when the page icon changes.
    ///
    public delegate* unmanaged<cef_display_handler_t*, cef_browser_t*, nint, void> on_favicon_urlchange;

    ///
    /// Called when web content in the page has toggled fullscreen mode. If
    /// |fullscreen| is true (1) the content will automatically be sized to fill
    /// the browser content area. If |fullscreen| is false (0) the content will
    /// automatically return to its original size and position. With Alloy style
    /// the client is responsible for triggering the fullscreen transition (for
    /// example, by calling cef_window_t::SetFullscreen when using Views). With
    /// Chrome style the fullscreen transition will be triggered automatically.
    /// The cef_window_delegate_t::OnWindowFullscreenTransition function will be
    /// called during the fullscreen transition for notification purposes.
    ///
    public delegate* unmanaged<cef_display_handler_t*, cef_browser_t*, int, void> on_fullscreen_mode_change;

    ///
    /// Called when the browser is about to display a tooltip. |text| contains the
    /// text that will be displayed in the tooltip. To handle the display of the
    /// tooltip yourself return true (1). Otherwise, you can optionally modify
    /// |text| and then return false (0) to allow the browser to display the
    /// tooltip. When window rendering is disabled the application is responsible
    /// for drawing tooltips and the return value is ignored.
    ///
    public delegate* unmanaged<cef_display_handler_t*, cef_browser_t*, cef_string_t*, int> on_tooltip;

    ///
    /// Called when the browser receives a status message. |value| contains the
    /// text that will be displayed in the status message.
    ///
    public delegate* unmanaged<cef_display_handler_t*, cef_browser_t*, cef_string_t*, void> on_status_message;

    ///
    /// Called to display a console message. Return true (1) to stop the message
    /// from being output to the console.
    ///
    public delegate* unmanaged<cef_display_handler_t*, cef_browser_t*, cef_log_severity_t, cef_string_t*, cef_string_t*, int, int> on_console_message;

    ///
    /// Called when auto-resize is enabled via
    /// cef_browser_host_t::SetAutoResizeEnabled and the contents have auto-
    /// resized. |new_size| will be the desired size in view coordinates. Return
    /// true (1) if the resize was handled or false (0) for default handling.
    ///
    public delegate* unmanaged<cef_display_handler_t*, cef_browser_t*, cef_size_t*, int> on_auto_resize;

    ///
    /// Called when the overall page loading progress has changed. |progress|
    /// ranges from 0.0 to 1.0.
    ///
    public delegate* unmanaged<cef_display_handler_t*, cef_browser_t*, double, void> on_loading_progress_change;

    ///
    /// Called when the browser's cursor has changed. If |type| is CT_CUSTOM then
    /// |custom_cursor_info| will be populated with the custom cursor information.
    /// Return true (1) if the cursor change was handled or false (0) for default
    /// handling.
    ///
    public delegate* unmanaged<cef_display_handler_t*, cef_browser_t*, nint, cef_cursor_type_t, cef_cursor_info_t*, int> on_cursor_change;

    ///
    /// Called when the browser's access to an audio and/or video source has
    /// changed.
    ///
    public delegate* unmanaged<cef_display_handler_t*, cef_browser_t*, int, int, void> on_media_access_change;
}
