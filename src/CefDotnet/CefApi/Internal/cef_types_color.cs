namespace CefDotnet.CefApi.Internal;

///
/// Describes how to interpret the components of a pixel.
///
public enum cef_color_type_t : int
{
    ///
    /// RGBA with 8 bits per pixel (32bits total).
    ///
    CEF_COLOR_TYPE_RGBA_8888,

    ///
    /// BGRA with 8 bits per pixel (32bits total).
    ///
    CEF_COLOR_TYPE_BGRA_8888,

    CEF_COLOR_TYPE_NUM_VALUES,
}