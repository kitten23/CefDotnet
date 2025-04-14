using CefDotnet.CefApi.Internal;
using CefDotnet.CefApi.Types;
using CefDotnet.CefApi.Values;
using System;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// Specifies the zoom commands supported by CefBrowserHost::Zoom.
///
public enum cef_zoom_command_t
{
    CEF_ZOOM_COMMAND_OUT,
    CEF_ZOOM_COMMAND_RESET,
    CEF_ZOOM_COMMAND_IN,
}

///
/// Margin type for PDF printing.
///
public enum cef_pdf_print_margin_type_t
{
    ///
    /// Default margins of 1cm (~0.4 inches).
    ///
    PDF_PRINT_MARGIN_DEFAULT,

    ///
    /// No margins.
    ///
    PDF_PRINT_MARGIN_NONE,

    ///
    /// Custom margins using the |margin_*| values from cef_pdf_print_settings_t.
    ///
    PDF_PRINT_MARGIN_CUSTOM,
}

///
/// Structure representing PDF print settings. These values match the parameters
/// supported by the DevTools Page.printToPDF function. See
/// https://chromedevtools.github.io/devtools-protocol/tot/Page/#method-printToPDF
///
[StructLayout(LayoutKind.Sequential)]
public unsafe ref struct cef_pdf_print_settings_t
{
    public cef_pdf_print_settings_t()
    {
        size = (nuint)sizeof(cef_pdf_print_settings_t);
    }

    ///
    /// Size of this structure.
    ///
    nuint size;

    ///
    /// Set to true (1) for landscape mode or false (0) for portrait mode.
    ///
    public int landscape;

    ///
    /// Set to true (1) to print background graphics.
    ///
    public int print_background;

    ///
    /// The percentage to scale the PDF by before printing (e.g. .5 is 50%).
    /// If this value is less than or equal to zero the default value of 1.0
    /// will be used.
    ///
    public double scale;

    ///
    /// Output paper size in inches. If either of these values is less than or
    /// equal to zero then the default paper size (letter, 8.5 x 11 inches) will
    /// be used.
    ///
    public double paper_width;
    public double paper_height;

    ///
    /// Set to true (1) to prefer page size as defined by css. Defaults to false
    /// (0), in which case the content will be scaled to fit the paper size.
    ///
    public int prefer_css_page_size;

    ///
    /// Margin type.
    ///
    public cef_pdf_print_margin_type_t margin_type;

    ///
    /// Margins in inches. Only used if |margin_type| is set to
    /// PDF_PRINT_MARGIN_CUSTOM.
    ///
    public double margin_top;
    public double margin_right;
    public double margin_bottom;
    public double margin_left;

    ///
    /// Paper ranges to print, one based, e.g., '1-5, 8, 11-13'. Pages are printed
    /// in the document order, not in the order specified, and no more than once.
    /// Defaults to empty string, which implies the entire document is printed.
    /// The page numbers are quietly capped to actual page count of the document,
    /// and ranges beyond the end of the document are ignored. If this results in
    /// no pages to print, an error is reported. It is an error to specify a range
    /// with start greater than end.
    ///
    public cef_string_t page_ranges;

    ///
    /// Set to true (1) to display the header and/or footer. Modify
    /// |header_template| and/or |footer_template| to customize the display.
    ///
    public int display_header_footer;

    ///
    /// HTML template for the print header. Only displayed if
    /// |display_header_footer| is true (1). Should be valid HTML markup with
    /// the following classes used to inject printing values into them:
    ///
    /// - date: formatted print date
    /// - title: document title
    /// - url: document location
    /// - pageNumber: current page number
    /// - totalPages: total pages in the document
    ///
    /// For example, "<span class=title></span>" would generate a span containing
    /// the title.
    ///
    public cef_string_t header_template;

    ///
    /// HTML template for the print footer. Only displayed if
    /// |display_header_footer| is true (1). Uses the same format as
    /// |header_template|.
    ///
    public cef_string_t footer_template;

    ///
    /// Set to true (1) to generate tagged (accessible) PDF.
    ///
    public int generate_tagged_pdf;

    ///
    /// Set to true (1) to generate a document outline.
    ///
    public int generate_document_outline;
}

///
/// Mouse button types.
///
public enum cef_mouse_button_type_t
{
    MBT_LEFT = 0,
    MBT_MIDDLE,
    MBT_RIGHT,
}

///
/// Touch points states types.
///
public enum cef_touch_event_type_t
{
    CEF_TET_RELEASED = 0,
    CEF_TET_PRESSED,
    CEF_TET_MOVED,
    CEF_TET_CANCELLED
}

///
/// The device type that caused the event.
///
public enum cef_pointer_type_t
{
    CEF_POINTER_TYPE_TOUCH = 0,
    CEF_POINTER_TYPE_MOUSE,
    CEF_POINTER_TYPE_PEN,
    CEF_POINTER_TYPE_ERASER,
    CEF_POINTER_TYPE_UNKNOWN
}

///
/// Structure representing touch event information.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_touch_event_t
{
    ///
    /// Id of a touch point. Must be unique per touch, can be any number except
    /// -1. Note that a maximum of 16 concurrent touches will be tracked; touches
    /// beyond that will be ignored.
    ///
    public int id;

    ///
    /// X coordinate relative to the left side of the view.
    ///
    public float x;

    ///
    /// Y coordinate relative to the top side of the view.
    ///
    public float y;

    ///
    /// X radius in pixels. Set to 0 if not applicable.
    ///
    public float radius_x;

    ///
    /// Y radius in pixels. Set to 0 if not applicable.
    ///
    public float radius_y;

    ///
    /// Rotation angle in radians. Set to 0 if not applicable.
    ///
    public float rotation_angle;

    ///
    /// The normalized pressure of the pointer input in the range of [0,1].
    /// Set to 0 if not applicable.
    ///
    public float pressure;

    ///
    /// The state of the touch point. Touches begin with one CEF_TET_PRESSED event
    /// followed by zero or more CEF_TET_MOVED events and finally one
    /// CEF_TET_RELEASED or CEF_TET_CANCELLED event. Events not respecting this
    /// order will be ignored.
    ///
    public cef_touch_event_type_t type;

    ///
    /// Bit flags describing any pressed modifier keys. See
    /// cef_event_flags_t for values.
    ///
    public uint modifiers;

    ///
    /// The device type that caused the event.
    ///
    public cef_pointer_type_t pointer_type;
}

///
/// Structure representing mouse event information.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_mouse_event_t
{
    ///
    /// X coordinate relative to the left side of the view.
    ///
    int x;

    ///
    /// Y coordinate relative to the top side of the view.
    ///
    int y;

    ///
    /// Bit flags describing any pressed modifier keys. See
    /// cef_event_flags_t for values.
    ///
    uint modifiers;
}

///
/// Composition underline style.
///
public enum cef_composition_underline_style_t
{
    CEF_CUS_SOLID,
    CEF_CUS_DOT,
    CEF_CUS_DASH,
    CEF_CUS_NONE,
    CEF_CUS_NUM_VALUES,
}

///
/// Structure representing IME composition underline information. This is a thin
/// wrapper around Blink's WebCompositionUnderline class and should be kept in
/// sync with that.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_composition_underline_t
{
    public cef_composition_underline_t()
    {
        size = (nuint)sizeof(cef_composition_underline_t);
    }

    ///
    /// Size of this structure.
    ///
    nuint size;

    ///
    /// Underline character range.
    ///
    public cef_range_t range;

    ///
    /// Text color.
    ///
    public uint color;

    ///
    /// Background color.
    ///
    public uint background_color;

    ///
    /// Set to true (1) for thick underline.
    ///
    public int thick;

    ///
    /// Style.
    ///
    public cef_composition_underline_style_t style;
}

///
/// Structure used to represent a browser. When used in the browser process the
/// functions of this structure may be called on any thread unless otherwise
/// indicated in the comments. When used in the render process the functions of
/// this structure may only be called on the main thread.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_browser_t
{
    ///
    /// Base structure.
    ///
    public cef_base_ref_counted_t @base;

    ///
    /// True if this object is currently valid. This will return false (0) after
    /// cef_life_span_handler_t::OnBeforeClose is called.
    ///
    public delegate* unmanaged<cef_browser_t*, int> is_valid;

    ///
    /// Returns the browser host object. This function can only be called in the
    /// browser process.
    ///
    public delegate* unmanaged<cef_browser_t*, cef_browser_host_t*> get_host;

    ///
    /// Returns true (1) if the browser can navigate backwards.
    ///
    public delegate* unmanaged<cef_browser_t*, int> can_go_back;

    ///
    /// Navigate backwards.
    ///
    public delegate* unmanaged<cef_browser_t*, void> go_back;

    ///
    /// Returns true (1) if the browser can navigate forwards.
    ///
    public delegate* unmanaged<cef_browser_t*, int> can_go_forward;

    ///
    /// Navigate forwards.
    ///
    public delegate* unmanaged<cef_browser_t*, void> go_forward;

    ///
    /// Returns true (1) if the browser is currently loading.
    ///
    public delegate* unmanaged<cef_browser_t*, int> is_loading;

    ///
    /// Reload the current page.
    ///
    public delegate* unmanaged<cef_browser_t*, void> reload;

    ///
    /// Reload the current page ignoring any cached data.
    ///
    public delegate* unmanaged<cef_browser_t*, void> reload_ignore_cache;

    ///
    /// Stop loading the page.
    ///
    public delegate* unmanaged<cef_browser_t*, void> stop_load;

    ///
    /// Returns the globally unique identifier for this browser. This value is
    /// also used as the tabId for extension APIs.
    ///
    public delegate* unmanaged<cef_browser_t*, int> get_identifier;

    ///
    /// Returns true (1) if this object is pointing to the same handle as |that|
    /// object.
    ///
    public delegate* unmanaged<cef_browser_t*, cef_browser_t*, int> is_same;

    ///
    /// Returns true (1) if the browser is a popup.
    ///
    public delegate* unmanaged<cef_browser_t*, int> is_popup;

    ///
    /// Returns true (1) if a document has been loaded in the browser.
    ///
    public delegate* unmanaged<cef_browser_t*, int> has_document;

    ///
    /// Returns the main (top-level) frame for the browser. In the browser process
    /// this will return a valid object until after
    /// cef_life_span_handler_t::OnBeforeClose is called. In the renderer process
    /// this will return NULL if the main frame is hosted in a different renderer
    /// process (e.g. for cross-origin sub-frames). The main frame object will
    /// change during cross-origin navigation or re-navigation after renderer
    /// process termination (due to crashes, etc).
    ///
    public delegate* unmanaged<cef_browser_t*, cef_frame_t*> get_main_frame;

    ///
    /// Returns the focused frame for the browser.
    ///
    public delegate* unmanaged<cef_browser_t*, cef_frame_t*> get_focused_frame;

    ///
    /// Returns the frame with the specified identifier, or NULL if not found.
    ///
    public delegate* unmanaged<cef_browser_t*, cef_string_t*, cef_frame_t*> get_frame_by_identifier;

    ///
    /// Returns the frame with the specified name, or NULL if not found.
    ///
    public delegate* unmanaged<cef_browser_t*, cef_string_t*, cef_frame_t*> get_frame_by_name;

    ///
    /// Returns the number of frames that currently exist.
    ///
    public delegate* unmanaged<cef_browser_t*, nuint> get_frame_count;

    ///
    /// Returns the identifiers of all existing frames.
    ///
    public delegate* unmanaged<cef_browser_t*, nint> get_frame_identifiers;

    ///
    /// Returns the names of all existing frames.
    ///
    public delegate* unmanaged<cef_browser_t*, nint, void> get_frame_names;
}

///
/// Callback structure for cef_browser_host_t::RunFileDialog. The functions of
/// this structure will be called on the browser process UI thread.
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_run_file_dialog_callback_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Called asynchronously after the file dialog is dismissed. |file_paths|
    /// will be a single value or a list of values depending on the dialog mode.
    /// If the selection was cancelled |file_paths| will be NULL.
    ///
    public delegate* unmanaged<cef_run_file_dialog_callback_t*, nint, void> on_file_dialog_dismissed;
}

///
/// Callback structure for cef_browser_host_t::GetNavigationEntries. The
/// functions of this structure will be called on the browser process UI thread.
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_navigation_entry_visitor_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Method that will be executed. Do not keep a reference to |entry| outside
    /// of this callback. Return true (1) to continue visiting entries or false
    /// (0) to stop. |current| is true (1) if this entry is the currently loaded
    /// navigation entry. |index| is the 0-based index of this entry and |total|
    /// is the total number of entries.
    ///
    public delegate* unmanaged<cef_navigation_entry_visitor_t*, cef_navigation_entry_t*, int, int, int, int> visit;
}

///
/// Callback structure for cef_browser_host_t::PrintToPDF. The functions of this
/// structure will be called on the browser process UI thread.
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_pdf_print_callback_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Method that will be executed when the PDF printing has completed. |path|
    /// is the output path. |ok| will be true (1) if the printing completed
    /// successfully or false (0) otherwise.
    ///
    public delegate* unmanaged<cef_pdf_print_callback_t*, cef_string_t*, int, void> on_pdf_print_finished;
}

///
/// Callback structure for cef_browser_host_t::DownloadImage. The functions of
/// this structure will be called on the browser process UI thread.
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_download_image_callback_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Method that will be executed when the image download has completed.
    /// |image_url| is the URL that was downloaded and |http_status_code| is the
    /// resulting HTTP status code. |image| is the resulting image, possibly at
    /// multiple scale factors, or NULL if the download failed.
    ///
    public delegate* unmanaged<cef_download_image_callback_t*, cef_string_t*, int, cef_image_t*, void> on_download_image_finished;
}

///
/// Structure used to represent the browser process aspects of a browser. The
/// functions of this structure can only be called in the browser process. They
/// may be called on any thread in that process unless otherwise indicated in
/// the comments.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_browser_host_t
{
    ///
    /// Base structure.
    ///
    public cef_base_ref_counted_t @base;

    ///
    /// Returns the hosted browser object.
    ///
    public delegate* unmanaged<cef_browser_host_t*, cef_browser_t*> get_browser;

    ///
    /// Request that the browser close. Closing a browser is a multi-stage process
    /// that may complete either synchronously or asynchronously, and involves
    /// callbacks such as cef_life_span_handler_t::DoClose (Alloy style only),
    /// cef_life_span_handler_t::OnBeforeClose, and a top-level window close
    /// handler such as cef_window_delegate_t::CanClose (or platform-specific
    /// equivalent). In some cases a close request may be delayed or canceled by
    /// the user. Using try_close_browser() instead of close_browser() is
    /// recommended for most use cases. See cef_life_span_handler_t::do_close()
    /// documentation for detailed usage and examples.
    ///
    /// If |force_close| is false (0) then JavaScript unload handlers, if any, may
    /// be fired and the close may be delayed or canceled by the user. If
    /// |force_close| is true (1) then the user will not be prompted and the close
    /// will proceed immediately (possibly asynchronously). If browser close is
    /// delayed and not canceled the default behavior is to call the top-level
    /// window close handler once the browser is ready to be closed. This default
    /// behavior can be changed for Alloy style browsers by implementing
    /// cef_life_span_handler_t::do_close(). is_ready_to_be_closed() can be used
    /// to detect mandatory browser close events when customizing close behavior
    /// on the browser process UI thread.
    ///
    public delegate* unmanaged<cef_browser_host_t*, int, void> close_browser;

    ///
    /// Helper for closing a browser. This is similar in behavior to
    /// CLoseBrowser(false (0)) but returns a boolean to reflect the immediate
    /// close status. Call this function from a top-level window close handler
    /// such as cef_window_delegate_t::CanClose (or platform-specific equivalent)
    /// to request that the browser close, and return the result to indicate if
    /// the window close should proceed. Returns false (0) if the close will be
    /// delayed (JavaScript unload handlers triggered but still pending) or true
    /// (1) if the close will proceed immediately (possibly asynchronously). See
    /// close_browser() documentation for additional usage information. This
    /// function must be called on the browser process UI thread.
    ///
    public delegate* unmanaged<cef_browser_host_t*, int> try_close_browser;

    ///
    /// Returns true (1) if the browser is ready to be closed, meaning that the
    /// close has already been initiated and that JavaScript unload handlers have
    /// already executed or should be ignored. This can be used from a top-level
    /// window close handler such as cef_window_delegate_t::CanClose (or platform-
    /// specific equivalent) to distringuish between potentially cancelable
    /// browser close events (like the user clicking the top-level window close
    /// button before browser close has started) and mandatory browser close
    /// events (like JavaScript `window.close()` or after browser close has
    /// started in response to [Try]close_browser()). Not completing the browser
    /// close for mandatory close events (when this function returns true (1))
    /// will leave the browser in a partially closed state that interferes with
    /// proper functioning. See close_browser() documentation for additional usage
    /// information. This function must be called on the browser process UI
    /// thread.
    ///
    public delegate* unmanaged<cef_browser_host_t*, int> is_ready_to_be_closed;

    ///
    /// Set whether the browser is focused.
    ///
    public delegate* unmanaged<cef_browser_host_t*, int, void> set_focus;

    ///
    /// Retrieve the window handle (if any) for this browser. If this browser is
    /// wrapped in a cef_browser_view_t this function should be called on the
    /// browser process UI thread and it will return the handle for the top-level
    /// native window.
    ///
    public delegate* unmanaged<cef_browser_host_t*, nint> get_window_handle;

    ///
    /// Retrieve the window handle (if any) of the browser that opened this
    /// browser. Will return NULL for non-popup browsers or if this browser is
    /// wrapped in a cef_browser_view_t. This function can be used in combination
    /// with custom handling of modal windows.
    ///
    public delegate* unmanaged<cef_browser_host_t*, nint> get_opener_window_handle;

    ///
    /// Retrieve the unique identifier of the browser that opened this browser.
    /// Will return 0 for non-popup browsers.
    ///
    public delegate* unmanaged<cef_browser_host_t*, int> get_opener_identifier;

    ///
    /// Returns true (1) if this browser is wrapped in a cef_browser_view_t.
    ///
    public delegate* unmanaged<cef_browser_host_t*, int> has_view;

    ///
    /// Returns the client for this browser.
    ///
    public delegate* unmanaged<cef_browser_host_t*, cef_client_t*> get_client;

    ///
    /// Returns the request context for this browser.
    ///
    public delegate* unmanaged<cef_browser_host_t*, cef_request_context_t*> get_request_context;

    ///
    /// Returns true (1) if this browser can execute the specified zoom command.
    /// This function can only be called on the UI thread.
    ///
    public delegate* unmanaged<cef_browser_host_t*, cef_zoom_command_t, int> can_zoom;

    ///
    /// Execute a zoom command in this browser. If called on the UI thread the
    /// change will be applied immediately. Otherwise, the change will be applied
    /// asynchronously on the UI thread.
    ///
    public delegate* unmanaged<cef_browser_host_t*, cef_zoom_command_t, void> zoom;

    ///
    /// Get the default zoom level. This value will be 0.0 by default but can be
    /// configured. This function can only be called on the UI thread.
    ///
    public delegate* unmanaged<cef_browser_host_t*, double> get_default_zoom_level;

    ///
    /// Get the current zoom level. This function can only be called on the UI
    /// thread.
    ///
    public delegate* unmanaged<cef_browser_host_t*, double> get_zoom_level;

    ///
    /// Change the zoom level to the specified value. Specify 0.0 to reset the
    /// zoom level to the default. If called on the UI thread the change will be
    /// applied immediately. Otherwise, the change will be applied asynchronously
    /// on the UI thread.
    ///
    public delegate* unmanaged<cef_browser_host_t*, double, void> set_zoom_level;

    ///
    /// Call to run a file chooser dialog. Only a single file chooser dialog may
    /// be pending at any given time. |mode| represents the type of dialog to
    /// display. |title| to the title to be used for the dialog and may be NULL to
    /// show the default title ("Open" or "Save" depending on the mode).
    /// |default_file_path| is the path with optional directory and/or file name
    /// component that will be initially selected in the dialog. |accept_filters|
    /// are used to restrict the selectable file types and may any combination of
    /// (a) valid lower-cased MIME types (e.g. "text/*" or "image/*"), (b)
    /// individual file extensions (e.g. ".txt" or ".png"), or (c) combined
    /// description and file extension delimited using "|" and ";" (e.g. "Image
    /// Types|.png;.gif;.jpg"). |callback| will be executed after the dialog is
    /// dismissed or immediately if another dialog is already pending. The dialog
    /// will be initiated asynchronously on the UI thread.
    ///
    public delegate* unmanaged<cef_browser_host_t*, cef_file_dialog_mode_t, cef_string_t*, cef_string_t*, nint, cef_run_file_dialog_callback_t*, void> run_file_dialog;

    ///
    /// Download the file at |url| using cef_download_handler_t.
    ///
    public delegate* unmanaged<cef_browser_host_t*, cef_string_t*, void> start_download;

    ///
    /// Download |image_url| and execute |callback| on completion with the images
    /// received from the renderer. If |is_favicon| is true (1) then cookies are
    /// not sent and not accepted during download. Images with density independent
    /// pixel (DIP) sizes larger than |max_image_size| are filtered out from the
    /// image results. Versions of the image at different scale factors may be
    /// downloaded up to the maximum scale factor supported by the system. If
    /// there are no image results <= |max_image_size| then the smallest image is
    /// resized to |max_image_size| and is the only result. A |max_image_size| of
    /// 0 means unlimited. If |bypass_cache| is true (1) then |image_url| is
    /// requested from the server even if it is present in the browser cache.
    ///
    public delegate* unmanaged<cef_browser_host_t*, cef_string_t*, int, uint, int, cef_download_image_callback_t*, void> download_image;

    ///
    /// Print the current browser contents.
    ///
    public delegate* unmanaged<cef_browser_host_t*, void> print;

    ///
    /// Print the current browser contents to the PDF file specified by |path| and
    /// execute |callback| on completion. The caller is responsible for deleting
    /// |path| when done. For PDF printing to work on Linux you must implement the
    /// cef_print_handler_t::GetPdfPaperSize function.
    ///
    public delegate* unmanaged<cef_browser_host_t*, cef_string_t*, cef_pdf_print_settings_t*, cef_pdf_print_callback_t*, void> print_to_pdf;

    ///
    /// Search for |searchText|. |forward| indicates whether to search forward or
    /// backward within the page. |matchCase| indicates whether the search should
    /// be case-sensitive. |findNext| indicates whether this is the first request
    /// or a follow-up. The search will be restarted if |searchText| or
    /// |matchCase| change. The search will be stopped if |searchText| is NULL.
    /// The cef_find_handler_t instance, if any, returned via
    /// cef_client_t::GetFindHandler will be called to report find results.
    ///
    public delegate* unmanaged<cef_browser_host_t*, cef_string_t*, int, int, int, void> find;

    ///
    /// Cancel all searches that are currently going on.
    ///
    public delegate* unmanaged<cef_browser_host_t*, int, void> stop_finding;

    ///
    /// Open developer tools (DevTools) in its own browser. The DevTools browser
    /// will remain associated with this browser. If the DevTools browser is
    /// already open then it will be focused, in which case the |windowInfo|,
    /// |client| and |settings| parameters will be ignored. If
    /// |inspect_element_at| is non-NULL then the element at the specified (x,y)
    /// location will be inspected. The |windowInfo| parameter will be ignored if
    /// this browser is wrapped in a cef_browser_view_t.
    ///
    public delegate* unmanaged<cef_browser_host_t*, cef_window_info_t*, cef_client_t*, cef_browser_settings_t*, cef_point_t*, void> show_dev_tools;

    ///
    /// Explicitly close the associated DevTools browser, if any.
    ///
    public delegate* unmanaged<cef_browser_host_t*, void> close_dev_tools;

    ///
    /// Returns true (1) if this browser currently has an associated DevTools
    /// browser. Must be called on the browser process UI thread.
    ///
    public delegate* unmanaged<cef_browser_host_t*, int> has_dev_tools;

    ///
    /// Send a function call message over the DevTools protocol. |message| must be
    /// a UTF8-encoded JSON dictionary that contains "id" (int), "function"
    /// (string) and "params" (dictionary, optional) values. See the DevTools
    /// protocol documentation at https://chromedevtools.github.io/devtools-
    /// protocol/ for details of supported functions and the expected "params"
    /// dictionary contents. |message| will be copied if necessary. This function
    /// will return true (1) if called on the UI thread and the message was
    /// successfully submitted for validation, otherwise false (0). Validation
    /// will be applied asynchronously and any messages that fail due to
    /// formatting errors or missing parameters may be discarded without
    /// notification. Prefer ExecuteDevToolsMethod if a more structured approach
    /// to message formatting is desired.
    ///
    /// Every valid function call will result in an asynchronous function result
    /// or error message that references the sent message "id". Event messages are
    /// received while notifications are enabled (for example, between function
    /// calls for "Page.enable" and "Page.disable"). All received messages will be
    /// delivered to the observer(s) registered with AddDevToolsMessageObserver.
    /// See cef_dev_tools_message_observer_t::OnDevToolsMessage documentation for
    /// details of received message contents.
    ///
    /// Usage of the SendDevToolsMessage, ExecuteDevToolsMethod and
    /// AddDevToolsMessageObserver functions does not require an active DevTools
    /// front-end or remote-debugging session. Other active DevTools sessions will
    /// continue to function independently. However, any modification of global
    /// browser state by one session may not be reflected in the UI of other
    /// sessions.
    ///
    /// Communication with the DevTools front-end (when displayed) can be logged
    /// for development purposes by passing the `--devtools-protocol-log-
    /// file=<path>` command-line flag.
    ///
    public delegate* unmanaged<cef_browser_host_t*, void*, nuint, int> send_dev_tools_message;

    ///
    /// Execute a function call over the DevTools protocol. This is a more
    /// structured version of SendDevToolsMessage. |message_id| is an incremental
    /// number that uniquely identifies the message (pass 0 to have the next
    /// number assigned automatically based on previous values). |function| is the
    /// function name. |params| are the function parameters, which may be NULL.
    /// See the DevTools protocol documentation (linked above) for details of
    /// supported functions and the expected |params| dictionary contents. This
    /// function will return the assigned message ID if called on the UI thread
    /// and the message was successfully submitted for validation, otherwise 0.
    /// See the SendDevToolsMessage documentation for additional usage
    /// information.
    ///
    public delegate* unmanaged<cef_browser_host_t*, int, cef_string_t*, cef_dictionary_value_t*, int> execute_dev_tools_method;

    ///
    /// Add an observer for DevTools protocol messages (function results and
    /// events). The observer will remain registered until the returned
    /// Registration object is destroyed. See the SendDevToolsMessage
    /// documentation for additional usage information.
    ///
    public delegate* unmanaged<cef_browser_host_t*, cef_dev_tools_message_observer_t*, cef_registration_t*> add_dev_tools_message_observer;

    ///
    /// Retrieve a snapshot of current navigation entries as values sent to the
    /// specified visitor. If |current_only| is true (1) only the current
    /// navigation entry will be sent, otherwise all navigation entries will be
    /// sent.
    ///
    public delegate* unmanaged<cef_browser_host_t*, cef_navigation_entry_visitor_t*, int, void> get_navigation_entries;

    ///
    /// If a misspelled word is currently selected in an editable node calling
    /// this function will replace it with the specified |word|.
    ///
    public delegate* unmanaged<cef_browser_host_t*, cef_string_t*, void> replace_misspelling;

    ///
    /// Add the specified |word| to the spelling dictionary.
    ///
    public delegate* unmanaged<cef_browser_host_t*, cef_string_t*, void> add_word_to_dictionary;

    ///
    /// Returns true (1) if window rendering is disabled.
    ///
    public delegate* unmanaged<cef_browser_host_t*, int> is_window_rendering_disabled;

    ///
    /// Notify the browser that the widget has been resized. The browser will
    /// first call cef_render_handler_t::GetViewRect to get the new size and then
    /// call cef_render_handler_t::OnPaint asynchronously with the updated
    /// regions. This function is only used when window rendering is disabled.
    ///
    public delegate* unmanaged<cef_browser_host_t*, void> was_resized;

    ///
    /// Notify the browser that it has been hidden or shown. Layouting and
    /// cef_render_handler_t::OnPaint notification will stop when the browser is
    /// hidden. This function is only used when window rendering is disabled.
    ///
    public delegate* unmanaged<cef_browser_host_t*, int, void> was_hidden;

    ///
    /// Send a notification to the browser that the screen info has changed. The
    /// browser will then call cef_render_handler_t::GetScreenInfo to update the
    /// screen information with the new values. This simulates moving the webview
    /// window from one display to another, or changing the properties of the
    /// current display. This function is only used when window rendering is
    /// disabled.
    ///
    public delegate* unmanaged<cef_browser_host_t*, void> notify_screen_info_changed;

    ///
    /// Invalidate the view. The browser will call cef_render_handler_t::OnPaint
    /// asynchronously. This function is only used when window rendering is
    /// disabled.
    ///
    public delegate* unmanaged<cef_browser_host_t*, cef_paint_element_type_t, void> invalidate;

    ///
    /// Issue a BeginFrame request to Chromium.  Only valid when
    /// cef_window_tInfo::external_begin_frame_enabled is set to true (1).
    ///
    public delegate* unmanaged<cef_browser_host_t*, void> send_external_begin_frame;

    ///
    /// Send a key event to the browser.
    ///
    public delegate* unmanaged<cef_browser_host_t*, cef_key_event_t*, void> send_key_event;

    ///
    /// Send a mouse click event to the browser. The |x| and |y| coordinates are
    /// relative to the upper-left corner of the view.
    ///
    public delegate* unmanaged<cef_browser_host_t*, cef_mouse_event_t*, cef_mouse_button_type_t, int, int, void> send_mouse_click_event;

    ///
    /// Send a mouse move event to the browser. The |x| and |y| coordinates are
    /// relative to the upper-left corner of the view.
    ///
    public delegate* unmanaged<cef_browser_host_t*, cef_mouse_event_t*, int, void> send_mouse_move_event;

    ///
    /// Send a mouse wheel event to the browser. The |x| and |y| coordinates are
    /// relative to the upper-left corner of the view. The |deltaX| and |deltaY|
    /// values represent the movement delta in the X and Y directions
    /// respectively. In order to scroll inside select popups with window
    /// rendering disabled cef_render_handler_t::GetScreenPoint should be
    /// implemented properly.
    ///
    public delegate* unmanaged<cef_browser_host_t*, cef_mouse_event_t*, int, int, void> send_mouse_wheel_event;

    ///
    /// Send a touch event to the browser for a windowless browser.
    ///
    public delegate* unmanaged<cef_browser_host_t*, cef_touch_event_t*, void> send_touch_event;

    ///
    /// Send a capture lost event to the browser.
    ///
    public delegate* unmanaged<cef_browser_host_t*, void> send_capture_lost_event;

    ///
    /// Notify the browser that the window hosting it is about to be moved or
    /// resized. This function is only used on Windows and Linux.
    ///
    public delegate* unmanaged<cef_browser_host_t*, void> notify_move_or_resize_started;

    ///
    /// Returns the maximum rate in frames per second (fps) that
    /// cef_render_handler_t::OnPaint will be called for a windowless browser. The
    /// actual fps may be lower if the browser cannot generate frames at the
    /// requested rate. The minimum value is 1 and the maximum value is 60
    /// (default 30). This function can only be called on the UI thread.
    ///
    public delegate* unmanaged<cef_browser_host_t*, int> get_windowless_frame_rate;

    ///
    /// Set the maximum rate in frames per second (fps) that
    /// cef_render_handler_t:: OnPaint will be called for a windowless browser.
    /// The actual fps may be lower if the browser cannot generate frames at the
    /// requested rate. The minimum value is 1 and the maximum value is 60
    /// (default 30). Can also be set at browser creation via
    /// cef_browser_tSettings.windowless_frame_rate.
    ///
    public delegate* unmanaged<cef_browser_host_t*, int, void> set_windowless_frame_rate;

    ///
    /// Begins a new composition or updates the existing composition. Blink has a
    /// special node (a composition node) that allows the input function to change
    /// text without affecting other DOM nodes. |text| is the optional text that
    /// will be inserted into the composition node. |underlines| is an optional
    /// set of ranges that will be underlined in the resulting text.
    /// |replacement_range| is an optional range of the existing text that will be
    /// replaced. |selection_range| is an optional range of the resulting text
    /// that will be selected after insertion or replacement. The
    /// |replacement_range| value is only used on OS X.
    ///
    /// This function may be called multiple times as the composition changes.
    /// When the client is done making changes the composition should either be
    /// canceled or completed. To cancel the composition call
    /// ImeCancelComposition. To complete the composition call either
    /// ImeCommitText or ImeFinishComposingText. Completion is usually signaled
    /// when:
    ///
    /// 1. The client receives a WM_IME_COMPOSITION message with a GCS_RESULTSTR
    ///    flag (on Windows), or;
    /// 2. The client receives a "commit" signal of GtkIMContext (on Linux), or;
    /// 3. insertText of NSTextInput is called (on Mac).
    ///
    /// This function is only used when window rendering is disabled.
    ///
    public delegate* unmanaged<cef_browser_host_t*, cef_string_t*, nuint, cef_composition_underline_t*, cef_range_t*, cef_range_t*, void> ime_set_composition;

    ///
    /// Completes the existing composition by optionally inserting the specified
    /// |text| into the composition node. |replacement_range| is an optional range
    /// of the existing text that will be replaced. |relative_cursor_pos| is where
    /// the cursor will be positioned relative to the current cursor position. See
    /// comments on ImeSetComposition for usage. The |replacement_range| and
    /// |relative_cursor_pos| values are only used on OS X. This function is only
    /// used when window rendering is disabled.
    ///
    public delegate* unmanaged<cef_browser_host_t*, cef_string_t*, cef_range_t*, int, void> ime_commit_text;

    ///
    /// Completes the existing composition by applying the current composition
    /// node contents. If |keep_selection| is false (0) the current selection, if
    /// any, will be discarded. See comments on ImeSetComposition for usage. This
    /// function is only used when window rendering is disabled.
    ///
    public delegate* unmanaged<cef_browser_host_t*, int, void> ime_finish_composing_text;

    ///
    /// Cancels the existing composition and discards the composition node
    /// contents without applying them. See comments on ImeSetComposition for
    /// usage. This function is only used when window rendering is disabled.
    ///
    public delegate* unmanaged<cef_browser_host_t*, void> ime_cancel_composition;

    ///
    /// Call this function when the user drags the mouse into the web view (before
    /// calling DragTargetDragOver/DragTargetLeave/DragTargetDrop). |drag_data|
    /// should not contain file contents as this type of data is not allowed to be
    /// dragged into the web view. File contents can be removed using
    /// cef_drag_data_t::ResetFileContents (for example, if |drag_data| comes from
    /// cef_render_handler_t::StartDragging). This function is only used when
    /// window rendering is disabled.
    ///
    public delegate* unmanaged<cef_browser_host_t*, cef_drag_data_t*, cef_mouse_event_t*, cef_drag_operations_mask_t, void> drag_target_drag_enter;

    ///
    /// Call this function each time the mouse is moved across the web view during
    /// a drag operation (after calling DragTargetDragEnter and before calling
    /// DragTargetDragLeave/DragTargetDrop). This function is only used when
    /// window rendering is disabled.
    ///
    public delegate* unmanaged<cef_browser_host_t*, cef_mouse_event_t*, cef_drag_operations_mask_t, void> drag_target_drag_over;

    ///
    /// Call this function when the user drags the mouse out of the web view
    /// (after calling DragTargetDragEnter). This function is only used when
    /// window rendering is disabled.
    ///
    public delegate* unmanaged<cef_browser_host_t*, void> drag_target_drag_leave;

    ///
    /// Call this function when the user completes the drag operation by dropping
    /// the object onto the web view (after calling DragTargetDragEnter). The
    /// object being dropped is |drag_data|, given as an argument to the previous
    /// DragTargetDragEnter call. This function is only used when window rendering
    /// is disabled.
    ///
    public delegate* unmanaged<cef_browser_host_t*, cef_mouse_event_t*, void> drag_target_drop;

    ///
    /// Call this function when the drag operation started by a
    /// cef_render_handler_t::StartDragging call has ended either in a drop or by
    /// being cancelled. |x| and |y| are mouse coordinates relative to the upper-
    /// left corner of the view. If the web view is both the drag source and the
    /// drag target then all DragTarget* functions should be called before
    /// DragSource* mthods. This function is only used when window rendering is
    /// disabled.
    ///
    public delegate* unmanaged<cef_browser_host_t*, int, int, cef_drag_operations_mask_t, void> drag_source_ended_at;

    ///
    /// Call this function when the drag operation started by a
    /// cef_render_handler_t::StartDragging call has completed. This function may
    /// be called immediately without first calling DragSourceEndedAt to cancel a
    /// drag operation. If the web view is both the drag source and the drag
    /// target then all DragTarget* functions should be called before DragSource*
    /// mthods. This function is only used when window rendering is disabled.
    ///
    public delegate* unmanaged<cef_browser_host_t*, void> drag_source_system_drag_ended;

    ///
    /// Returns the current visible navigation entry for this browser. This
    /// function can only be called on the UI thread.
    ///
    public delegate* unmanaged<cef_browser_host_t*, cef_navigation_entry_t*> get_visible_navigation_entry;

    ///
    /// Set accessibility state for all frames. |accessibility_state| may be
    /// default, enabled or disabled. If |accessibility_state| is STATE_DEFAULT
    /// then accessibility will be disabled by default and the state may be
    /// further controlled with the "force-renderer-accessibility" and "disable-
    /// renderer-accessibility" command-line switches. If |accessibility_state| is
    /// STATE_ENABLED then accessibility will be enabled. If |accessibility_state|
    /// is STATE_DISABLED then accessibility will be completely disabled.
    ///
    /// For windowed browsers accessibility will be enabled in Complete mode
    /// (which corresponds to kAccessibilityModeComplete in Chromium). In this
    /// mode all platform accessibility objects will be created and managed by
    /// Chromium's internal implementation. The client needs only to detect the
    /// screen reader and call this function appropriately. For example, on macOS
    /// the client can handle the @"AXEnhancedUserStructure" accessibility
    /// attribute to detect VoiceOver state changes and on Windows the client can
    /// handle WM_GETOBJECT with OBJID_CLIENT to detect accessibility readers.
    ///
    /// For windowless browsers accessibility will be enabled in TreeOnly mode
    /// (which corresponds to kAccessibilityModeWebContentsOnly in Chromium). In
    /// this mode renderer accessibility is enabled, the full tree is computed,
    /// and events are passed to CefAccessibiltyHandler, but platform
    /// accessibility objects are not created. The client may implement platform
    /// accessibility objects using CefAccessibiltyHandler callbacks if desired.
    ///
    public delegate* unmanaged<cef_browser_host_t*, cef_state_t, void> set_accessibility_state;

    ///
    /// Enable notifications of auto resize via
    /// cef_display_handler_t::OnAutoResize. Notifications are disabled by
    /// default. |min_size| and |max_size| define the range of allowed sizes.
    ///
    public delegate* unmanaged<cef_browser_host_t*, int, cef_size_t*, cef_size_t*, void> set_auto_resize_enabled;

    ///
    /// Set whether the browser's audio is muted.
    ///
    public delegate* unmanaged<cef_browser_host_t*, int, void> set_audio_muted;

    ///
    /// Returns true (1) if the browser's audio is muted.  This function can only
    /// be called on the UI thread.
    ///
    public delegate* unmanaged<cef_browser_host_t*, int> is_audio_muted;

    ///
    /// Returns true (1) if the renderer is currently in browser fullscreen. This
    /// differs from window fullscreen in that browser fullscreen is entered using
    /// the JavaScript Fullscreen API and modifies CSS attributes such as the
    /// ::backdrop pseudo-element and :fullscreen pseudo-structure. This function
    /// can only be called on the UI thread.
    ///
    public delegate* unmanaged<cef_browser_host_t*, int> is_fullscreen;

    ///
    /// Requests the renderer to exit browser fullscreen. In most cases exiting
    /// window fullscreen should also exit browser fullscreen. With Alloy style
    /// this function should be called in response to a user action such as
    /// clicking the green traffic light button on MacOS
    /// (cef_window_delegate_t::OnWindowFullscreenTransition callback) or pressing
    /// the "ESC" key (cef_keyboard_handler_t::OnPreKeyEvent callback). With
    /// Chrome style these standard exit actions are handled internally but
    /// new/additional user actions can use this function. Set |will_cause_resize|
    /// to true (1) if exiting browser fullscreen will cause a view resize.
    ///
    public delegate* unmanaged<cef_browser_host_t*, int, void> exit_fullscreen;

    ///
    /// Returns true (1) if a Chrome command is supported and enabled. Use the
    /// cef_id_for_command_id_name() function for version-safe mapping of command
    /// IDC names from cef_command_ids.h to version-specific numerical
    /// |command_id| values. This function can only be called on the UI thread.
    /// Only used with Chrome style.
    ///
    public delegate* unmanaged<cef_browser_host_t*, int, int> can_execute_chrome_command;

    ///
    /// Execute a Chrome command. Use the cef_id_for_command_id_name() function
    /// for version-safe mapping of command IDC names from cef_command_ids.h to
    /// version-specific numerical |command_id| values. |disposition| provides
    /// information about the intended command target. Only used with Chrome
    /// style.
    ///
    public delegate* unmanaged<cef_browser_host_t*, int, cef_window_open_disposition_t, void> execute_chrome_command;

    ///
    /// Returns true (1) if the render process associated with this browser is
    /// currently unresponsive as indicated by a lack of input event processing
    /// for at least 15 seconds. To receive associated state change notifications
    /// and optionally handle an unresponsive render process implement
    /// cef_request_handler_t::OnRenderProcessUnresponsive. This function can only
    /// be called on the UI thread.
    ///
    public delegate* unmanaged<cef_browser_host_t*, int> is_render_process_unresponsive;

    ///
    /// Returns the runtime style for this browser (ALLOY or CHROME). See
    /// cef_runtime_style_t documentation for details.
    ///
    public delegate* unmanaged<cef_browser_host_t*, cef_runtime_style_t> get_runtime_style;
}