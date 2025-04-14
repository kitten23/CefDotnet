using CefDotnet.CefApi.Types;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;
///
/// Structure that manages custom preference registrations.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_preference_registrar_t

{
    ///
    /// Base structure.
    ///
    cef_base_scoped_t @base;

    ///
    /// Register a preference with the specified |name| and |default_value|. To
    /// avoid conflicts with built-in preferences the |name| value should contain
    /// an application-specific prefix followed by a period (e.g. "myapp.value").
    /// The contents of |default_value| will be copied. The data type for the
    /// preference will be inferred from |default_value|'s type and cannot be
    /// changed after registration. Returns true (1) on success. Returns false (0)
    /// if |name| is already registered or if |default_value| has an invalid type.
    /// This function must be called from within the scope of the
    /// cef_browser_process_handler_t::OnRegisterCustomPreferences callback.
    ///
    public delegate* unmanaged<cef_preference_registrar_t*, cef_string_t*, cef_value_t*, int> add_preference;
}