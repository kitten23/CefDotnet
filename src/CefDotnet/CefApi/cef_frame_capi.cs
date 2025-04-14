using CefDotnet.CefApi.Types;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// Structure used to represent a frame in the browser window. When used in the
/// browser process the functions of this structure may be called on any thread
/// unless otherwise indicated in the comments. When used in the render process
/// the functions of this structure may only be called on the main thread.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_frame_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// True if this object is currently attached to a valid frame.
    ///
    public delegate* unmanaged<cef_frame_t*, int> is_valid;

    ///
    /// Execute undo in this frame.
    ///
    public delegate* unmanaged<cef_frame_t*, void> undo;

    ///
    /// Execute redo in this frame.
    ///
    public delegate* unmanaged<cef_frame_t*, void> redo;

    ///
    /// Execute cut in this frame.
    ///
    public delegate* unmanaged<cef_frame_t*, void> cut;

    ///
    /// Execute copy in this frame.
    ///
    public delegate* unmanaged<cef_frame_t*, void> copy;

    ///
    /// Execute paste in this frame.
    ///
    public delegate* unmanaged<cef_frame_t*, void> paste;

    ///
    /// Execute paste and match style in this frame.
    ///
    public delegate* unmanaged<cef_frame_t*, void> paste_and_match_style;

    ///
    /// Execute delete in this frame.
    ///
    public delegate* unmanaged<cef_frame_t*, void> del;

    ///
    /// Execute select all in this frame.
    ///
    public delegate* unmanaged<cef_frame_t*, void> select_all;

    ///
    /// Save this frame's HTML source to a temporary file and open it in the
    /// default text viewing application. This function can only be called from
    /// the browser process.
    ///
    public delegate* unmanaged<cef_frame_t*, void> view_source;

    ///
    /// Retrieve this frame's HTML source as a string sent to the specified
    /// visitor.
    ///
    public delegate* unmanaged<cef_frame_t*, cef_string_visitor_t*, void> get_source;

    ///
    /// Retrieve this frame's display text as a string sent to the specified
    /// visitor.
    ///
    public delegate* unmanaged<cef_frame_t*, cef_string_visitor_t*, void> get_text;

    ///
    /// Load the request represented by the |request| object.
    ///
    /// WARNING: This function will fail with "bad IPC message" reason
    /// INVALID_INITIATOR_ORIGIN (213) unless you first navigate to the request
    /// origin using some other mechanism (LoadURL, link click, etc).
    ///
    public delegate* unmanaged<cef_frame_t*, cef_request_t*, void> load_request;

    ///
    /// Load the specified |url|.
    ///
    public delegate* unmanaged<cef_frame_t*, cef_string_t*, void> load_url;

    ///
    /// Execute a string of JavaScript code in this frame. The |script_url|
    /// parameter is the URL where the script in question can be found, if any.
    /// The renderer may request this URL to show the developer the source of the
    /// error.  The |start_line| parameter is the base line number to use for
    /// error reporting.
    ///
    public delegate* unmanaged<cef_frame_t*, cef_string_t*, cef_string_t*, int, void> execute_java_script;

    ///
    /// Returns true (1) if this is the main (top-level) frame.
    ///
    public delegate* unmanaged<cef_frame_t*, int> is_main;

    ///
    /// Returns true (1) if this is the focused frame.
    ///
    public delegate* unmanaged<cef_frame_t*, int> is_focused;

    ///
    /// Returns the name for this frame. If the frame has an assigned name (for
    /// example, set via the iframe "name" attribute) then that value will be
    /// returned. Otherwise a unique name will be constructed based on the frame
    /// parent hierarchy. The main (top-level) frame will always have an NULL name
    /// value.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_frame_t*, cef_string_t*> get_name;

    ///
    /// Returns the globally unique identifier for this frame or NULL if the
    /// underlying frame does not yet exist.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_frame_t*, cef_string_t*> get_identifier;

    ///
    /// Returns the parent of this frame or NULL if this is the main (top-level)
    /// frame.
    ///
    public delegate* unmanaged<cef_frame_t*, cef_frame_t*> get_parent;

    ///
    /// Returns the URL currently loaded in this frame.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_frame_t*, cef_string_t*> get_url;

    ///
    /// Returns the browser that this frame belongs to.
    ///
    public delegate* unmanaged<cef_frame_t*, cef_browser_t*> get_browser;

    ///
    /// Get the V8 context associated with the frame. This function can only be
    /// called from the render process.
    ///
    public delegate* unmanaged<cef_frame_t*, cef_v8_context_t*> get_v8_context;

    ///
    /// Visit the DOM document. This function can only be called from the render
    /// process.
    ///
    public delegate* unmanaged<cef_frame_t*, cef_domvisitor_t*, void> visit_dom;

    ///
    /// Create a new URL request that will be treated as originating from this
    /// frame and the associated browser. Use cef_urlrequest_t::Create instead if
    /// you do not want the request to have this association, in which case it may
    /// be handled differently (see documentation on that function). A request
    /// created with this function may only originate from the browser process,
    /// and will behave as follows:
    ///   - It may be intercepted by the client via CefResourceRequestHandler or
    ///     CefSchemeHandlerFactory.
    ///   - POST data may only contain a single element of type PDE_TYPE_FILE or
    ///     PDE_TYPE_BYTES.
    ///
    /// The |request| object will be marked as read-only after calling this
    /// function.
    ///
    public delegate* unmanaged<cef_frame_t*, cef_request_t*, cef_urlrequest_client_t*, cef_urlrequest_t*> create_urlrequest;

    ///
    /// Send a message to the specified |target_process|. Ownership of the message
    /// contents will be transferred and the |message| reference will be
    /// invalidated. Message delivery is not guaranteed in all cases (for example,
    /// if the browser is closing, navigating, or if the target process crashes).
    /// Send an ACK message back from the target process if confirmation is
    /// required.
    ///
    public delegate* unmanaged<cef_frame_t*, cef_process_id_t, cef_process_message_t*, void> send_process_message;
}