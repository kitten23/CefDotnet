using CefDotnet.CefApi.Internal;
using CefDotnet.CefApi.Types;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// Structure representing the issuer or subject field of an X.509 certificate.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_x509_cert_principal_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Returns a name that can be used to represent the issuer. It tries in this
    /// order: Common Name (CN), Organization Name (O) and Organizational Unit
    /// Name (OU) and returns the first non-NULL one found.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_x509_cert_principal_t*, cef_string_t*> get_display_name;

    ///
    /// Returns the common name.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_x509_cert_principal_t*, cef_string_t*> get_common_name;

    ///
    /// Returns the locality name.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_x509_cert_principal_t*, cef_string_t*> get_locality_name;

    ///
    /// Returns the state or province name.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_x509_cert_principal_t*, cef_string_t*> get_state_or_province_name;

    ///
    /// Returns the country name.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_x509_cert_principal_t*, cef_string_t*> get_country_name;

    ///
    /// Retrieve the list of organization names.
    ///
    public delegate* unmanaged<cef_x509_cert_principal_t*, nint, void> get_organization_names;

    ///
    /// Retrieve the list of organization unit names.
    ///
    public delegate* unmanaged<cef_x509_cert_principal_t*, nint, void> get_organization_unit_names;
}

///
/// Structure representing a X.509 certificate.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_x509_certificate_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Returns the subject of the X.509 certificate. For HTTPS server
    /// certificates this represents the web server.  The common name of the
    /// subject should match the host name of the web server.
    ///
    public delegate* unmanaged<cef_x509_certificate_t*, cef_x509_cert_principal_t*> get_subject;

    ///
    /// Returns the issuer of the X.509 certificate.
    ///
    public delegate* unmanaged<cef_x509_certificate_t*, cef_x509_cert_principal_t*> get_issuer;

    ///
    /// Returns the DER encoded serial number for the X.509 certificate. The value
    /// possibly includes a leading 00 byte.
    ///
    public delegate* unmanaged<cef_x509_certificate_t*, cef_binary_value_t*> get_serial_number;

    ///
    /// Returns the date before which the X.509 certificate is invalid.
    /// CefBaseTime.GetTimeT() will return 0 if no date was specified.
    ///
    public delegate* unmanaged<cef_x509_certificate_t*, cef_basetime_t> get_valid_start;

    ///
    /// Returns the date after which the X.509 certificate is invalid.
    /// CefBaseTime.GetTimeT() will return 0 if no date was specified.
    ///
    public delegate* unmanaged<cef_x509_certificate_t*, cef_basetime_t> get_valid_expiry;

    ///
    /// Returns the DER encoded data for the X.509 certificate.
    ///
    public delegate* unmanaged<cef_x509_certificate_t*, cef_binary_value_t*> get_derencoded;

    ///
    /// Returns the PEM encoded data for the X.509 certificate.
    ///
    public delegate* unmanaged<cef_x509_certificate_t*, cef_binary_value_t*> get_pemencoded;

    ///
    /// Returns the number of certificates in the issuer chain. If 0, the
    /// certificate is self-signed.
    ///
    public delegate* unmanaged<cef_x509_certificate_t*, nuint> get_issuer_chain_size;

    ///
    /// Returns the DER encoded data for the certificate issuer chain. If we
    /// failed to encode a certificate in the chain it is still present in the
    /// array but is an NULL string.
    ///
    public delegate* unmanaged<cef_x509_certificate_t*, nuint*, cef_binary_value_t**, void> get_derencoded_issuer_chain;

    ///
    /// Returns the PEM encoded data for the certificate issuer chain. If we
    /// failed to encode a certificate in the chain it is still present in the
    /// array but is an NULL string.
    ///
    public delegate* unmanaged<cef_x509_certificate_t*, nuint*, cef_binary_value_t**, void> get_pemencoded_issuer_chain;
}
