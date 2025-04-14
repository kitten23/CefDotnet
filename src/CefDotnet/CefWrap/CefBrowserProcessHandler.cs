using CefDotnet.CefApi;
using CefDotnet.CefApi.Types;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CefDotnet.CefWrap;

unsafe public class CefBrowserProcessHandler : CefBaseRefCounted<cef_browser_process_handler_t>
{
    public CefBrowserProcessHandler()
    {
        _on_register_custom_preferences = (cef_browser_process_handler_t* self, cef_preferences_type_t type, cef_preference_registrar_t* registrar) =>
        {
            Debug.WriteLine($"CefBrowserProcessHandler|_on_register_custom_preferences type={type}");
        };

        _on_context_initialized = (cef_browser_process_handler_t* self) =>
        {
            Debug.WriteLine($"CefBrowserProcessHandler|_on_context_initialized");
        };

        _on_before_child_process_launch = (cef_browser_process_handler_t* self, cef_command_line_t* command_line) =>
        {
            Debug.WriteLine($"CefBrowserProcessHandler|_on_before_child_process_launch {CefCommandLine.ToString(command_line)}");
            Ref.Release(command_line);
        };

        _on_already_running_app_relaunch = (cef_browser_process_handler_t* self, cef_command_line_t* command_line, cef_string_t* current_directory) =>
        {
            Debug.WriteLine($"CefBrowserProcessHandler|_on_already_running_app_relaunch {CefCommandLine.ToString(command_line)}");
            Ref.Release(command_line);
            return 0;
        };

        _on_schedule_message_pump_work = (cef_browser_process_handler_t* self, long delay_ms) =>
        {
            Debug.WriteLine($"CefBrowserProcessHandler|_on_schedule_message_pump_work");
        };

        _get_default_client = (cef_browser_process_handler_t* self) =>
        {
            Debug.WriteLine($"CefBrowserProcessHandler|_get_default_client");
            return null;
        };

        _get_default_request_context_handler = (cef_browser_process_handler_t* self) =>
        {
            Debug.WriteLine($"CefBrowserProcessHandler|_get_default_request_context_handler");
            return null;
        };

        cef_browser_process_handler_t* p = Ptr;
        p->on_register_custom_preferences = (delegate* unmanaged<cef_browser_process_handler_t*, cef_preferences_type_t, cef_preference_registrar_t*, void>)Marshal.GetFunctionPointerForDelegate(_on_register_custom_preferences);
        p->on_context_initialized = (delegate* unmanaged<cef_browser_process_handler_t*, void>)Marshal.GetFunctionPointerForDelegate(_on_context_initialized);
        p->on_before_child_process_launch = (delegate* unmanaged<cef_browser_process_handler_t*, cef_command_line_t*, void>)Marshal.GetFunctionPointerForDelegate(_on_before_child_process_launch);
        p->on_already_running_app_relaunch = (delegate* unmanaged<cef_browser_process_handler_t*, cef_command_line_t*, cef_string_t*, int>)Marshal.GetFunctionPointerForDelegate(_on_already_running_app_relaunch);
        p->on_schedule_message_pump_work = (delegate* unmanaged<cef_browser_process_handler_t*, long, void>)Marshal.GetFunctionPointerForDelegate(_on_schedule_message_pump_work);
        p->get_default_client = (delegate* unmanaged<cef_browser_process_handler_t*, cef_client_t*>)Marshal.GetFunctionPointerForDelegate(_get_default_client);
        p->get_default_request_context_handler = (delegate* unmanaged<cef_browser_process_handler_t*, cef_request_context_handler_t*>)Marshal.GetFunctionPointerForDelegate(_get_default_request_context_handler);
    }

    on_register_custom_preferences _on_register_custom_preferences;
    on_context_initialized _on_context_initialized;
    on_before_child_process_launch _on_before_child_process_launch;
    on_already_running_app_relaunch _on_already_running_app_relaunch;
    on_schedule_message_pump_work _on_schedule_message_pump_work;
    get_default_client _get_default_client;
    get_default_request_context_handler _get_default_request_context_handler;
    public delegate void on_register_custom_preferences(cef_browser_process_handler_t* self, cef_preferences_type_t type, cef_preference_registrar_t* registrar);
    public delegate void on_context_initialized(cef_browser_process_handler_t* self);
    public delegate void on_before_child_process_launch(cef_browser_process_handler_t* self, cef_command_line_t* command_line);
    public delegate int on_already_running_app_relaunch(cef_browser_process_handler_t* self, cef_command_line_t* command_line, cef_string_t* current_directory);
    public delegate void on_schedule_message_pump_work(cef_browser_process_handler_t* self, long delay_ms);
    public delegate cef_client_t* get_default_client(cef_browser_process_handler_t* self);
    public delegate cef_request_context_handler_t* get_default_request_context_handler(cef_browser_process_handler_t* self);
}
