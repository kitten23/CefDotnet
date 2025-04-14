using CefDotnet.CefApi.Internal;
using CefDotnet.CefApi.Types;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// Describes how to interpret the alpha component of a pixel.
/// 
public enum cef_alpha_type_t
{
    ///
    /// No transparency. The alpha component is ignored.
    ///
    CEF_ALPHA_TYPE_OPAQUE,

    ///
    /// Transparency with pre-multiplied alpha component.
    ///
    CEF_ALPHA_TYPE_PREMULTIPLIED,

    ///
    /// Transparency with post-multiplied alpha component.
    ///
    CEF_ALPHA_TYPE_POSTMULTIPLIED,
}

///
/// Container for a single image represented at different scale factors. All
/// image representations should be the same size in density independent pixel
/// (DIP) units. For example, if the image at scale factor 1.0 is 100x100 pixels
/// then the image at scale factor 2.0 should be 200x200 pixels -- both images
/// will display with a DIP size of 100x100 units. The functions of this
/// structure can be called on any browser process thread.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_image_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Returns true (1) if this Image is NULL.
    ///
    public delegate* unmanaged<cef_image_t*, int> is_empty;

    ///
    /// Returns true (1) if this Image and |that| Image share the same underlying
    /// storage. Will also return true (1) if both images are NULL.
    ///
    public delegate* unmanaged<cef_image_t*, cef_image_t*, int> is_same;

    ///
    /// Add a bitmap image representation for |scale_factor|. Only 32-bit
    /// RGBA/BGRA formats are supported. |pixel_width| and |pixel_height| are the
    /// bitmap representation size in pixel coordinates. |pixel_data| is the array
    /// of pixel data and should be |pixel_width| x |pixel_height| x 4 bytes in
    /// size. |color_type| and |alpha_type| values specify the pixel format.
    ///
    public delegate* unmanaged<cef_image_t*, float, int, int, cef_color_type_t, cef_alpha_type_t, void*, nuint, int> add_bitmap;

    ///
    /// Add a PNG image representation for |scale_factor|. |png_data| is the image
    /// data of size |png_data_size|. Any alpha transparency in the PNG data will
    /// be maintained.
    ///
    public delegate* unmanaged<cef_image_t*, float, void*, nuint, int> add_png;

    ///
    /// Create a JPEG image representation for |scale_factor|. |jpeg_data| is the
    /// image data of size |jpeg_data_size|. The JPEG format does not support
    /// transparency so the alpha byte will be set to 0xFF for all pixels.
    ///
    public delegate* unmanaged<cef_image_t*, float, void*, nuint, int> add_jpeg;

    ///
    /// Returns the image width in density independent pixel (DIP) units.
    ///
    public delegate* unmanaged<cef_image_t*, nuint> get_width;

    ///
    /// Returns the image height in density independent pixel (DIP) units.
    ///
    public delegate* unmanaged<cef_image_t*, nuint> get_height;

    ///
    /// Returns true (1) if this image contains a representation for
    /// |scale_factor|.
    ///
    public delegate* unmanaged<cef_image_t*, float, int> has_representation;

    ///
    /// Removes the representation for |scale_factor|. Returns true (1) on
    /// success.
    ///
    public delegate* unmanaged<cef_image_t*, float, int> remove_representation;

    ///
    /// Returns information for the representation that most closely matches
    /// |scale_factor|. |actual_scale_factor| is the actual scale factor for the
    /// representation. |pixel_width| and |pixel_height| are the representation
    /// size in pixel coordinates. Returns true (1) on success.
    ///
    public delegate* unmanaged<cef_image_t*, float, float*, int*, int*, int> get_representation_info;

    ///
    /// Returns the bitmap representation that most closely matches
    /// |scale_factor|. Only 32-bit RGBA/BGRA formats are supported. |color_type|
    /// and |alpha_type| values specify the desired output pixel format.
    /// |pixel_width| and |pixel_height| are the output representation size in
    /// pixel coordinates. Returns a cef_binary_value_t containing the pixel data
    /// on success or NULL on failure.
    ///
    public delegate* unmanaged<cef_image_t*, float, cef_color_type_t, cef_alpha_type_t, int*, int*, cef_binary_value_t*> get_as_bitmap;

    ///
    /// Returns the PNG representation that most closely matches |scale_factor|.
    /// If |with_transparency| is true (1) any alpha transparency in the image
    /// will be represented in the resulting PNG data. |pixel_width| and
    /// |pixel_height| are the output representation size in pixel coordinates.
    /// Returns a cef_binary_value_t containing the PNG image data on success or
    /// NULL on failure.
    ///
    public delegate* unmanaged<cef_image_t*, float, int, int*, int*, cef_binary_value_t*> get_as_png;

    ///
    /// Returns the JPEG representation that most closely matches |scale_factor|.
    /// |quality| determines the compression level with 0 == lowest and 100 ==
    /// highest. The JPEG format does not support alpha transparency and the alpha
    /// channel, if any, will be discarded. |pixel_width| and |pixel_height| are
    /// the output representation size in pixel coordinates. Returns a
    /// cef_binary_value_t containing the JPEG image data on success or NULL on
    /// failure.
    ///
    public delegate* unmanaged<cef_image_t*, float, int, int*, int*, cef_binary_value_t*> get_as_jpeg;
}