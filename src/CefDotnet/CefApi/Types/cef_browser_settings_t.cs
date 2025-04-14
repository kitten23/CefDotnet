using System.Runtime.InteropServices;

namespace CefDotnet.CefApi.Types;

///
/// Browser initialization settings. Specify NULL or 0 to get the recommended
/// default values. The consequences of using custom values may not be well
/// tested. Many of these and other settings can also configured using command-
/// line switches.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe ref struct cef_browser_settings_t
{
    public cef_browser_settings_t()
    {
        size = (nuint)sizeof(cef_browser_settings_t);
    }

    ///
    /// Size of this structure.
    ///
    nuint size;

    ///
    /// The maximum rate in frames per second (fps) that CefRenderHandler::OnPaint
    /// will be called for a windowless browser. The actual fps may be lower if
    /// the browser cannot generate frames at the requested rate. The minimum
    /// value is 1 and the maximum value is 60 (default 30). This value can also
    /// be changed dynamically via CefBrowserHost::SetWindowlessFrameRate.
    ///
    public int windowless_frame_rate;

    /// BEGIN values that map to WebPreferences settings.

    ///
    /// Font settings.
    ///
    public cef_string_t standard_font_family;
    public cef_string_t fixed_font_family;
    public cef_string_t serif_font_family;
    public cef_string_t sans_serif_font_family;
    public cef_string_t cursive_font_family;
    public cef_string_t fantasy_font_family;
    public int default_font_size;
    public int default_fixed_font_size;
    public int minimum_font_size;
    public int minimum_logical_font_size;

    ///
    /// Default encoding for Web content. If empty "ISO-8859-1" will be used. Also
    /// configurable using the "default-encoding" command-line switch.
    ///
    public cef_string_t default_encoding;

    ///
    /// Controls the loading of fonts from remote sources. Also configurable using
    /// the "disable-remote-fonts" command-line switch.
    ///
    public cef_state_t remote_fonts;

    ///
    /// Controls whether JavaScript can be executed. Also configurable using the
    /// "disable-javascript" command-line switch.
    ///
    public cef_state_t javascript;

    ///
    /// Controls whether JavaScript can be used to close windows that were not
    /// opened via JavaScript. JavaScript can still be used to close windows that
    /// were opened via JavaScript or that have no back/forward history. Also
    /// configurable using the "disable-javascript-close-windows" command-line
    /// switch.
    ///
    public cef_state_t javascript_close_windows;

    ///
    /// Controls whether JavaScript can access the clipboard. Also configurable
    /// using the "disable-javascript-access-clipboard" command-line switch.
    ///
    public cef_state_t javascript_access_clipboard;

    ///
    /// Controls whether DOM pasting is supported in the editor via
    /// execCommand("paste"). The |javascript_access_clipboard| setting must also
    /// be enabled. Also configurable using the "disable-javascript-dom-paste"
    /// command-line switch.
    ///
    public cef_state_t javascript_dom_paste;

    ///
    /// Controls whether image URLs will be loaded from the network. A cached
    /// image will still be rendered if requested. Also configurable using the
    /// "disable-image-loading" command-line switch.
    ///
    public cef_state_t image_loading;

    ///
    /// Controls whether standalone images will be shrunk to fit the page. Also
    /// configurable using the "image-shrink-standalone-to-fit" command-line
    /// switch.
    ///
    public cef_state_t image_shrink_standalone_to_fit;

    ///
    /// Controls whether text areas can be resized. Also configurable using the
    /// "disable-text-area-resize" command-line switch.
    ///
    public cef_state_t text_area_resize;

    ///
    /// Controls whether the tab key can advance focus to links. Also configurable
    /// using the "disable-tab-to-links" command-line switch.
    ///
    public cef_state_t tab_to_links;

    ///
    /// Controls whether local storage can be used. Also configurable using the
    /// "disable-local-storage" command-line switch.
    ///
    public cef_state_t local_storage;

    ///
    /// Controls whether databases can be used. Also configurable using the
    /// "disable-databases" command-line switch.
    ///
    public cef_state_t databases;

    ///
    /// Controls whether WebGL can be used. Note that WebGL requires hardware
    /// support and may not work on all systems even when enabled. Also
    /// configurable using the "disable-webgl" command-line switch.
    ///
    public cef_state_t webgl;

    /// END values that map to WebPreferences settings.

    ///
    /// Background color used for the browser before a document is loaded and when
    /// no document color is specified. The alpha component must be either fully
    /// opaque (0xFF) or fully transparent (0x00). If the alpha component is fully
    /// opaque then the RGB components will be used as the background color. If
    /// the alpha component is fully transparent for a windowed browser then the
    /// CefSettings.background_color value will be used. If the alpha component is
    /// fully transparent for a windowless (off-screen) browser then transparent
    /// painting will be enabled.
    ///
    public uint background_color;

    ///
    /// Controls whether the Chrome status bubble will be used. Only supported
    /// with Chrome style. For details about the status bubble see
    /// https://www.chromium.org/user-experience/status-bubble/
    ///
    public cef_state_t chrome_status_bubble;

    ///
    /// Controls whether the Chrome zoom bubble will be shown when zooming. Only
    /// supported with Chrome style.
    ///
    public cef_state_t chrome_zoom_bubble;
}
