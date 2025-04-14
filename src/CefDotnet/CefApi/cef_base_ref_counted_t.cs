using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CefDotnet.CefApi;

///
/// All ref-counted framework structures must include this structure first.
///
[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_base_ref_counted_t
{
    ///
    /// Size of the data structure.
    ///
    public nuint size;

    ///
    /// Called to increment the reference count for the object. Should be called
    /// for every new copy of a pointer to a given object.
    ///
    public delegate* unmanaged<cef_base_ref_counted_t*, void> add_ref;

    ///
    /// Called to decrement the reference count for the object. If the reference
    /// count falls to 0 the object should self-delete. Returns true (1) if the
    /// resulting reference count is 0.
    ///
    public delegate* unmanaged<cef_base_ref_counted_t*, int> release;

    ///
    /// Returns true (1) if the current reference count is 1.
    ///
    public delegate* unmanaged<cef_base_ref_counted_t*, int> has_one_ref;

    ///
    /// Returns true (1) if the current reference count is at least 1.
    ///
    public delegate* unmanaged<cef_base_ref_counted_t*, int> has_at_least_one_ref;
}