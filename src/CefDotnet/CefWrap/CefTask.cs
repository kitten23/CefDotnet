using CefDotnet.CefApi;
using CefDotnet.CefApi.Types;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CefDotnet.CefWrap;

unsafe public class CefTask : CefBaseRefCounted<cef_task_t>
{
    public CefTask(Action action)
    {
        _execute = (cef_task_t* self) =>
        {
            action();
        };

        Ptr->execute = (delegate* unmanaged<cef_task_t*, void>)Marshal.GetFunctionPointerForDelegate(_execute);
    }

    delegate_execute _execute;

    protected delegate void delegate_execute(cef_task_t* task);
}
