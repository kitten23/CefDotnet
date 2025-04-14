using CefDotnet.CefApi.Internal;
using CefDotnet.CefApi.Types;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// Supported certificate status code values. See net\cert\cert_status_flags.h
/// for more information. CERT_STATUS_NONE is new in CEF because we use an
/// enum while cert_status_flags.h uses a public and static const variables.
///
public enum cef_cert_status_t : uint
{
    CERT_STATUS_NONE = 0,
    CERT_STATUS_COMMON_NAME_INVALID = 1 << 0,
    CERT_STATUS_DATE_INVALID = 1 << 1,
    CERT_STATUS_AUTHORITY_INVALID = 1 << 2,
    // 1 << 3 is reserved for ERR_CERT_CONTAINS_ERRORS (not useful with WinHTTP).
    CERT_STATUS_NO_REVOCATION_MECHANISM = 1 << 4,
    CERT_STATUS_UNABLE_TO_CHECK_REVOCATION = 1 << 5,
    CERT_STATUS_REVOKED = 1 << 6,
    CERT_STATUS_INVALID = 1 << 7,
    CERT_STATUS_WEAK_SIGNATURE_ALGORITHM = 1 << 8,
    // 1 << 9 was used for CERT_STATUS_NOT_IN_DNS
    CERT_STATUS_NON_UNIQUE_NAME = 1 << 10,
    CERT_STATUS_WEAK_KEY = 1 << 11,
    // 1 << 12 was used for CERT_STATUS_WEAK_DH_KEY
    CERT_STATUS_PINNED_KEY_MISSING = 1 << 13,
    CERT_STATUS_NAME_CONSTRAINT_VIOLATION = 1 << 14,
    CERT_STATUS_VALIDITY_TOO_LONG = 1 << 15,

    // Bits 16 to 31 are for non-error statuses.
    CERT_STATUS_IS_EV = 1 << 16,
    CERT_STATUS_REV_CHECKING_ENABLED = 1 << 17,
    // Bit 18 was CERT_STATUS_IS_DNSSEC
    CERT_STATUS_SHA1_SIGNATURE_PRESENT = 1 << 19,
    CERT_STATUS_CT_COMPLIANCE_FAILED = 1 << 20,
}

/// Supported SSL version values. See net/ssl/ssl_connection_status_flags.h
/// for more information.
public enum cef_ssl_version_t:int
{
    /// Unknown SSL version.
    SSL_CONNECTION_VERSION_UNKNOWN,
    SSL_CONNECTION_VERSION_SSL2,
    SSL_CONNECTION_VERSION_SSL3,
    SSL_CONNECTION_VERSION_TLS1,
    SSL_CONNECTION_VERSION_TLS1_1,
    SSL_CONNECTION_VERSION_TLS1_2,
    SSL_CONNECTION_VERSION_TLS1_3,
    SSL_CONNECTION_VERSION_QUIC,
    SSL_CONNECTION_VERSION_NUM_VALUES,
}
;

/// Supported SSL content status flags. See content/public/common/ssl_status.h
/// for more information.
public enum cef_ssl_content_status_t : uint
{
    SSL_CONTENT_NORMAL_CONTENT = 0,
    SSL_CONTENT_DISPLAYED_INSECURE_CONTENT = 1 << 0,
    SSL_CONTENT_RAN_INSECURE_CONTENT = 1 << 1,
}

///
/// Structure representing the SSL information for a navigation entry.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_sslstatus_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Returns true (1) if the status is related to a secure SSL/TLS connection.
    ///
    public delegate* unmanaged<cef_sslstatus_t*, int> is_secure_connection;

    ///
    /// Returns a bitmask containing any and all problems verifying the server
    /// certificate.
    ///
    public delegate* unmanaged<cef_sslstatus_t*, cef_cert_status_t> get_cert_status;

    ///
    /// Returns the SSL version used for the SSL connection.
    ///
    public delegate* unmanaged<cef_sslstatus_t*, cef_ssl_version_t> get_sslversion;

    ///
    /// Returns a bitmask containing the page security content status.
    ///
    public delegate* unmanaged<cef_sslstatus_t*, cef_ssl_content_status_t> get_content_status;

    ///
    /// Returns the X.509 certificate.
    ///
    public delegate* unmanaged<cef_sslstatus_t*, cef_x509_certificate_t*> get_x509_certificate;
}