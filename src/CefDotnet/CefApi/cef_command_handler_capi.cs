using CefDotnet.CefApi.Types;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// Implement this structure to handle events related to commands. The functions
/// of this structure will be called on the UI thread.
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_command_handler_t
{
    ///
    /// Base structure.
    ///
    public cef_base_ref_counted_t @base;

    ///
    /// Called to execute a Chrome command triggered via menu selection or
    /// keyboard shortcut. Use the cef_id_for_command_id_name() function for
    /// version-safe mapping of command IDC names from cef_command_ids.h to
    /// version-specific numerical |command_id| values. |disposition| provides
    /// information about the intended command target. Return true (1) if the
    /// command was handled or false (0) for the default implementation. For
    /// context menu commands this will be called after
    /// cef_context_menu_handler_t::OnContextMenuCommand. Only used with Chrome
    /// style.
    ///
    public delegate* unmanaged<cef_command_handler_t*, cef_browser_t*, int, cef_window_open_disposition_t, int> on_chrome_command;

    ///
    /// Called to check if a Chrome app menu item should be visible. Use the
    /// cef_id_for_command_id_name() function for version-safe mapping of command
    /// IDC names from cef_command_ids.h to version-specific numerical
    /// |command_id| values. Only called for menu items that would be visible by
    /// default. Only used with Chrome style.
    ///
    public delegate* unmanaged<cef_command_handler_t*, cef_browser_t*, int, int> is_chrome_app_menu_item_visible;
    ///
    /// Called to check if a Chrome app menu item should be enabled. Use the
    /// cef_id_for_command_id_name() function for version-safe mapping of command
    /// IDC names from cef_command_ids.h to version-specific numerical
    /// |command_id| values. Only called for menu items that would be enabled by
    /// default. Only used with Chrome style.
    ///
    public delegate* unmanaged<cef_command_handler_t*, cef_browser_t*, int, int> is_chrome_app_menu_item_enabled;
    ///
    /// Called during browser creation to check if a Chrome page action icon
    /// should be visible. Only called for icons that would be visible by default.
    /// Only used with Chrome style.
    ///
    public delegate* unmanaged<cef_command_handler_t*, cef_chrome_page_action_icon_type_t, int> is_chrome_page_action_icon_visible;
    ///
    /// Called during browser creation to check if a Chrome toolbar button should
    /// be visible. Only called for buttons that would be visible by default. Only
    /// used with Chrome style.
    ///
    public delegate* unmanaged<cef_command_handler_t*, cef_chrome_toolbar_button_type_t, int> is_chrome_toolbar_button_visible;
}

///
/// Chrome toolbar button types. Should be kept in sync with CEF's internal
/// ToolbarButtonType type.
///
public enum cef_chrome_toolbar_button_type_t
{
    CEF_CTBT_CAST,
    CEF_CTBT_DOWNLOAD,
    CEF_CTBT_SEND_TAB_TO_SELF,
    CEF_CTBT_SIDE_PANEL,
    CEF_CTBT_NUM_VALUES,
}

///
/// Chrome page action icon types. Should be kept in sync with Chromium's
/// PageActionIconType type.
///
public enum cef_chrome_page_action_icon_type_t
{
    CEF_CPAIT_BOOKMARK_STAR,
    CEF_CPAIT_CLICK_TO_CALL,
    CEF_CPAIT_COOKIE_CONTROLS,
    CEF_CPAIT_FILE_SYSTEM_ACCESS,
    CEF_CPAIT_FIND,
    CEF_CPAIT_MEMORY_SAVER,
    CEF_CPAIT_INTENT_PICKER,
    CEF_CPAIT_LOCAL_CARD_MIGRATION,
    CEF_CPAIT_MANAGE_PASSWORDS,
    CEF_CPAIT_PAYMENTS_OFFER_NOTIFICATION,
    CEF_CPAIT_PRICE_TRACKING,
    CEF_CPAIT_PWA_INSTALL,
    CEF_CPAIT_QR_CODE_GENERATOR_DEPRECATED,
    CEF_CPAIT_READER_MODE_DEPRECATED,
    CEF_CPAIT_SAVE_AUTOFILL_ADDRESS,
    CEF_CPAIT_SAVE_CARD,
    CEF_CPAIT_SEND_TAB_TO_SELF_DEPRECATED,
    CEF_CPAIT_SHARING_HUB,
    CEF_CPAIT_SIDE_SEARCH_DEPRECATED,
    CEF_CPAIT_SMS_REMOTE_FETCHER,
    CEF_CPAIT_TRANSLATE,
    CEF_CPAIT_VIRTUAL_CARD_ENROLL,
    CEF_CPAIT_VIRTUAL_CARD_INFORMATION,
    CEF_CPAIT_ZOOM,
    CEF_CPAIT_SAVE_IBAN,
    CEF_CPAIT_MANDATORY_REAUTH,
    CEF_CPAIT_PRICE_INSIGHTS,
    CEF_CPAIT_READ_ANYTHING_DEPRECATED,
    CEF_CPAIT_PRODUCT_SPECIFICATIONS,
    CEF_CPAIT_LENS_OVERLAY,
    CEF_CPAIT_DISCOUNTS,
    CEF_CPAIT_OPTIMIZATION_GUIDE,
    CEF_CPAIT_COLLABORATION_MESSAGING,
    CEF_CPAIT_NUM_VALUES,
}
