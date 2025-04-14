using CefDotnet.CefApi.Types;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// Result codes for CefMediaRouter::CreateRoute. Should be kept in sync with
/// Chromium's media_router::mojom::RouteRequestResultCode type.
///
public enum cef_media_route_create_result_t : int
{
    CEF_MRCR_UNKNOWN_ERROR,
    CEF_MRCR_OK,
    CEF_MRCR_TIMED_OUT,
    CEF_MRCR_ROUTE_NOT_FOUND,
    CEF_MRCR_SINK_NOT_FOUND,
    CEF_MRCR_INVALID_ORIGIN,
    CEF_MRCR_OFF_THE_RECORD_MISMATCH_DEPRECATED,
    CEF_MRCR_NO_SUPPORTED_PROVIDER,
    CEF_MRCR_CANCELLED,
    CEF_MRCR_ROUTE_ALREADY_EXISTS,
    CEF_MRCR_DESKTOP_PICKER_FAILED,
    CEF_MRCR_ROUTE_ALREADY_TERMINATED,
    CEF_MRCR_REDUNDANT_REQUEST,
    CEF_MRCR_USER_NOT_ALLOWED,
    CEF_MRCR_NOTIFICATION_DISABLED,
    CEF_MRCR_NUM_VALUES,
}

///
/// Connection state for a MediaRoute object. Should be kept in sync with
/// Chromium's blink::mojom::PresentationConnectionState type.
///
public enum cef_media_route_connection_state_t : int
{
    CEF_MRCS_UNKNOWN = -1,
    CEF_MRCS_CONNECTING,
    CEF_MRCS_CONNECTED,
    CEF_MRCS_CLOSED,
    CEF_MRCS_TERMINATED,
    CEF_MRCS_NUM_VALUES,
}

///
/// Icon types for a MediaSink object. Should be kept in sync with Chromium's
/// media_router::SinkIconType type.
///
public enum cef_media_sink_icon_type_t : int
{
    CEF_MSIT_CAST,
    CEF_MSIT_CAST_AUDIO_GROUP,
    CEF_MSIT_CAST_AUDIO,
    CEF_MSIT_MEETING,
    CEF_MSIT_HANGOUT,
    CEF_MSIT_EDUCATION,
    CEF_MSIT_WIRED_DISPLAY,
    CEF_MSIT_GENERIC,
    CEF_MSIT_NUM_VALUES,
}

///
/// Device information for a MediaSink object.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe ref struct cef_media_sink_device_info_t
{
    public cef_media_sink_device_info_t()
    {
        size = (nuint)sizeof(cef_media_sink_device_info_t);
    }

    ///
    /// Size of this structure.
    ///
    nuint size;

    public cef_string_t ip_address;
    public int port;
    public cef_string_t model_name;
}

///
/// Supports discovery of and communication with media devices on the local
/// network via the Cast and DIAL protocols. The functions of this structure may
/// be called on any browser process thread unless otherwise indicated.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_media_router_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Add an observer for MediaRouter events. The observer will remain
    /// registered until the returned Registration object is destroyed.
    ///
    public delegate* unmanaged<cef_media_router_t*, cef_media_observer_t*, cef_registration_t*> add_observer;

    ///
    /// Returns a MediaSource object for the specified media source URN. Supported
    /// URN schemes include "cast:" and "dial:", and will be already known by the
    /// client application (e.g. "cast:<appId>?clientId=<clientId>").
    ///
    public delegate* unmanaged<cef_media_router_t*, cef_string_t*, cef_media_source_t*> get_source;

    ///
    /// Trigger an asynchronous call to cef_media_observer_t::OnSinks on all
    /// registered observers.
    ///
    public delegate* unmanaged<cef_media_router_t*, void> notify_current_sinks;

    ///
    /// Create a new route between |source| and |sink|. Source and sink must be
    /// valid, compatible (as reported by cef_media_sink_t::IsCompatibleWith), and
    /// a route between them must not already exist. |callback| will be executed
    /// on success or failure. If route creation succeeds it will also trigger an
    /// asynchronous call to cef_media_observer_t::OnRoutes on all registered
    /// observers.
    ///
    public delegate* unmanaged<cef_media_router_t*, cef_media_source_t*, cef_media_sink_t*, cef_media_route_create_callback_t*, void> create_route;

    ///
    /// Trigger an asynchronous call to cef_media_observer_t::OnRoutes on all
    /// registered observers.
    ///
    public delegate* unmanaged<cef_media_router_t*, void> notify_current_routes;
}

///
/// Implemented by the client to observe MediaRouter events and registered via
/// cef_media_router_t::AddObserver. The functions of this structure will be
/// called on the browser process UI thread.
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_media_observer_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// The list of available media sinks has changed or
    /// cef_media_router_t::NotifyCurrentSinks was called.
    ///
    public delegate* unmanaged<cef_media_observer_t*, nuint, cef_media_sink_t**, void> on_sinks;

    ///
    /// The list of available media routes has changed or
    /// cef_media_router_t::NotifyCurrentRoutes was called.
    ///
    public delegate* unmanaged<cef_media_observer_t*, nuint, cef_media_route_t**, void> on_routes;

    ///
    /// The connection state of |route| has changed.
    ///
    public delegate* unmanaged<cef_media_observer_t*, cef_media_route_t*, cef_media_route_connection_state_t, void> on_route_state_changed;

    ///
    /// A message was received over |route|. |message| is only valid for the scope
    /// of this callback and should be copied if necessary.
    ///
    public delegate* unmanaged<cef_media_observer_t*, cef_media_route_t*, void*, nuint, void> on_route_message_received;
}

///
/// Represents the route between a media source and sink. Instances of this
/// object are created via cef_media_router_t::CreateRoute and retrieved via
/// cef_media_observer_t::OnRoutes. Contains the status and metadata of a
/// routing operation. The functions of this structure may be called on any
/// browser process thread unless otherwise indicated.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_media_route_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Returns the ID for this route.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_media_route_t*, cef_string_t*> get_id;

    ///
    /// Returns the source associated with this route.
    ///
    public delegate* unmanaged<cef_media_route_t*, cef_media_source_t*> get_source;

    ///
    /// Returns the sink associated with this route.
    ///
    public delegate* unmanaged<cef_media_route_t*, cef_media_sink_t*> get_sink;

    ///
    /// Send a message over this route. |message| will be copied if necessary.
    ///
    public delegate* unmanaged<cef_media_route_t*, void*, nuint, void> send_route_message;

    ///
    /// Terminate this route. Will result in an asynchronous call to
    /// cef_media_observer_t::OnRoutes on all registered observers.
    ///
    public delegate* unmanaged<cef_media_route_t*, void> terminate;
}

///
/// Callback structure for cef_media_router_t::CreateRoute. The functions of
/// this structure will be called on the browser process UI thread.
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_media_route_create_callback_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Method that will be executed when the route creation has finished.
    /// |result| will be CEF_MRCR_OK if the route creation succeeded. |error| will
    /// be a description of the error if the route creation failed. |route| is the
    /// resulting route, or NULL if the route creation failed.
    ///
    public delegate* unmanaged<cef_media_route_create_callback_t*, cef_media_route_create_result_t, cef_string_t*, cef_media_route_t*, void> on_media_route_create_finished;
}

///
/// Represents a sink to which media can be routed. Instances of this object are
/// retrieved via cef_media_observer_t::OnSinks. The functions of this structure
/// may be called on any browser process thread unless otherwise indicated.
///
/// NOTE: This struct is allocated DLL-side.
///
public unsafe struct cef_media_sink_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Returns the ID for this sink.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_media_sink_t*, cef_string_t*> get_id;

    ///
    /// Returns the name of this sink.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_media_sink_t*, cef_string_t*> get_name;

    ///
    /// Returns the icon type for this sink.
    ///
    public delegate* unmanaged<cef_media_sink_t*, cef_media_sink_icon_type_t> get_icon_type;

    ///
    /// Asynchronously retrieves device info.
    ///
    public delegate* unmanaged<cef_media_sink_t*, cef_media_sink_device_info_callback_t*, void> get_device_info;

    ///
    /// Returns true (1) if this sink accepts content via Cast.
    ///
    public delegate* unmanaged<cef_media_sink_t*, int> is_cast_sink;

    ///
    /// Returns true (1) if this sink accepts content via DIAL.
    ///
    public delegate* unmanaged<cef_media_sink_t*, int> is_dial_sink;

    ///
    /// Returns true (1) if this sink is compatible with |source|.
    ///
    public delegate* unmanaged<cef_media_sink_t*, cef_media_source_t*, int> is_compatible_with;
}

///
/// Callback structure for cef_media_sink_t::GetDeviceInfo. The functions of
/// this structure will be called on the browser process UI thread.
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_media_sink_device_info_callback_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Method that will be executed asyncronously once device information has
    /// been retrieved.
    ///
    public delegate* unmanaged<cef_media_sink_device_info_callback_t*, cef_media_sink_device_info_t*, void> on_media_sink_device_info;
}

///
/// Represents a source from which media can be routed. Instances of this object
/// are retrieved via cef_media_router_t::GetSource. The functions of this
/// structure may be called on any browser process thread unless otherwise
/// indicated.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_media_source_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Returns the ID (media source URN or URL) for this source.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_media_source_t*, cef_string_t*> get_id;

    ///
    /// Returns true (1) if this source outputs its content via Cast.
    ///
    public delegate* unmanaged<cef_media_source_t*, int> is_cast_source;

    ///
    /// Returns true (1) if this source outputs its content via DIAL.
    ///
    public delegate* unmanaged<cef_media_source_t*, int> is_dial_source;
}
