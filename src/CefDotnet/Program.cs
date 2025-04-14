using CefDotnet.CefApi;
using CefDotnet.CefApi.Internal;
using CefDotnet.CefWrap;
using CefDotnet.Win;
using System.Diagnostics;

namespace CefDotnet;

//if run as sub process
internal class Program
{
    unsafe static void Main(string[] args)
    {
        cef_main_args_t mainArgs = new()
        {
            instance = kernel32.GetModuleHandleW(null)
        };

        Debug.WriteLine("sub | cef_execute_process");
        var exitCode = libcef.cef_execute_process(&mainArgs, null, IntPtr.Zero);
        Debug.WriteLine($"sub | cef_execute_process ret = {exitCode}");

        if (exitCode != -1)
        {
            Environment.Exit(exitCode);
        }

    }
}
