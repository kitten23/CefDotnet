using System.Runtime.InteropServices;

namespace CefDotnet.CefApi.Internal;

///
/// Structure representing a point.
///
[StructLayout(LayoutKind.Sequential)]
public struct cef_point_t
{
    public int x;
    public int y;
}

///
/// Structure representing a rectangle.
///
[StructLayout(LayoutKind.Sequential)]
public struct cef_rect_t
{
    public int x;
    public int y;
    public int width;
    public int height;
}

///
/// Structure representing a size.
///
[StructLayout(LayoutKind.Sequential)]
public struct cef_size_t
{
    public int width;
    public int height;
}

///
/// Structure representing insets.
///
[StructLayout(LayoutKind.Sequential)]
public struct cef_insets_t
{
    public int top;
    public int left;
    public int bottom;
    public int right;
}
