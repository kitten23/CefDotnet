using CefDotnet.CefApi;
using CefDotnet.CefApi.Types;
using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CefDotnet.CefWrap;

unsafe public class CefCommandHandler : CefBaseRefCounted<cef_command_handler_t>
{
    public CefCommandHandler()
    {
        _on_chrome_command = (cef_command_handler_t* self, cef_browser_t* browser, int command_id, cef_window_open_disposition_t disposition) =>
        {
            var ret = OnChromeCommand?.Invoke(self, browser, command_id, disposition) ?? 0;
            Ref.Release(browser);
            return ret;
        };
        _is_chrome_app_menu_item_visible = (cef_command_handler_t* self, cef_browser_t* browser, int command_id) =>
        {
            var ret = IsChromeAppMenuItemVisible?.Invoke(self, browser, command_id) ?? 1;
            Ref.Release(browser);
            return ret;
        };
        _is_chrome_app_menu_item_enabled = (cef_command_handler_t* self, cef_browser_t* browser, int command_id) =>
        {
            var ret = IsChromeAppMenuItemEnabled?.Invoke(self, browser, command_id) ?? 1;
            Ref.Release(browser);
            return ret;
        };
        _is_chrome_page_action_icon_visible = (cef_command_handler_t* self, cef_chrome_page_action_icon_type_t icon_type) =>
        {
            return IsChromePageActionIconVisible?.Invoke(self, icon_type) ?? 1;
        };
        _is_chrome_toolbar_button_visible = (cef_command_handler_t* self, cef_chrome_toolbar_button_type_t button_type) =>
        {
            return IsChromeToolbarButtonVisible?.Invoke(self, button_type) ?? 1;
        };

        Ptr->on_chrome_command = (delegate* unmanaged<cef_command_handler_t*, cef_browser_t*, int, cef_window_open_disposition_t, int>)Marshal.GetFunctionPointerForDelegate(_on_chrome_command);
        Ptr->is_chrome_app_menu_item_visible = (delegate* unmanaged<cef_command_handler_t*, cef_browser_t*, int, int>)Marshal.GetFunctionPointerForDelegate(_is_chrome_app_menu_item_visible);
        Ptr->is_chrome_app_menu_item_enabled = (delegate* unmanaged<cef_command_handler_t*, cef_browser_t*, int, int>)Marshal.GetFunctionPointerForDelegate(_is_chrome_app_menu_item_enabled);
        Ptr->is_chrome_page_action_icon_visible = (delegate* unmanaged<cef_command_handler_t*, cef_chrome_page_action_icon_type_t, int>)Marshal.GetFunctionPointerForDelegate(_is_chrome_page_action_icon_visible);
        Ptr->is_chrome_toolbar_button_visible = (delegate* unmanaged<cef_command_handler_t*, cef_chrome_toolbar_button_type_t, int>)Marshal.GetFunctionPointerForDelegate(_is_chrome_toolbar_button_visible);
    }

    on_chrome_command _on_chrome_command;
    is_chrome_app_menu_item_visible _is_chrome_app_menu_item_visible;
    is_chrome_app_menu_item_enabled _is_chrome_app_menu_item_enabled;
    is_chrome_page_action_icon_visible _is_chrome_page_action_icon_visible;
    is_chrome_toolbar_button_visible _is_chrome_toolbar_button_visible;

    public on_chrome_command? OnChromeCommand { get; set; }
    public is_chrome_app_menu_item_visible? IsChromeAppMenuItemVisible { get; set; }
    public is_chrome_app_menu_item_enabled? IsChromeAppMenuItemEnabled { get; set; }
    public is_chrome_page_action_icon_visible? IsChromePageActionIconVisible { get; set; }
    public is_chrome_toolbar_button_visible? IsChromeToolbarButtonVisible { get; set; }

    public delegate int on_chrome_command(cef_command_handler_t* self, cef_browser_t* browser, int command_id, cef_window_open_disposition_t disposition);
    public delegate int is_chrome_app_menu_item_visible(cef_command_handler_t* self, cef_browser_t* browser, int command_id);
    public delegate int is_chrome_app_menu_item_enabled(cef_command_handler_t* self, cef_browser_t* browser, int command_id);
    public delegate int is_chrome_page_action_icon_visible(cef_command_handler_t* self, cef_chrome_page_action_icon_type_t icon_type);
    public delegate int is_chrome_toolbar_button_visible(cef_command_handler_t* self, cef_chrome_toolbar_button_type_t button_type);
}
