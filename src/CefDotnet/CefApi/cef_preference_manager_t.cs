using CefDotnet.CefApi.Types;
using CefDotnet.CefApi.Values;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_preference_manager_t
///
/// Manage access to preferences. Many built-in preferences are registered by
/// Chromium. Custom preferences can be registered in
/// cef_browser_process_handler_t::OnRegisterCustomPreferences.
///
/// NOTE: This struct is allocated DLL-side.
///
{
    ///
    /// Base structure.
    ///
    public cef_base_ref_counted_t @base;

    ///
    /// Returns true (1) if a preference with the specified |name| exists. This
    /// function must be called on the browser process UI thread.
    ///
    public delegate* unmanaged<cef_preference_manager_t*, cef_string_t*, int> has_preference;

    ///
    /// Returns the value for the preference with the specified |name|. Returns
    /// NULL if the preference does not exist. The returned object contains a copy
    /// of the underlying preference value and modifications to the returned
    /// object will not modify the underlying preference value. This function must
    /// be called on the browser process UI thread.
    ///
    public delegate* unmanaged<cef_preference_manager_t*, cef_string_t*, cef_value_t*> get_preference;

    ///
    /// Returns all preferences as a dictionary. If |include_defaults| is true (1)
    /// then preferences currently at their default value will be included. The
    /// returned object contains a copy of the underlying preference values and
    /// modifications to the returned object will not modify the underlying
    /// preference values. This function must be called on the browser process UI
    /// thread.
    ///
    public delegate* unmanaged<cef_preference_manager_t*, int, cef_dictionary_value_t*> get_all_preferences;

    ///
    /// Returns true (1) if the preference with the specified |name| can be
    /// modified using SetPreference. As one example preferences set via the
    /// command-line usually cannot be modified. This function must be called on
    /// the browser process UI thread.
    ///
    public delegate* unmanaged<cef_preference_manager_t*, cef_string_t*, int> can_set_preference;

    ///
    /// Set the |value| associated with preference |name|. Returns true (1) if the
    /// value is set successfully and false (0) otherwise. If |value| is NULL the
    /// preference will be restored to its default value. If setting the
    /// preference fails then |error| will be populated with a detailed
    /// description of the problem. This function must be called on the browser
    /// process UI thread.
    ///
    public delegate* unmanaged<cef_preference_manager_t*, cef_string_t*, cef_value_t*, cef_string_t*, int> set_preference;
}
