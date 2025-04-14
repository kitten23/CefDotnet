using CefDotnet.CefApi.Types;
using System;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// Callback for asynchronous continuation of cef_resource_handler_t::skip().
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_resource_skip_callback_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Callback for asynchronous continuation of skip(). If |bytes_skipped| > 0
    /// then either skip() will be called again until the requested number of
    /// bytes have been skipped or the request will proceed. If |bytes_skipped| <=
    /// 0 the request will fail with ERR_REQUEST_RANGE_NOT_SATISFIABLE.
    ///
    public delegate* unmanaged<cef_resource_skip_callback_t*, long, void> cont;
}

///
/// Callback for asynchronous continuation of cef_resource_handler_t::read().
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_resource_read_callback_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Callback for asynchronous continuation of read(). If |bytes_read| == 0 the
    /// response will be considered complete. If |bytes_read| > 0 then read() will
    /// be called again until the request is complete (based on either the result
    /// or the expected content length). If |bytes_read| < 0 then the request will
    /// fail and the |bytes_read| value will be treated as the error code.
    ///
    public delegate* unmanaged<cef_resource_read_callback_t*, int, void> cont;
}

///
/// Structure used to implement a custom request handler structure. The
/// functions of this structure will be called on the IO thread unless otherwise
/// indicated.
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_resource_handler_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Open the response stream. To handle the request immediately set
    /// |handle_request| to true (1) and return true (1). To decide at a later
    /// time set |handle_request| to false (0), return true (1), and execute
    /// |callback| to continue or cancel the request. To cancel the request
    /// immediately set |handle_request| to true (1) and return false (0). This
    /// function will be called in sequence but not from a dedicated thread. For
    /// backwards compatibility set |handle_request| to false (0) and return false
    /// (0) and the ProcessRequest function will be called.
    ///
    public delegate* unmanaged<cef_resource_handler_t*, cef_request_t*, int*, cef_callback_t*, int> open;

    ///
    /// Begin processing the request. To handle the request return true (1) and
    /// call cef_callback_t::cont() once the response header information is
    /// available (cef_callback_t::cont() can also be called from inside this
    /// function if header information is available immediately). To cancel the
    /// request return false (0).
    ///
    /// WARNING: This function is deprecated. Use Open instead.
    ///
    public delegate* unmanaged<cef_resource_handler_t*, cef_request_t*, cef_callback_t*, int> process_request;

    ///
    /// Retrieve response header information. If the response length is not known
    /// set |response_length| to -1 and read_response() will be called until it
    /// returns false (0). If the response length is known set |response_length|
    /// to a positive value and read_response() will be called until it returns
    /// false (0) or the specified number of bytes have been read. Use the
    /// |response| object to set the mime type, http status code and other
    /// optional header values. To redirect the request to a new URL set
    /// |redirectUrl| to the new URL. |redirectUrl| can be either a relative or
    /// fully qualified URL. It is also possible to set |response| to a redirect
    /// http status code and pass the new URL via a Location header. Likewise with
    /// |redirectUrl| it is valid to set a relative or fully qualified URL as the
    /// Location header value. If an error occured while setting up the request
    /// you can call set_error() on |response| to indicate the error condition.
    ///
    public delegate* unmanaged<cef_resource_handler_t*, cef_response_t*, long*, cef_string_t*, void> get_response_headers;

    ///
    /// Skip response data when requested by a Range header. Skip over and discard
    /// |bytes_to_skip| bytes of response data. If data is available immediately
    /// set |bytes_skipped| to the number of bytes skipped and return true (1). To
    /// read the data at a later time set |bytes_skipped| to 0, return true (1)
    /// and execute |callback| when the data is available. To indicate failure set
    /// |bytes_skipped| to < 0 (e.g. -2 for ERR_FAILED) and return false (0). This
    /// function will be called in sequence but not from a dedicated thread.
    ///
    public delegate* unmanaged<cef_resource_handler_t*, long, long*, cef_resource_skip_callback_t*, int> skip;

    ///
    /// Read response data. If data is available immediately copy up to
    /// |bytes_to_read| bytes into |data_out|, set |bytes_read| to the number of
    /// bytes copied, and return true (1). To read the data at a later time keep a
    /// pointer to |data_out|, set |bytes_read| to 0, return true (1) and execute
    /// |callback| when the data is available (|data_out| will remain valid until
    /// the callback is executed). To indicate response completion set
    /// |bytes_read| to 0 and return false (0). To indicate failure set
    /// |bytes_read| to < 0 (e.g. -2 for ERR_FAILED) and return false (0). This
    /// function will be called in sequence but not from a dedicated thread. For
    /// backwards compatibility set |bytes_read| to -1 and return false (0) and
    /// the ReadResponse function will be called.
    ///
    public delegate* unmanaged<cef_resource_handler_t*, void*, int, int*, cef_resource_read_callback_t*, int> read;

    ///
    /// Read response data. If data is available immediately copy up to
    /// |bytes_to_read| bytes into |data_out|, set |bytes_read| to the number of
    /// bytes copied, and return true (1). To read the data at a later time set
    /// |bytes_read| to 0, return true (1) and call cef_callback_t::cont() when
    /// the data is available. To indicate response completion return false (0).
    ///
    /// WARNING: This function is deprecated. Use Skip and Read instead.
    ///
    public delegate* unmanaged<cef_resource_handler_t*, void*, int, int*, cef_callback_t*, int> read_response;

    ///
    /// Request processing has been canceled.
    ///
    public delegate* unmanaged<cef_resource_handler_t*, void> cancel;
}
