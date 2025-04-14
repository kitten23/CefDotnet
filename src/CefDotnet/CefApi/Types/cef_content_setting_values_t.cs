namespace CefDotnet.CefApi.Types;
///
/// Supported content setting values. Should be kept in sync with Chromium's
/// ContentSetting type.
///
public enum cef_content_setting_values_t : uint
{
    CEF_CONTENT_SETTING_VALUE_DEFAULT,
    CEF_CONTENT_SETTING_VALUE_ALLOW,
    CEF_CONTENT_SETTING_VALUE_BLOCK,
    CEF_CONTENT_SETTING_VALUE_ASK,
    CEF_CONTENT_SETTING_VALUE_SESSION_ONLY,
    CEF_CONTENT_SETTING_VALUE_DETECT_IMPORTANT_CONTENT,

    CEF_CONTENT_SETTING_VALUE_NUM_VALUES,
}