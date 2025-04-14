using CefDotnet.CefApi.Types;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// Structure used to implement a custom resource bundle structure. See
/// CefSettings for additional options related to resource bundle loading. The
/// functions of this structure may be called on multiple threads.
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_resource_bundle_handler_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Called to retrieve a localized translation for the specified |string_id|.
    /// To provide the translation set |string| to the translation string and
    /// return true (1). To use the default translation return false (0). Use the
    /// cef_id_for_pack_string_name() function for version-safe mapping of string
    /// IDS names from cef_pack_strings.h to version-specific numerical
    /// |string_id| values.
    ///
    public delegate* unmanaged<cef_resource_bundle_handler_t*, int, cef_string_t*, int> get_localized_string;

    ///
    /// Called to retrieve data for the specified scale independent |resource_id|.
    /// To provide the resource data set |data| and |data_size| to the data
    /// pointer and size respectively and return true (1). To use the default
    /// resource data return false (0). The resource data will not be copied and
    /// must remain resident in memory. Use the cef_id_for_pack_resource_name()
    /// function for version-safe mapping of resource IDR names from
    /// cef_pack_resources.h to version-specific numerical |resource_id| values.
    ///
    public delegate* unmanaged<cef_resource_bundle_handler_t*, int, void**, nuint*, int> get_data_resource;

    ///
    /// Called to retrieve data for the specified |resource_id| nearest the scale
    /// factor |scale_factor|. To provide the resource data set |data| and
    /// |data_size| to the data pointer and size respectively and return true (1).
    /// To use the default resource data return false (0). The resource data will
    /// not be copied and must remain resident in memory. Use the
    /// cef_id_for_pack_resource_name() function for version-safe mapping of
    /// resource IDR names from cef_pack_resources.h to version-specific numerical
    /// |resource_id| values.
    ///
    public delegate* unmanaged<cef_resource_bundle_handler_t*, int, cef_scale_factor_t, void**, nuint*, int> get_data_resource_for_scale;
}
