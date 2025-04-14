using CefDotnet.CefApi.Types;
using System;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// Flags used to customize the behavior of CefURLRequest.
///
public enum cef_urlrequest_flags_t:uint
{
    ///
    /// Default behavior.
    ///
    UR_FLAG_NONE = 0,

    ///
    /// If set the cache will be skipped when handling the request. Setting this
    /// value is equivalent to specifying the "Cache-Control: no-cache" request
    /// header. Setting this value in combination with UR_FLAG_ONLY_FROM_CACHE
    /// will cause the request to fail.
    ///
    UR_FLAG_SKIP_CACHE = 1 << 0,

    ///
    /// If set the request will fail if it cannot be served from the cache (or
    /// some equivalent local store). Setting this value is equivalent to
    /// specifying the "Cache-Control: only-if-cached" request header. Setting
    /// this value in combination with UR_FLAG_SKIP_CACHE or UR_FLAG_DISABLE_CACHE
    /// will cause the request to fail.
    ///
    UR_FLAG_ONLY_FROM_CACHE = 1 << 1,

    ///
    /// If set the cache will not be used at all. Setting this value is equivalent
    /// to specifying the "Cache-Control: no-store" request header. Setting this
    /// value in combination with UR_FLAG_ONLY_FROM_CACHE will cause the request
    /// to fail.
    ///
    UR_FLAG_DISABLE_CACHE = 1 << 2,

    ///
    /// If set user name, password, and cookies may be sent with the request, and
    /// cookies may be saved from the response.
    ///
    UR_FLAG_ALLOW_STORED_CREDENTIALS = 1 << 3,

    ///
    /// If set upload progress events will be generated when a request has a body.
    ///
    UR_FLAG_REPORT_UPLOAD_PROGRESS = 1 << 4,

    ///
    /// If set the CefURLRequestClient::OnDownloadData method will not be called.
    ///
    UR_FLAG_NO_DOWNLOAD_DATA = 1 << 5,

    ///
    /// If set 5XX redirect errors will be propagated to the observer instead of
    /// automatically re-tried. This currently only applies for requests
    /// originated in the browser process.
    ///
    UR_FLAG_NO_RETRY_ON_5XX = 1 << 6,

    ///
    /// If set 3XX responses will cause the fetch to halt immediately rather than
    /// continue through the redirect.
    ///
    UR_FLAG_STOP_ON_REDIRECT = 1 << 7,
}

///
/// Flags that represent CefURLRequest status.
///
public enum cef_urlrequest_status_t:int
{
    ///
    /// Unknown status.
    ///
    UR_UNKNOWN,

    ///
    /// Request succeeded.
    ///
    UR_SUCCESS,

    ///
    /// An IO request is pending, and the caller will be informed when it is
    /// completed.
    ///
    UR_IO_PENDING,

    ///
    /// Request was canceled programatically.
    ///
    UR_CANCELED,

    ///
    /// Request failed for some reason.
    ///
    UR_FAILED,

    UR_NUM_VALUES,
}

///
/// Structure used to make a URL request. URL requests are not associated with a
/// browser instance so no cef_client_t callbacks will be executed. URL requests
/// can be created on any valid CEF thread in either the browser or render
/// process. Once created the functions of the URL request object must be
/// accessed on the same thread that created it.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_urlrequest_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Returns the request object used to create this URL request. The returned
    /// object is read-only and should not be modified.
    ///
    public delegate* unmanaged<cef_urlrequest_t*, cef_request_t*> get_request;

    ///
    /// Returns the client.
    ///
    public delegate* unmanaged<cef_urlrequest_t*, cef_urlrequest_client_t*> get_client;

    ///
    /// Returns the request status.
    ///
    public delegate* unmanaged<cef_urlrequest_t*, cef_urlrequest_status_t> get_request_status;

    ///
    /// Returns the request error if status is UR_CANCELED or UR_FAILED, or 0
    /// otherwise.
    ///
    public delegate* unmanaged<cef_urlrequest_t*, cef_errorcode_t> get_request_error;

    ///
    /// Returns the response, or NULL if no response information is available.
    /// Response information will only be available after the upload has
    /// completed. The returned object is read-only and should not be modified.
    ///
    public delegate* unmanaged<cef_urlrequest_t*, cef_response_t*> get_response;

    ///
    /// Returns true (1) if the response body was served from the cache. This
    /// includes responses for which revalidation was required.
    ///
    public delegate* unmanaged<cef_urlrequest_t*, int> response_was_cached;

    ///
    /// Cancel the request.
    ///
    public delegate* unmanaged<cef_urlrequest_t*, void> cancel;
}

///
/// Structure that should be implemented by the cef_urlrequest_t client. The
/// functions of this structure will be called on the same thread that created
/// the request unless otherwise documented.
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_urlrequest_client_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Notifies the client that the request has completed. Use the
    /// cef_urlrequest_t::GetRequestStatus function to determine if the request
    /// was successful or not.
    ///
    public delegate* unmanaged<cef_urlrequest_client_t*, cef_urlrequest_t*, void> on_request_complete;

    ///
    /// Notifies the client of upload progress. |current| denotes the number of
    /// bytes sent so far and |total| is the total size of uploading data (or -1
    /// if chunked upload is enabled). This function will only be called if the
    /// UR_FLAG_REPORT_UPLOAD_PROGRESS flag is set on the request.
    ///
    public delegate* unmanaged<cef_urlrequest_client_t*, cef_urlrequest_t*, long, long, void> on_upload_progress;

    ///
    /// Notifies the client of download progress. |current| denotes the number of
    /// bytes received up to the call and |total| is the expected total size of
    /// the response (or -1 if not determined).
    ///
    public delegate* unmanaged<cef_urlrequest_client_t*, cef_urlrequest_t*, long, long, void> on_download_progress;

    ///
    /// Called when some part of the response is read. |data| contains the current
    /// bytes received since the last call. This function will not be called if
    /// the UR_FLAG_NO_DOWNLOAD_DATA flag is set on the request.
    ///
    public delegate* unmanaged<cef_urlrequest_client_t*, cef_urlrequest_t*, void*, nuint, void> on_download_data;

    ///
    /// Called on the IO thread when the browser needs credentials from the user.
    /// |isProxy| indicates whether the host is a proxy server. |host| contains
    /// the hostname and |port| contains the port number. Return true (1) to
    /// continue the request and call cef_auth_callback_t::cont() when the
    /// authentication information is available. If the request has an associated
    /// browser/frame then returning false (0) will result in a call to
    /// GetAuthCredentials on the cef_request_handler_t associated with that
    /// browser, if any. Otherwise, returning false (0) will cancel the request
    /// immediately. This function will only be called for requests initiated from
    /// the browser process.
    ///
    public delegate* unmanaged<cef_urlrequest_client_t*, int, cef_string_t*, int, cef_string_t*, cef_string_t*, cef_auth_callback_t*, int> get_auth_credentials;
}
