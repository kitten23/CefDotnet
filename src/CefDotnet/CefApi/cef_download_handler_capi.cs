using CefDotnet.CefApi.Internal;
using CefDotnet.CefApi.Types;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// Callback structure used to asynchronously continue a download.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_before_download_callback_t
{
    ///
    /// Base structure.
    ///
    public cef_base_ref_counted_t @base;

    ///
    /// Call to continue the download. Set |download_path| to the full file path
    /// for the download including the file name or leave blank to use the
    /// suggested name and the default temp directory. Set |show_dialog| to true
    /// (1) if you do wish to show the default "Save As" dialog.
    ///
    public delegate* unmanaged<cef_before_download_callback_t*, cef_string_t*, int, void> cont;
}

///
/// Callback structure used to asynchronously cancel a download.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_download_item_callback_t
{
    ///
    /// Base structure.
    ///
    public cef_base_ref_counted_t @base;

    ///
    /// Call to cancel the download.
    ///
    public delegate* unmanaged<cef_download_item_callback_t*, void> cancel;

    ///
    /// Call to pause the download.
    ///
    public delegate* unmanaged<cef_download_item_callback_t*, void> pause;

    ///
    /// Call to resume the download.
    ///
    public delegate* unmanaged<cef_download_item_callback_t*, void> resume;
}

///
/// Structure used to handle file downloads. The functions of this structure
/// will called on the browser process UI thread.
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_download_handler_t
{
    ///
    /// Base structure.
    ///
    public cef_base_ref_counted_t @base;

    ///
    /// Called before a download begins in response to a user-initiated action
    /// (e.g. alt + link click or link click that returns a `Content-Disposition:
    /// attachment` response from the server). |url| is the target download URL
    /// and |request_function| is the target function (GET, POST, etc). Return
    /// true (1) to proceed with the download or false (0) to cancel the download.
    ///
    public delegate* unmanaged<cef_download_handler_t*, cef_browser_t*, cef_string_t*, cef_string_t*, int> can_download;

    ///
    /// Called before a download begins. |suggested_name| is the suggested name
    /// for the download file. Return true (1) and execute |callback| either
    /// asynchronously or in this function to continue or cancel the download.
    /// Return false (0) to proceed with default handling (cancel with Alloy
    /// style, download shelf with Chrome style). Do not keep a reference to
    /// |download_item| outside of this function.
    ///

    public delegate* unmanaged<cef_download_handler_t*, cef_browser_t*, cef_download_item_t*, cef_string_t*, cef_before_download_callback_t*, int> on_before_download;

    ///
    /// Called when a download's status or progress information has been updated.
    /// This may be called multiple times before and after on_before_download().
    /// Execute |callback| either asynchronously or in this function to cancel the
    /// download if desired. Do not keep a reference to |download_item| outside of
    /// this function.
    ///
    public delegate* unmanaged<cef_download_handler_t*, cef_browser_t*, cef_download_item_t*, cef_download_item_callback_t*, void> on_download_updated;
}

///
/// Structure used to represent a download item.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_download_item_t
{
    ///
    /// Base structure.
    ///
    public cef_base_ref_counted_t @base;

    ///
    /// Returns true (1) if this object is valid. Do not call any other functions
    /// if this function returns false (0).
    ///
    public delegate* unmanaged<cef_download_item_t*, int> is_valid;

    ///
    /// Returns true (1) if the download is in progress.
    ///
    public delegate* unmanaged<cef_download_item_t*, int> is_in_progress;

    ///
    /// Returns true (1) if the download is complete.
    ///
    public delegate* unmanaged<cef_download_item_t*, int> is_complete;

    ///
    /// Returns true (1) if the download has been canceled.
    ///
    public delegate* unmanaged<cef_download_item_t*, int> is_canceled;

    ///
    /// Returns true (1) if the download has been interrupted.
    ///
    public delegate* unmanaged<cef_download_item_t*, int> is_interrupted;

    ///
    /// Returns the most recent interrupt reason.
    ///
    public delegate* unmanaged<cef_download_item_t*, cef_download_interrupt_reason_t> get_interrupt_reason;

    ///
    /// Returns a simple speed estimate in bytes/s.
    ///
    public delegate* unmanaged<cef_download_item_t*, long> get_current_speed;

    ///
    /// Returns the rough percent complete or -1 if the receive total size is
    /// unknown.
    ///
    public delegate* unmanaged<cef_download_item_t*, int> get_percent_complete;

    ///
    /// Returns the total number of bytes.
    ///
    public delegate* unmanaged<cef_download_item_t*, long> get_total_bytes;

    ///
    /// Returns the number of received bytes.
    ///
    public delegate* unmanaged<cef_download_item_t*, long> get_received_bytes;

    ///
    /// Returns the time that the download started.
    ///
    public delegate* unmanaged<cef_download_item_t*, cef_basetime_t> get_start_time;

    ///
    /// Returns the time that the download ended.
    ///
    public delegate* unmanaged<cef_download_item_t*, cef_basetime_t> get_end_time;

    ///
    /// Returns the full path to the downloaded or downloading file.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_download_item_t*, cef_string_t*> get_full_path;

    ///
    /// Returns the unique identifier for this download.
    ///
    public delegate* unmanaged<cef_download_item_t*, uint> get_id;

    ///
    /// Returns the URL.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_download_item_t*, cef_string_t*> get_url;

    ///
    /// Returns the original URL before any redirections.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_download_item_t*, cef_string_t*> get_original_url;

    ///
    /// Returns the suggested file name.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_download_item_t*, cef_string_t*> get_suggested_file_name;

    ///
    /// Returns the content disposition.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_download_item_t*, cef_string_t*> get_content_disposition;

    ///
    /// Returns the mime type.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_download_item_t*, cef_string_t*> get_mime_type;
}

///
/// Download interrupt reasons. Should be kept in sync with
/// Chromium's download::DownloadInterruptReason type.
///
public enum cef_download_interrupt_reason_t
{
    CEF_DOWNLOAD_INTERRUPT_REASON_NONE = 0,

    /// Generic file operation failure.
    CEF_DOWNLOAD_INTERRUPT_REASON_FILE_FAILED = 1,

    /// The file cannot be accessed due to security restrictions.
    CEF_DOWNLOAD_INTERRUPT_REASON_FILE_ACCESS_DENIED = 2,

    /// There is not enough room on the drive.
    CEF_DOWNLOAD_INTERRUPT_REASON_FILE_NO_SPACE = 3,

    /// The directory or file name is too long.
    CEF_DOWNLOAD_INTERRUPT_REASON_FILE_NAME_TOO_LONG = 5,

    /// The file is too large for the file system to handle.
    CEF_DOWNLOAD_INTERRUPT_REASON_FILE_TOO_LARGE = 6,

    /// The file contains a virus.
    CEF_DOWNLOAD_INTERRUPT_REASON_FILE_VIRUS_INFECTED = 7,

    /// The file was in use. Too many files are opened at once. We have run out of
    /// memory.
    CEF_DOWNLOAD_INTERRUPT_REASON_FILE_TRANSIENT_ERROR = 10,

    /// The file was blocked due to local policy.
    CEF_DOWNLOAD_INTERRUPT_REASON_FILE_BLOCKED = 11,

    /// An attempt to check the safety of the download failed due to unexpected
    /// reasons. See http://crbug.com/153212.
    CEF_DOWNLOAD_INTERRUPT_REASON_FILE_SECURITY_CHECK_FAILED = 12,

    /// An attempt was made to seek past the end of a file in opening
    /// a file (as part of resuming a previously interrupted download).
    CEF_DOWNLOAD_INTERRUPT_REASON_FILE_TOO_SHORT = 13,

    /// The partial file didn't match the expected hash.
    CEF_DOWNLOAD_INTERRUPT_REASON_FILE_HASH_MISMATCH = 14,

    /// The source and the target of the download were the same.
    CEF_DOWNLOAD_INTERRUPT_REASON_FILE_SAME_AS_SOURCE = 15,

    // Network errors.

    /// Generic network failure.
    CEF_DOWNLOAD_INTERRUPT_REASON_NETWORK_FAILED = 20,

    /// The network operation timed out.
    CEF_DOWNLOAD_INTERRUPT_REASON_NETWORK_TIMEOUT = 21,

    /// The network connection has been lost.
    CEF_DOWNLOAD_INTERRUPT_REASON_NETWORK_DISCONNECTED = 22,

    /// The server has gone down.
    CEF_DOWNLOAD_INTERRUPT_REASON_NETWORK_SERVER_DOWN = 23,

    /// The network request was invalid. This may be due to the original URL or a
    /// redirected URL:
    /// - Having an unsupported scheme.
    /// - Being an invalid URL.
    /// - Being disallowed by policy.
    CEF_DOWNLOAD_INTERRUPT_REASON_NETWORK_INVALID_REQUEST = 24,

    // Server responses.

    /// The server indicates that the operation has failed (generic).
    CEF_DOWNLOAD_INTERRUPT_REASON_SERVER_FAILED = 30,

    /// The server does not support range requests.
    /// Internal use only:  must restart from the beginning.
    CEF_DOWNLOAD_INTERRUPT_REASON_SERVER_NO_RANGE = 31,

    /// The server does not have the requested data.
    CEF_DOWNLOAD_INTERRUPT_REASON_SERVER_BAD_CONTENT = 33,

    /// Server didn't authorize access to resource.
    CEF_DOWNLOAD_INTERRUPT_REASON_SERVER_UNAUTHORIZED = 34,

    /// Server certificate problem.
    CEF_DOWNLOAD_INTERRUPT_REASON_SERVER_CERT_PROBLEM = 35,

    /// Server access forbidden.
    CEF_DOWNLOAD_INTERRUPT_REASON_SERVER_FORBIDDEN = 36,

    /// Unexpected server response. This might indicate that the responding server
    /// may not be the intended server.
    CEF_DOWNLOAD_INTERRUPT_REASON_SERVER_UNREACHABLE = 37,

    /// The server sent fewer bytes than the content-length header. It may
    /// indicate that the connection was closed prematurely, or the Content-Length
    /// header was invalid. The download is only interrupted if strong validators
    /// are present. Otherwise, it is treated as finished.
    CEF_DOWNLOAD_INTERRUPT_REASON_SERVER_CONTENT_LENGTH_MISMATCH = 38,

    /// An unexpected cross-origin redirect happened.
    CEF_DOWNLOAD_INTERRUPT_REASON_SERVER_CROSS_ORIGIN_REDIRECT = 39,

    // User input.

    /// The user canceled the download.
    CEF_DOWNLOAD_INTERRUPT_REASON_USER_CANCELED = 40,

    /// The user shut down the browser.
    /// Internal use only:  resume pending downloads if possible.
    CEF_DOWNLOAD_INTERRUPT_REASON_USER_SHUTDOWN = 41,

    // Crash.

    /// The browser crashed.
    /// Internal use only:  resume pending downloads if possible.
    CEF_DOWNLOAD_INTERRUPT_REASON_CRASH = 50,
}
