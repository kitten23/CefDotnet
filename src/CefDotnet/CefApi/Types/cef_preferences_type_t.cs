namespace CefDotnet.CefApi.Types;

///
/// Preferences type passed to
/// CefBrowserProcessHandler::OnRegisterCustomPreferences.
///
public enum cef_preferences_type_t : uint
{
    /// Global preferences registered a single time at application startup.
    CEF_PREFERENCES_TYPE_GLOBAL,

    /// Request context preferences registered each time a new CefRequestContext
    /// is created.
    CEF_PREFERENCES_TYPE_REQUEST_CONTEXT,

    CEF_PREFERENCES_TYPE_NUM_VALUES,
}