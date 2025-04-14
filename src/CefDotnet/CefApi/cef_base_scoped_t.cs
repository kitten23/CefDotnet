using CefDotnet.CefApi.Types;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// All scoped framework structures must include this structure first.
///
[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_base_scoped_t
{
    //public cef_base_scoped_t()
    //{
    //    size = (nuint)sizeof(cef_base_scoped_t);
    //}

    ///
    /// Size of the data structure.
    ///
    nuint size;

    ///
    /// Called to delete this object. May be NULL if the object is not owned.
    ///
    public delegate* unmanaged<cef_base_scoped_t*, void> del;

}