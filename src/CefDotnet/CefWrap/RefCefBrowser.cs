using CefDotnet.CefApi;
using System.Diagnostics;

namespace CefDotnet.CefWrap;

/// <summary>
/// just a unsafe wrap
/// </summary>
unsafe public class RefCefBrowser : Ref<cef_browser_t>
{
    public RefCefBrowser(cef_browser_t* ptr) : base(ptr)
    {
        Debug.WriteLine($"RefCefBrowser [{ptr->get_identifier(ptr)}]");
#if DEBUG
        Cef.AddBrowserRef(this);
#endif
    }

#if DEBUG
    private bool disposedValue;

    protected override void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing) { }

            Cef.RemoveBrowserRef(this);
            disposedValue = true;
        }
        base.Dispose(disposing);
    }
#endif

    public bool IsValid => Ptr->is_valid(Ptr) == 1;

    public Ref<cef_browser_host_t> GetHost()
    {
        return new(Ptr->get_host(Ptr));
    }

    public Ref<cef_frame_t> GetMainFrame()
    {
        return new(Ptr->get_main_frame(Ptr));
    }

    public int GetIdentifier()
    {
        return Ptr->get_identifier(Ptr);
    }

    public bool IsSame(cef_browser_t* other)
    {
        Ref.AddRef(other);
        return 1 == Ptr->is_same(Ptr, other);
    }

}
