using System.Runtime.InteropServices;

namespace CefDotnet.CefApi.Types;

///
/// Structure representing a range.
///
[StructLayout(LayoutKind.Sequential)]
public struct cef_range_t
{
    public uint from;
    public uint to;
}
