using CefDotnet.CefApi.Types;
using CefDotnet.CefApi.Values;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// Structure representing a message. Can be used on any process and thread.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_process_message_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Returns true (1) if this object is valid. Do not call any other functions
    /// if this function returns false (0).
    ///
    public delegate* unmanaged<cef_process_message_t*, int> is_valid;

    ///
    /// Returns true (1) if the values of this object are read-only. Some APIs may
    /// expose read-only objects.
    ///
    public delegate* unmanaged<cef_process_message_t*, int> is_read_only;

    ///
    /// Returns a writable copy of this object. Returns nullptr when message
    /// contains a shared memory region.
    ///
    public delegate* unmanaged<cef_process_message_t*, cef_process_message_t*> copy;

    ///
    /// Returns the message name.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_process_message_t*, cef_string_t*> get_name;

    ///
    /// Returns the list of arguments. Returns nullptr when message contains a
    /// shared memory region.
    ///
    public delegate* unmanaged<cef_process_message_t*, cef_list_value_t*> get_argument_list;

    ///
    /// Returns the shared memory region. Returns nullptr when message contains an
    /// argument list.
    ///
    public delegate* unmanaged<cef_process_message_t*, cef_shared_memory_region_t*> get_shared_memory_region;
}
