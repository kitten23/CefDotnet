using System.Runtime.InteropServices;

namespace CefDotnet.Win;

public static partial class kernel32
{
    public const string LibName = "kernel32";

    [LibraryImport(LibName, SetLastError = true, StringMarshalling = StringMarshalling.Utf16)]
    public static partial IntPtr GetModuleHandleW(string? lpModuleName);
}
