using CefDotnet.CefApi;

namespace CefDotnet.CefWrap;

/// <summary>
/// just a unsafe wrap
/// </summary>
unsafe public class RefCefRequestContext : Ref<cef_request_context_t>
{
    public static RefCefRequestContext GetGlobalRequestContext()
    {
        return new(libcef.cef_request_context_get_global_context());
    }

    public RefCefRequestContext(cef_request_context_t* ptr) : base(ptr)
    {
    }
}
