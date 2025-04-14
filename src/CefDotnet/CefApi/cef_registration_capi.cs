using CefDotnet.CefApi.Types;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// Generic callback structure used for managing the lifespan of a registration.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_registration_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;
}
