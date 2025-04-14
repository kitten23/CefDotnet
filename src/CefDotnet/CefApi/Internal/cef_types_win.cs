using CefDotnet.CefApi.Types;
using CefDotnet.Win;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi.Internal;

///
/// Structure representing CefExecuteProcess arguments.
///
[StructLayout(LayoutKind.Sequential)]
public ref struct cef_main_args_t
{
    public nint instance;
}

///
/// Structure representing window information.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe ref struct cef_window_info_t
{
    public cef_window_info_t()
    {
        size = (nuint)sizeof(cef_window_info_t);
    }

    ///
    /// Size of this structure.
    ///
    nuint size;

    // Standard parameters required by CreateWindowEx()
    public ExtendedWindowStyles ex_style;
    public cef_string_t window_name;
    public WindowStyles style;
    public cef_rect_t bounds;
    public nint parent_window;
    public nint menu;

    ///
    /// Set to true (1) to create the browser using windowless (off-screen)
    /// rendering. No window will be created for the browser and all rendering
    /// will occur via the CefRenderHandler interface. The |parent_window| value
    /// will be used to identify monitor info and to act as the parent window for
    /// dialogs, context menus, etc. If |parent_window| is not provided then the
    /// main screen monitor will be used and some functionality that requires a
    /// parent window may not function correctly. In order to create windowless
    /// browsers the CefSettings.windowless_rendering_enabled value must be set to
    /// true. Transparent painting is enabled by default but can be disabled by
    /// setting CefBrowserSettings.background_color to an opaque value.
    ///
    public int windowless_rendering_enabled;

    ///
    /// Set to true (1) to enable shared textures for windowless rendering. Only
    /// valid if windowless_rendering_enabled above is also set to true. Currently
    /// only supported on Windows (D3D11).
    ///
    public int shared_texture_enabled;

    ///
    /// Set to true (1) to enable the ability to issue BeginFrame requests from
    /// the client application by calling CefBrowserHost::SendExternalBeginFrame.
    ///
    public int external_begin_frame_enabled;

    ///
    /// Handle for the new browser window. Only used with windowed rendering.
    ///
    public nint window;

    ///
    /// Optionally change the runtime style. Alloy style will always be used if
    /// |windowless_rendering_enabled| is true. See cef_runtime_style_t
    /// documentation for details.
    ///
    public cef_runtime_style_t runtime_style;

    public void SetAsChild(nint parent, cef_rect_t* windowBounds)
    {
        style = WindowStyles.WS_CHILD | WindowStyles.WS_CLIPCHILDREN | WindowStyles.WS_CLIPSIBLINGS | WindowStyles.WS_TABSTOP | WindowStyles.WS_VISIBLE;
        parent_window = parent;
        bounds = *windowBounds;
    }
}

///
/// Structure containing shared texture information for the OnAcceleratedPaint
/// callback. Resources will be released to the underlying pool for reuse when
/// the callback returns from client code.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_accelerated_paint_info_t
{
    public cef_accelerated_paint_info_t()
    {
        size = (nuint)sizeof(cef_accelerated_paint_info_t);
    }

    ///
    /// Size of this structure.
    ///
    nuint size;

    ///
    /// Handle for the shared texture. The shared texture is instantiated
    /// without a keyed mutex.
    ///
    nint shared_texture_handle;

    ///
    /// The pixel format of the texture.
    ///
    cef_color_type_t format;

    ///
    /// The extra common info.
    ///
    cef_accelerated_paint_info_common_t extra;
}