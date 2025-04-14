using CefDotnet.CefApi.Types;
using System;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// Generic callback structure used for asynchronous continuation.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_callback_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Continue processing.
    ///
    public delegate* unmanaged<cef_callback_t*, void> cont;

    ///
    /// Cancel processing.
    ///
    public delegate* unmanaged<cef_callback_t*, void> cancel;
}

///
/// Generic callback structure used for asynchronous completion.
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_completion_callback_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Method that will be called once the task is complete.
    ///
    public delegate* unmanaged<cef_completion_callback_t*, void> on_complete;
}
