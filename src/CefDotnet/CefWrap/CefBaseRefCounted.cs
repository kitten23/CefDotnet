using CefDotnet.CefApi;
using CefDotnet.CefApi.Types;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace CefDotnet.CefWrap;

unsafe public class CefBaseRefCounted<T> : IDisposable
{
    static ConcurrentDictionary<CefBaseRefCountedHolder, byte> GlobalRefDict = [];

    public CefBaseRefCounted()
    {
        _Holder = new CefBaseRefCountedHolder(sizeof(T), GlobalRefDict);
    }

    protected CefBaseRefCountedHolder _Holder;

    #region IDisposable
    private bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
            }

            _Holder.GcFree();
            disposedValue = true;
        }
    }
    ~CefBaseRefCounted()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    #endregion

    public T* Ptr => (T*)_Holder.Handle;

    public T* GetRef()
    {
        _Holder.AddRef(_Holder._ptr);
        return Ptr;
    }
}

unsafe public class CefBaseRefCountedHolder : IDisposable
{
    public delegate void Act(cef_base_ref_counted_t* self);
    public delegate int Func(cef_base_ref_counted_t* self);

    public Act AddRef;
    public Func Release;
    public Func HasOneRef;
    public Func HasAtLeastOneRef;

    public nint Handle { get; private set; }
    public int _NativeRefCount = 0;//dll side ref count
    public cef_base_ref_counted_t* _ptr;

    readonly ConcurrentDictionary<CefBaseRefCountedHolder, byte> GlobalRefDict;
    bool _IsGloabl = false;

    public CefBaseRefCountedHolder(int size, ConcurrentDictionary<CefBaseRefCountedHolder, byte> globalRefDict)
    {
        GlobalRefDict = globalRefDict;
        Handle = Marshal.AllocHGlobal(size);
        AddRef = (cef_base_ref_counted_t* p) =>
        {
            var ret = Interlocked.Increment(ref _NativeRefCount);
        };
        Release = (cef_base_ref_counted_t* p) =>
        {
            var ret = Interlocked.Decrement(ref _NativeRefCount);
            if (ret == 0)
            {
                if (_IsGloabl)
                {
                    GlobalRefDict.TryRemove(this, out _);
                }
            }
            return ret > 0 ? 0 : 1;
        };
        HasOneRef = (cef_base_ref_counted_t* p) =>
        {
            return _NativeRefCount == 1 ? 1 : 0;
        };
        HasAtLeastOneRef = (cef_base_ref_counted_t* p) =>
        {
            return _NativeRefCount > 0 ? 1 : 0;
        };

        _ptr = (cef_base_ref_counted_t*)Handle;
        _ptr->size = (nuint)size;
        _ptr->add_ref = (delegate* unmanaged<cef_base_ref_counted_t*, void>)Marshal.GetFunctionPointerForDelegate(AddRef);
        _ptr->release = (delegate* unmanaged<cef_base_ref_counted_t*, int>)Marshal.GetFunctionPointerForDelegate(Release);
        _ptr->has_one_ref = (delegate* unmanaged<cef_base_ref_counted_t*, int>)Marshal.GetFunctionPointerForDelegate(HasOneRef);
        _ptr->has_at_least_one_ref = (delegate* unmanaged<cef_base_ref_counted_t*, int>)Marshal.GetFunctionPointerForDelegate(HasAtLeastOneRef);
    }

    #region IDisposable
    private bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
            }

            if (Handle != nint.Zero)
            {
                Marshal.FreeHGlobal(Handle);
                Handle = nint.Zero;
            }

            disposedValue = true;
        }
    }

    ~CefBaseRefCountedHolder()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    #endregion

    public void GcFree()
    {
        if (_NativeRefCount > 0)
        {
            GlobalRefDict.TryAdd(this, 0);
            _IsGloabl = true;
        }
    }
}

public unsafe class CefBaseRefCountedDebug<T> : IDisposable
{
    static ConcurrentDictionary<CefBaseRefCountedHolder, byte> GlobalRefDict = [];
    public CefBaseRefCountedDebug(string name)
    {
        _Holder = new CefBaseRefCountedHolderDebug(sizeof(T), GlobalRefDict, name);
    }

    protected CefBaseRefCountedHolderDebug _Holder;

    #region IDisposable
    private bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
            }

            Debug.WriteLine($"[{_Holder.Name}] | Dispose, count={_Holder._NativeRefCount}");
            _Holder.GcFree();

            disposedValue = true;
        }
    }
    ~CefBaseRefCountedDebug()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    #endregion

    public T* Ptr => (T*)_Holder.Handle;

    public T* GetNativeRef()
    {
        _Holder.AddRef(_Holder._ptr);
        return Ptr;
    }

}

public unsafe class CefBaseRefCountedHolderDebug : CefBaseRefCountedHolder
{
    public CefBaseRefCountedHolderDebug(int size, ConcurrentDictionary<CefBaseRefCountedHolder, byte> dict, string name) : base(size, dict)
    {
        Name = name;
        var baseAdd = AddRef;
        AddRef = (cef_base_ref_counted_t* p) =>
        {
            //if (_NativeRefCount < 3)
            {
                Debug.WriteLine($"{name} [{_id}] add = {_NativeRefCount}");
            }
            baseAdd(p);
        };

        var baseRelease = Release;
        Release = (cef_base_ref_counted_t* p) =>
        {
            //if (_NativeRefCount < 3)
            {
                Debug.WriteLine($"{name} [{_id}] release = {_NativeRefCount}");
            }
            return baseRelease(p);
        };

        _ptr->add_ref = (delegate* unmanaged<cef_base_ref_counted_t*, void>)Marshal.GetFunctionPointerForDelegate(AddRef);
        _ptr->release = (delegate* unmanaged<cef_base_ref_counted_t*, int>)Marshal.GetFunctionPointerForDelegate(Release);
    }

    public int _id = Random.Shared.Next();
    public string Name { get; }
}