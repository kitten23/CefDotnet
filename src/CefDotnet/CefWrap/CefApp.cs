using CefDotnet.CefApi;
using CefDotnet.CefApi.Types;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CefDotnet.CefWrap;

unsafe public class CefApp : CefBaseRefCounted<cef_app_t>
{
    public CefApp()
    {
        CefBrowserProcessHandler = new();
        //CefRenderProcessHandler = new();

        _on_before_command_line_processing = (cef_app_t* self, cef_string_t* process_type, cef_command_line_t* command_line) =>
        {
            Debug.WriteLine($"CefApp|on_before_command_line_processing, {CefString.FromCefString(process_type)}, {CefCommandLine.ToString(command_line)}");
            OnBeforeCommandLineProcessing(CefString.FromCefString(process_type), command_line);
            Ref.Release(command_line);
        };
        _on_register_custom_schemes = (cef_app_t* self, cef_scheme_registrar_t* registrar) =>
        {
            Debug.WriteLine("CefApp|on_register_custom_schemes");
        };
        _get_resource_bundle_handler = (cef_app_t* self) =>
        {
            //Debug.WriteLine("CefApp|get_resource_bundle_handler");
            return null;
        };
        _get_browser_process_handler = (cef_app_t* self) =>
        {
            //Debug.WriteLine("CefApp|get_browser_process_handler");
            return CefBrowserProcessHandler.GetRef();
        };
        _get_render_process_handler = (cef_app_t* self) =>
        {
            Debug.WriteLine("CefApp|get_render_process_handler");
            //return CefRenderProcessHandler.Pointer;
            return null;
        };

        cef_app_t* p = Ptr;
        p->on_before_command_line_processing = (delegate* unmanaged<cef_app_t*, cef_string_t*, cef_command_line_t*, void>)Marshal.GetFunctionPointerForDelegate(_on_before_command_line_processing);
        p->on_register_custom_schemes = (delegate* unmanaged<cef_app_t*, cef_scheme_registrar_t*, void>)Marshal.GetFunctionPointerForDelegate(_on_register_custom_schemes);
        p->get_resource_bundle_handler = (delegate* unmanaged<cef_app_t*, cef_resource_bundle_handler_t*>)Marshal.GetFunctionPointerForDelegate(_get_resource_bundle_handler);
        p->get_browser_process_handler = (delegate* unmanaged<cef_app_t*, cef_browser_process_handler_t*>)Marshal.GetFunctionPointerForDelegate(_get_browser_process_handler);
        p->get_render_process_handler = (delegate* unmanaged<cef_app_t*, cef_render_process_handler_t*>)Marshal.GetFunctionPointerForDelegate(_get_render_process_handler);
    }

    CefBrowserProcessHandler CefBrowserProcessHandler;
    //CefRenderProcessHandler CefRenderProcessHandler;

    public delegate void DelegateOnBeforeCommandLineProcessing(string process_type, cef_command_line_t* command_line);
    public DelegateOnBeforeCommandLineProcessing OnBeforeCommandLineProcessing { get; set; } = (_, _) => { };

    on_before_command_line_processing _on_before_command_line_processing;
    on_register_custom_schemes _on_register_custom_schemes;
    get_resource_bundle_handler _get_resource_bundle_handler;
    get_browser_process_handler _get_browser_process_handler;
    get_render_process_handler _get_render_process_handler;

    protected delegate void on_before_command_line_processing(cef_app_t* self, cef_string_t* process_type, cef_command_line_t* command_line);
    protected delegate void on_register_custom_schemes(cef_app_t* self, cef_scheme_registrar_t* registrar);
    protected delegate cef_resource_bundle_handler_t* get_resource_bundle_handler(cef_app_t* self);
    protected delegate cef_browser_process_handler_t* get_browser_process_handler(cef_app_t* self);
    protected delegate cef_render_process_handler_t* get_render_process_handler(cef_app_t* self);
}
