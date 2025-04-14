using CefDotnet.CefApi.Types;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// Callback structure for cef_browser_host_t::AddDevToolsMessageObserver. The
/// functions of this structure will be called on the browser process UI thread.
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_dev_tools_message_observer_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Method that will be called on receipt of a DevTools protocol message.
    /// |browser| is the originating browser instance. |message| is a UTF8-encoded
    /// JSON dictionary representing either a function result or an event.
    /// |message| is only valid for the scope of this callback and should be
    /// copied if necessary. Return true (1) if the message was handled or false
    /// (0) if the message should be further processed and passed to the
    /// OnDevToolsMethodResult or OnDevToolsEvent functions as appropriate.
    ///
    /// Method result dictionaries include an "id" (int) value that identifies the
    /// orginating function call sent from
    /// cef_browser_host_t::SendDevToolsMessage, and optionally either a "result"
    /// (dictionary) or "error" (dictionary) value. The "error" dictionary will
    /// contain "code" (int) and "message" (string) values. Event dictionaries
    /// include a "function" (string) value and optionally a "params" (dictionary)
    /// value. See the DevTools protocol documentation at
    /// https://chromedevtools.github.io/devtools-protocol/ for details of
    /// supported function calls and the expected "result" or "params" dictionary
    /// contents. JSON dictionaries can be parsed using the CefParseJSON function
    /// if desired, however be aware of performance considerations when parsing
    /// large messages (some of which may exceed 1MB in size).
    ///

    public delegate* unmanaged<cef_dev_tools_message_observer_t*, cef_browser_t*, void*, nuint, int> on_dev_tools_message;

    ///
    /// Method that will be called after attempted execution of a DevTools
    /// protocol function. |browser| is the originating browser instance.
    /// |message_id| is the "id" value that identifies the originating function
    /// call message. If the function succeeded |success| will be true (1) and
    /// |result| will be the UTF8-encoded JSON "result" dictionary value (which
    /// may be NULL). If the function failed |success| will be false (0) and
    /// |result| will be the UTF8-encoded JSON "error" dictionary value. |result|
    /// is only valid for the scope of this callback and should be copied if
    /// necessary. See the OnDevToolsMessage documentation for additional details
    /// on |result| contents.
    ///
    public delegate* unmanaged<cef_dev_tools_message_observer_t*, cef_browser_t*, int, int, void*, nuint, void> on_dev_tools_method_result;

    ///
    /// Method that will be called on receipt of a DevTools protocol event.
    /// |browser| is the originating browser instance. |function| is the
    /// "function" value. |params| is the UTF8-encoded JSON "params" dictionary
    /// value (which may be NULL). |params| is only valid for the scope of this
    /// callback and should be copied if necessary. See the OnDevToolsMessage
    /// documentation for additional details on |params| contents.
    ///
    public delegate* unmanaged<cef_dev_tools_message_observer_t*, cef_browser_t*, cef_string_t*, void*, nuint, void> on_dev_tools_event;

    ///
    /// Method that will be called when the DevTools agent has attached. |browser|
    /// is the originating browser instance. This will generally occur in response
    /// to the first message sent while the agent is detached.
    ///
    public delegate* unmanaged<cef_dev_tools_message_observer_t*, cef_browser_t*, void> on_dev_tools_agent_attached;

    ///
    /// Method that will be called when the DevTools agent has detached. |browser|
    /// is the originating browser instance. Any function results that were
    /// pending before the agent became detached will not be delivered, and any
    /// active event subscriptions will be canceled.
    ///
    public delegate* unmanaged<cef_dev_tools_message_observer_t*, cef_browser_t*, void> on_dev_tools_agent_detached;    
}