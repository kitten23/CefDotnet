using CefDotnet.CefApi;
using CefDotnet.CefApi.Types;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CefDotnet.CefWrap;

unsafe public class CefClient : CefBaseRefCounted<cef_client_t>
{
    public CefClient()
    {
        _get_audio_handler = (cef_client_t* client) =>
        {
            Debug.WriteLine("CefClient|get_audio_handler");
            return CefAudioHandler.GetRef();
        };
        _get_command_handler = (cef_client_t* client) =>
        {
            //Debug.WriteLine("CefClient|get_command_handler");
            return CefCommandHandler.GetRef();
        };
        get_context_menu_handler = (cef_client_t* client) =>
        {
            Debug.WriteLine("CefClient|get_context_menu_handler");
            return nint.Zero;
        };
        get_dialog_handler = (cef_client_t* client) =>
        {
            Debug.WriteLine("CefClient|get_dialog_handler");
            return nint.Zero;
        };
        _get_display_handler = (cef_client_t* client) =>
        {
            //Debug.WriteLine("CefClient|get_display_handler");
            return CefDisplayHandler.GetRef();
        };
        _get_download_handler = (cef_client_t* client) =>
        {
            //Debug.WriteLine("CefClient|get_download_handler");
            return CefDownloadHandler.GetRef();
        };
        get_drag_handler = (cef_client_t* client) =>
        {
            Debug.WriteLine("CefClient|get_drag_handler");
            return nint.Zero;
        };
        get_find_handler = (cef_client_t* client) =>
        {
            Debug.WriteLine("CefClient|get_find_handler");
            return nint.Zero;
        };
        get_focus_handler = (cef_client_t* client) =>
        {
            //Debug.WriteLine("CefClient|get_focus_handler");
            return nint.Zero;
        };
        get_frame_handler = (cef_client_t* client) =>
        {
            Debug.WriteLine("CefClient|get_frame_handler");
            return nint.Zero;
        };
        get_permission_handler = (cef_client_t* client) =>
        {
            Debug.WriteLine("CefClient|get_permission_handler");
            return nint.Zero;
        };
        get_jsdialog_handler = (cef_client_t* client) =>
        {
            Debug.WriteLine("CefClient|get_jsdialog_handler");
            return nint.Zero;
        };
        get_keyboard_handler = (cef_client_t* client) =>
        {
            //Debug.WriteLine("CefClient|get_keyboard_handler");
            return nint.Zero;
        };
        _get_life_span_handler = (cef_client_t* client) =>
        {
            //Debug.WriteLine("CefClient|_get_life_span_handler");
            return CefLifeSpanHandler.GetRef();
        };
        _get_load_handler = (cef_client_t* client) =>
        {
            //Debug.WriteLine("CefClient|get_load_handler");
            return CefLoadHandler.GetRef();
        };
        get_print_handler = (cef_client_t* client) =>
        {
            Debug.WriteLine("CefClient|get_print_handler");
            return nint.Zero;
        };
        get_render_handler = (cef_client_t* client) =>
        {
            Debug.WriteLine("CefClient|get_render_handler");
            return nint.Zero;
        };
        _get_request_handler = (cef_client_t* client) =>
        {
            //Debug.WriteLine("CefClient|get_request_handler");
            return CefRequestHandler.GetRef();
        };
        _on_process_message_received = (cef_client_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_process_id_t source_process, cef_process_message_t* message) =>
        {
            Debug.WriteLine("CefClient|on_process_message_received");
            Ref.Release(browser);
            Ref.Release(frame);
            Ref.Release(message);
            return 0;
        };

        cef_client_t* p = Ptr;
        p->get_audio_handler = (delegate* unmanaged<cef_client_t*, cef_audio_handler_t*>)Marshal.GetFunctionPointerForDelegate(_get_audio_handler);
        p->get_command_handler = (delegate* unmanaged<cef_client_t*, cef_command_handler_t*>)Marshal.GetFunctionPointerForDelegate(_get_command_handler);
        p->get_context_menu_handler = (delegate* unmanaged<cef_client_t*, cef_context_menu_handler_t*>)Marshal.GetFunctionPointerForDelegate(get_context_menu_handler);
        p->get_dialog_handler = (delegate* unmanaged<cef_client_t*, cef_dialog_handler_t*>)Marshal.GetFunctionPointerForDelegate(get_dialog_handler);
        p->get_display_handler = (delegate* unmanaged<cef_client_t*, cef_display_handler_t*>)Marshal.GetFunctionPointerForDelegate(_get_display_handler);
        p->get_download_handler = (delegate* unmanaged<cef_client_t*, cef_download_handler_t*>)Marshal.GetFunctionPointerForDelegate(_get_download_handler);
        p->get_drag_handler = (delegate* unmanaged<cef_client_t*, cef_drag_handler_t*>)Marshal.GetFunctionPointerForDelegate(get_drag_handler);
        p->get_find_handler = (delegate* unmanaged<cef_client_t*, cef_find_handler_t*>)Marshal.GetFunctionPointerForDelegate(get_find_handler);
        p->get_focus_handler = (delegate* unmanaged<cef_client_t*, cef_focus_handler_t*>)Marshal.GetFunctionPointerForDelegate(get_focus_handler);
        p->get_frame_handler = (delegate* unmanaged<cef_client_t*, cef_frame_handler_t*>)Marshal.GetFunctionPointerForDelegate(get_frame_handler);
        p->get_permission_handler = (delegate* unmanaged<cef_client_t*, cef_permission_handler_t*>)Marshal.GetFunctionPointerForDelegate(get_permission_handler);
        p->get_jsdialog_handler = (delegate* unmanaged<cef_client_t*, cef_jsdialog_handler_t*>)Marshal.GetFunctionPointerForDelegate(get_jsdialog_handler);
        p->get_keyboard_handler = (delegate* unmanaged<cef_client_t*, cef_keyboard_handler_t*>)Marshal.GetFunctionPointerForDelegate(get_keyboard_handler);
        p->get_life_span_handler = (delegate* unmanaged<cef_client_t*, cef_life_span_handler_t*>)Marshal.GetFunctionPointerForDelegate(_get_life_span_handler);
        p->get_load_handler = (delegate* unmanaged<cef_client_t*, cef_load_handler_t*>)Marshal.GetFunctionPointerForDelegate(_get_load_handler);
        p->get_print_handler = (delegate* unmanaged<cef_client_t*, cef_print_handler_t*>)Marshal.GetFunctionPointerForDelegate(get_print_handler);
        p->get_render_handler = (delegate* unmanaged<cef_client_t*, cef_render_handler_t*>)Marshal.GetFunctionPointerForDelegate(get_render_handler);
        p->get_request_handler = (delegate* unmanaged<cef_client_t*, cef_request_handler_t*>)Marshal.GetFunctionPointerForDelegate(_get_request_handler);
        p->on_process_message_received = (delegate* unmanaged<cef_client_t*, cef_client_t*, cef_browser_t*, cef_frame_t*, cef_process_id_t, cef_preference_manager_t*, int>)Marshal.GetFunctionPointerForDelegate(_on_process_message_received);
    }

    public CefAudioHandler CefAudioHandler { get; private set; } = new CefAudioHandler();
    public CefCommandHandler CefCommandHandler { get; private set; } = new CefCommandHandler();
    public CefDisplayHandler CefDisplayHandler { get; private set; } = new CefDisplayHandler();
    public CefLifeSpanHandler CefLifeSpanHandler { get; private set; } = new CefLifeSpanHandler();
    public CefLoadHandler CefLoadHandler { get; private set; } = new CefLoadHandler();
    public CefRequestHandler CefRequestHandler { get; private set; } = new CefRequestHandler();
    public CefDownloadHandler CefDownloadHandler { get; private set; } = new CefDownloadHandler();

    get_audio_handler _get_audio_handler;
    get_command_handler _get_command_handler;
    GetHandler get_context_menu_handler;
    GetHandler get_dialog_handler;
    get_display_handler _get_display_handler;
    get_download_handler _get_download_handler;
    GetHandler get_drag_handler;
    GetHandler get_find_handler;
    GetHandler get_focus_handler;
    GetHandler get_frame_handler;
    GetHandler get_permission_handler;
    GetHandler get_jsdialog_handler;
    GetHandler get_keyboard_handler;
    get_life_span_handler _get_life_span_handler;
    get_load_handler _get_load_handler;
    GetHandler get_print_handler;
    GetHandler get_render_handler;
    get_request_handler _get_request_handler;
    on_process_message_received _on_process_message_received;

    public delegate nint GetHandler(cef_client_t* self);
    public delegate cef_audio_handler_t* get_audio_handler(cef_client_t* self);
    public delegate cef_command_handler_t* get_command_handler(cef_client_t* self);
    public delegate cef_display_handler_t* get_display_handler(cef_client_t* self);
    public delegate cef_download_handler_t* get_download_handler(cef_client_t* self);
    public delegate cef_life_span_handler_t* get_life_span_handler(cef_client_t* self);
    public delegate cef_load_handler_t* get_load_handler(cef_client_t* self);
    public delegate cef_request_handler_t* get_request_handler(cef_client_t* self);
    public delegate int on_process_message_received(cef_client_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_process_id_t source_process, cef_process_message_t* message);
}
