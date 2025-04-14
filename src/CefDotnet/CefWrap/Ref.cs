using CefDotnet.CefApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CefDotnet.CefWrap.CefBaseRefCountedHolder;

namespace CefDotnet.CefWrap;

unsafe public static class Ref
{
    public static void AddRef<T>(T* ptr)
    {
        cef_base_ref_counted_t* p = (cef_base_ref_counted_t*)ptr;
        p->add_ref(p);
    }

    public static void Release<T>(T* ptr)
    {
        cef_base_ref_counted_t* p = (cef_base_ref_counted_t*)ptr;
        p->release(p);
    }
}

unsafe public class Ref<T> : IDisposable
{
    public Ref(T* ptr)
    {
        Ptr = ptr;
        Ref.AddRef(ptr);
    }

    private bool disposedValue;
    public T* Ptr { get; protected set; }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
            }

            if (Ptr != null)
            {
                Ref.Release(Ptr);
                Ptr = null;
            }
            disposedValue = true;
        }
    }

    ~Ref()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}

unsafe public abstract class CefRefDebug<T> : Ref<T>
{
    public delegate cef_base_ref_counted_t* GetBaseRef();

    public CefRefDebug(T* ptr, string id) : base(ptr)
    {
        Id = id;
    }

    public string Id { get; }

    private bool disposedValue = false;
    protected override void Dispose(bool disposing)
    {
        if (disposedValue)
        {
            return;
        }

        disposedValue = true;

        cef_base_ref_counted_t* p = (cef_base_ref_counted_t*)Ptr;
        //Debug.WriteLine($"[{Id}] | Dispose, ref one={p->has_one_ref(p)}, has ref={p->has_at_least_one_ref(p)}");
        Debug.WriteLine($"DebugCefRef | Dispose [{Id}]");
        base.Dispose(disposing);
    }
}
