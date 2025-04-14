using CefDotnet.CefApi.Internal;
using CefDotnet.CefApi.Types;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// Structure used to represent drag data. The functions of this structure may
/// be called on any thread.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_drag_data_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Returns a copy of the current object.
    ///
    public delegate* unmanaged<cef_drag_data_t*, cef_drag_data_t*> clone;

    ///
    /// Returns true (1) if this object is read-only.
    ///
    public delegate* unmanaged<cef_drag_data_t*, int> is_read_only;

    ///
    /// Returns true (1) if the drag data is a link.
    ///
    public delegate* unmanaged<cef_drag_data_t*, int> is_link;

    ///
    /// Returns true (1) if the drag data is a text or html fragment.
    ///
    public delegate* unmanaged<cef_drag_data_t*, int> is_fragment;

    ///
    /// Returns true (1) if the drag data is a file.
    ///
    public delegate* unmanaged<cef_drag_data_t*, int> is_file;

    ///
    /// Return the link URL that is being dragged.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_drag_data_t*, cef_string_t*> get_link_url;

    ///
    /// Return the title associated with the link being dragged.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_drag_data_t*, cef_string_t*> get_link_title;

    ///
    /// Return the metadata, if any, associated with the link being dragged.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_drag_data_t*, cef_string_t*> get_link_metadata;

    ///
    /// Return the plain text fragment that is being dragged.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_drag_data_t*, cef_string_t*> get_fragment_text;

    ///
    /// Return the text/html fragment that is being dragged.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_drag_data_t*, cef_string_t*> get_fragment_html;

    ///
    /// Return the base URL that the fragment came from. This value is used for
    /// resolving relative URLs and may be NULL.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_drag_data_t*, cef_string_t*> get_fragment_base_url;

    ///
    /// Return the name of the file being dragged out of the browser window.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_drag_data_t*, cef_string_t*> get_file_name;

    ///
    /// Write the contents of the file being dragged out of the web view into
    /// |writer|. Returns the number of bytes sent to |writer|. If |writer| is
    /// NULL this function will return the size of the file contents in bytes.
    /// Call get_file_name() to get a suggested name for the file.
    ///
    public delegate* unmanaged<cef_drag_data_t*, cef_stream_writer_t*, nuint> get_file_contents;

    ///
    /// Retrieve the list of file names that are being dragged into the browser
    /// window.
    ///
    public delegate* unmanaged<cef_drag_data_t*, nint, int> get_file_names;

    ///
    /// Retrieve the list of file paths that are being dragged into the browser
    /// window.
    ///
    public delegate* unmanaged<cef_drag_data_t*, nint, int> get_file_paths;

    ///
    /// Set the link URL that is being dragged.
    ///
    public delegate* unmanaged<cef_drag_data_t*, cef_string_t*, void> set_link_url;

    ///
    /// Set the title associated with the link being dragged.
    ///
    public delegate* unmanaged<cef_drag_data_t*, cef_string_t*, void> set_link_title;

    ///
    /// Set the metadata associated with the link being dragged.
    ///
    public delegate* unmanaged<cef_drag_data_t*, cef_string_t*, void> set_link_metadata;

    ///
    /// Set the plain text fragment that is being dragged.
    ///
    public delegate* unmanaged<cef_drag_data_t*, cef_string_t*, void> set_fragment_text;

    ///
    /// Set the text/html fragment that is being dragged.
    ///
    public delegate* unmanaged<cef_drag_data_t*, cef_string_t*, void> set_fragment_html;

    ///
    /// Set the base URL that the fragment came from.
    ///
    public delegate* unmanaged<cef_drag_data_t*, cef_string_t*, void> set_fragment_base_url;

    ///
    /// Reset the file contents. You should do this before calling
    /// cef_browser_host_t::DragTargetDragEnter as the web view does not allow us
    /// to drag in this kind of data.
    ///
    public delegate* unmanaged<cef_drag_data_t*, void> reset_file_contents;

    ///
    /// Add a file that is being dragged into the webview.
    ///
    public delegate* unmanaged<cef_drag_data_t*, cef_string_t*, cef_string_t*, void> add_file;

    ///
    /// Clear list of filenames.
    ///
    public delegate* unmanaged<cef_drag_data_t*, void> clear_filenames;

    ///
    /// Get the image representation of drag data. May return NULL if no image
    /// representation is available.
    ///
    public delegate* unmanaged<cef_drag_data_t*, cef_image_t*> get_image;

    ///
    /// Get the image hotspot (drag start location relative to image dimensions).
    ///
    public delegate* unmanaged<cef_drag_data_t*, cef_point_t> get_image_hotspot;

  ///
  /// Returns true (1) if an image representation of drag data is available.
  ///
    public delegate* unmanaged<cef_drag_data_t*, int> has_image;
}