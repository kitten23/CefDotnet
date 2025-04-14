namespace CefDotnet.CefApi.Types;

///
/// Represents the state of a setting.
///
public enum cef_state_t
{
    ///
    /// Use the default state for the setting.
    ///
    STATE_DEFAULT = 0,

    ///
    /// Enable or allow the setting.
    ///
    STATE_ENABLED,

    ///
    /// Disable or disallow the setting.
    ///
    STATE_DISABLED,
}
