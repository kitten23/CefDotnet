using System.Runtime.InteropServices;

namespace CefDotnet.CefApi.Types;

// Build with the UTF16 string type as default.
[StructLayout(LayoutKind.Sequential)]
unsafe public ref struct cef_string_t
{
    public char* str;
    public nuint length;
    public nint dtor;

    public cef_string_t(char* p, int len)
    {
        str = p;
        length = (nuint)len;
    }
}