namespace CefDotnet.CefApi.Types;

///
/// The manner in which a link click should be opened. These constants match
/// their equivalents in Chromium's window_open_disposition.h and should not be
/// renumbered.
///
public enum cef_window_open_disposition_t
{
    CEF_WOD_UNKNOWN,

    ///
    /// Current tab. This is the default in most cases.
    ///
    CEF_WOD_CURRENT_TAB,

    ///
    /// Indicates that only one tab with the url should exist in the same window.
    ///
    CEF_WOD_SINGLETON_TAB,

    ///
    /// Shift key + Middle mouse button or meta/ctrl key while clicking.
    ///
    CEF_WOD_NEW_FOREGROUND_TAB,

    ///
    /// Middle mouse button or meta/ctrl key while clicking.
    ///
    CEF_WOD_NEW_BACKGROUND_TAB,

    ///
    /// New popup window.
    ///
    CEF_WOD_NEW_POPUP,

    ///
    /// Shift key while clicking.
    ///
    CEF_WOD_NEW_WINDOW,

    ///
    /// Alt key while clicking.
    ///
    CEF_WOD_SAVE_TO_DISK,

    ///
    /// New off-the-record (incognito) window.
    ///
    CEF_WOD_OFF_THE_RECORD,

    ///
    /// Special case error condition from the renderer.
    ///
    CEF_WOD_IGNORE_ACTION,

    ///
    /// Activates an existing tab containing the url, rather than navigating.
    /// This is similar to SINGLETON_TAB, but searches across all windows from
    /// the current profile and anonymity (instead of just the current one);
    /// closes the current tab on switching if the current tab was the NTP with
    /// no session history; and behaves like CURRENT_TAB instead of
    /// NEW_FOREGROUND_TAB when no existing tab is found.
    ///
    CEF_WOD_SWITCH_TO_TAB,

    ///
    /// Creates a new document picture-in-picture window showing a child WebView.
    ///
    CEF_WOD_NEW_PICTURE_IN_PICTURE,

    CEF_WOD_NUM_VALUES,
}
