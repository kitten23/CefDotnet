using CefDotnet.CefApi.Types;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// Return values for CefResponseFilter::Filter().
///
public enum cef_response_filter_status_t
{
    ///
    /// Some or all of the pre-filter data was read successfully but more data is
    /// needed in order to continue filtering (filtered output is pending).
    ///
    RESPONSE_FILTER_NEED_MORE_DATA,

    ///
    /// Some or all of the pre-filter data was read successfully and all available
    /// filtered output has been written.
    ///
    RESPONSE_FILTER_DONE,

    ///
    /// An error occurred during filtering.
    ///
    RESPONSE_FILTER_ERROR
}

///
/// Implement this structure to filter resource response content. The functions
/// of this structure will be called on the browser process IO thread.
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_response_filter_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Initialize the response filter. Will only be called a single time. The
    /// filter will not be installed if this function returns false (0).
    ///
    public delegate* unmanaged<cef_response_filter_t*, int> init_filter;

    ///
    /// Called to filter a chunk of data. Expected usage is as follows:
    ///
    ///  1. Read input data from |data_in| and set |data_in_read| to the number of
    ///     bytes that were read up to a maximum of |data_in_size|. |data_in| will
    ///     be NULL if |data_in_size| is zero.
    ///  2. Write filtered output data to |data_out| and set |data_out_written| to
    ///     the number of bytes that were written up to a maximum of
    ///     |data_out_size|. If no output data was written then all data must be
    ///     read from |data_in| (user must set |data_in_read| = |data_in_size|).
    ///  3. Return RESPONSE_FILTER_DONE if all output data was written or
    ///     RESPONSE_FILTER_NEED_MORE_DATA if output data is still pending.
    ///
    /// This function will be called repeatedly until the input buffer has been
    /// fully read (user sets |data_in_read| = |data_in_size|) and there is no
    /// more input data to filter (the resource response is complete). This
    /// function may then be called an additional time with an NULL input buffer
    /// if the user filled the output buffer (set |data_out_written| =
    /// |data_out_size|) and returned RESPONSE_FILTER_NEED_MORE_DATA to indicate
    /// that output data is still pending.
    ///
    /// Calls to this function will stop when one of the following conditions is
    /// met:
    ///
    ///  1. There is no more input data to filter (the resource response is
    ///     complete) and the user sets |data_out_written| = 0 or returns
    ///     RESPONSE_FILTER_DONE to indicate that all data has been written, or;
    ///  2. The user returns RESPONSE_FILTER_ERROR to indicate an error.
    ///
    /// Do not keep a reference to the buffers passed to this function.
    ///
    public delegate* unmanaged<cef_response_filter_t*, void*, nuint, nuint*, void*, nuint, nuint*, cef_response_filter_status_t> filter;
}
