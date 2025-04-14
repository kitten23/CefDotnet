using CefDotnet.CefApi.Types;
using CefDotnet.CefApi.Values;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// Structure that wraps platform-dependent share memory region mapping.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_shared_memory_region_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Returns true (1) if the mapping is valid.
    ///
    public delegate* unmanaged<cef_shared_memory_region_t*, int> is_valid;

    ///
    /// Returns the size of the mapping in bytes. Returns 0 for invalid instances.
    ///
    public delegate* unmanaged<cef_shared_memory_region_t*, nuint> size;

    ///
    /// Returns the pointer to the memory. Returns nullptr for invalid instances.
    /// The returned pointer is only valid for the life span of this object.
    ///
    public delegate* unmanaged<cef_shared_memory_region_t*, void*> memory;
}
