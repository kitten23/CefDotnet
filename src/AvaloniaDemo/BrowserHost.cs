using Avalonia.Controls;
using Avalonia.Platform;
using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaDemo;

public class BrowserHost : NativeControlHost
{
    public BrowserHost(nint handle)
    {
        Handle = handle;
    }

    public nint Handle { get; }

    protected override IPlatformHandle CreateNativeControlCore(IPlatformHandle parent)
    {
        return new PlatformHandle(Handle, "HWND");
    }

    protected override void DestroyNativeControlCore(IPlatformHandle control)
    {
        base.DestroyNativeControlCore(control);
    }
}
