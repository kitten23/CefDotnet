using CefDotnet.CefApi.Internal;
using CefDotnet.CefApi.Types;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// DOM document types.
///
public enum cef_dom_document_type_t : int
{
    DOM_DOCUMENT_TYPE_UNKNOWN,
    DOM_DOCUMENT_TYPE_HTML,
    DOM_DOCUMENT_TYPE_XHTML,
    DOM_DOCUMENT_TYPE_PLUGIN,
    DOM_DOCUMENT_TYPE_NUM_VALUES,
}

///
/// DOM event category flags.
///
public enum cef_dom_event_category_t : uint
{
    DOM_EVENT_CATEGORY_UNKNOWN = 0x0,
    DOM_EVENT_CATEGORY_UI = 0x1,
    DOM_EVENT_CATEGORY_MOUSE = 0x2,
    DOM_EVENT_CATEGORY_MUTATION = 0x4,
    DOM_EVENT_CATEGORY_KEYBOARD = 0x8,
    DOM_EVENT_CATEGORY_TEXT = 0x10,
    DOM_EVENT_CATEGORY_COMPOSITION = 0x20,
    DOM_EVENT_CATEGORY_DRAG = 0x40,
    DOM_EVENT_CATEGORY_CLIPBOARD = 0x80,
    DOM_EVENT_CATEGORY_MESSAGE = 0x100,
    DOM_EVENT_CATEGORY_WHEEL = 0x200,
    DOM_EVENT_CATEGORY_BEFORE_TEXT_INSERTED = 0x400,
    DOM_EVENT_CATEGORY_OVERFLOW = 0x800,
    DOM_EVENT_CATEGORY_PAGE_TRANSITION = 0x1000,
    DOM_EVENT_CATEGORY_POPSTATE = 0x2000,
    DOM_EVENT_CATEGORY_PROGRESS = 0x4000,
    DOM_EVENT_CATEGORY_XMLHTTPREQUEST_PROGRESS = 0x8000,
}

///
/// DOM event processing phases.
///
public enum cef_dom_event_phase_t : int
{
    DOM_EVENT_PHASE_UNKNOWN,
    DOM_EVENT_PHASE_CAPTURING,
    DOM_EVENT_PHASE_AT_TARGET,
    DOM_EVENT_PHASE_BUBBLING,
    DOM_EVENT_PHASE_NUM_VALUES,
}

///
/// DOM node types.
///
public enum cef_dom_node_type_t : int
{
    DOM_NODE_TYPE_UNSUPPORTED,
    DOM_NODE_TYPE_ELEMENT,
    DOM_NODE_TYPE_ATTRIBUTE,
    DOM_NODE_TYPE_TEXT,
    DOM_NODE_TYPE_CDATA_SECTION,
    DOM_NODE_TYPE_PROCESSING_INSTRUCTIONS,
    DOM_NODE_TYPE_COMMENT,
    DOM_NODE_TYPE_DOCUMENT,
    DOM_NODE_TYPE_DOCUMENT_TYPE,
    DOM_NODE_TYPE_DOCUMENT_FRAGMENT,
    DOM_NODE_TYPE_NUM_VALUES,
}

///
/// DOM form control types. Should be kept in sync with Chromium's
/// blink::mojom::FormControlType type.
///
public enum cef_dom_form_control_type_t : int
{
    DOM_FORM_CONTROL_TYPE_UNSUPPORTED,
    DOM_FORM_CONTROL_TYPE_BUTTON_BUTTON,
    DOM_FORM_CONTROL_TYPE_BUTTON_SUBMIT,
    DOM_FORM_CONTROL_TYPE_BUTTON_RESET,
    DOM_FORM_CONTROL_TYPE_BUTTON_POPOVER,
    DOM_FORM_CONTROL_TYPE_FIELDSET,
    DOM_FORM_CONTROL_TYPE_INPUT_BUTTON,
    DOM_FORM_CONTROL_TYPE_INPUT_CHECKBOX,
    DOM_FORM_CONTROL_TYPE_INPUT_COLOR,
    DOM_FORM_CONTROL_TYPE_INPUT_DATE,
    DOM_FORM_CONTROL_TYPE_INPUT_DATETIME_LOCAL,
    DOM_FORM_CONTROL_TYPE_INPUT_EMAIL,
    DOM_FORM_CONTROL_TYPE_INPUT_FILE,
    DOM_FORM_CONTROL_TYPE_INPUT_HIDDEN,
    DOM_FORM_CONTROL_TYPE_INPUT_IMAGE,
    DOM_FORM_CONTROL_TYPE_INPUT_MONTH,
    DOM_FORM_CONTROL_TYPE_INPUT_NUMBER,
    DOM_FORM_CONTROL_TYPE_INPUT_PASSWORD,
    DOM_FORM_CONTROL_TYPE_INPUT_RADIO,
    DOM_FORM_CONTROL_TYPE_INPUT_RANGE,
    DOM_FORM_CONTROL_TYPE_INPUT_RESET,
    DOM_FORM_CONTROL_TYPE_INPUT_SEARCH,
    DOM_FORM_CONTROL_TYPE_INPUT_SUBMIT,
    DOM_FORM_CONTROL_TYPE_INPUT_TELEPHONE,
    DOM_FORM_CONTROL_TYPE_INPUT_TEXT,
    DOM_FORM_CONTROL_TYPE_INPUT_TIME,
    DOM_FORM_CONTROL_TYPE_INPUT_URL,
    DOM_FORM_CONTROL_TYPE_INPUT_WEEK,
    DOM_FORM_CONTROL_TYPE_OUTPUT,
    DOM_FORM_CONTROL_TYPE_SELECT_ONE,
    DOM_FORM_CONTROL_TYPE_SELECT_MULTIPLE,
    DOM_FORM_CONTROL_TYPE_TEXT_AREA,
    DOM_FORM_CONTROL_TYPE_NUM_VALUES,
}

///
/// Structure to implement for visiting the DOM. The functions of this structure
/// will be called on the render process main thread.
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_domvisitor_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Method executed for visiting the DOM. The document object passed to this
    /// function represents a snapshot of the DOM at the time this function is
    /// executed. DOM objects are only valid for the scope of this function. Do
    /// not keep references to or attempt to access any DOM objects outside the
    /// scope of this function.
    ///
    public delegate* unmanaged<cef_domvisitor_t*, cef_domdocument_t*, void> visit;
}

///
/// Structure used to represent a DOM document. The functions of this structure
/// should only be called on the render process main thread thread.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_domdocument_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Returns the document type.
    ///
    public delegate* unmanaged<cef_domdocument_t*, cef_dom_document_type_t> get_type;

    ///
    /// Returns the root document node.
    ///
    public delegate* unmanaged<cef_domdocument_t*, cef_domnode_t*> get_document;

    ///
    /// Returns the BODY node of an HTML document.
    ///
    public delegate* unmanaged<cef_domdocument_t*, cef_domnode_t*> get_body;

    ///
    /// Returns the HEAD node of an HTML document.
    ///
    public delegate* unmanaged<cef_domdocument_t*, cef_domnode_t*> get_head;

    ///
    /// Returns the title of an HTML document.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_domdocument_t*, cef_string_t*> get_title;

    ///
    /// Returns the document element with the specified ID value.
    ///
    public delegate* unmanaged<cef_domdocument_t*, cef_string_t*, cef_domnode_t*> get_element_by_id;

    ///
    /// Returns the node that currently has keyboard focus.
    ///
    public delegate* unmanaged<cef_domdocument_t*, cef_domnode_t*> get_focused_node;

    ///
    /// Returns true (1) if a portion of the document is selected.
    ///
    public delegate* unmanaged<cef_domdocument_t*, int> has_selection;

    ///
    /// Returns the selection offset within the start node.
    ///
    public delegate* unmanaged<cef_domdocument_t*, int> get_selection_start_offset;

    ///
    /// Returns the selection offset within the end node.
    ///
    public delegate* unmanaged<cef_domdocument_t*, int> get_selection_end_offset;

    ///
    /// Returns the contents of this selection as markup.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_domdocument_t*, cef_string_t*> get_selection_as_markup;

    ///
    /// Returns the contents of this selection as text.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_domdocument_t*, cef_string_t*> get_selection_as_text;

    ///
    /// Returns the base URL for the document.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_domdocument_t*, cef_string_t*> get_base_url;

    ///
    /// Returns a complete URL based on the document base URL and the specified
    /// partial URL.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_domdocument_t*, cef_string_t*, cef_string_t*> get_complete_url;
}

///
/// Structure used to represent a DOM node. The functions of this structure
/// should only be called on the render process main thread.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_domnode_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Returns the type for this node.
    ///
    public delegate* unmanaged<cef_domnode_t*, cef_dom_node_type_t> get_type;

    ///
    /// Returns true (1) if this is a text node.
    ///
    public delegate* unmanaged<cef_domnode_t*, int> is_text;

    ///
    /// Returns true (1) if this is an element node.
    ///
    public delegate* unmanaged<cef_domnode_t*, int> is_element;

    ///
    /// Returns true (1) if this is an editable node.
    ///
    public delegate* unmanaged<cef_domnode_t*, int> is_editable;

    ///
    /// Returns true (1) if this is a form control element node.
    ///
    public delegate* unmanaged<cef_domnode_t*, int> is_form_control_element;

    ///
    /// Returns the type of this form control element node.
    ///
    public delegate* unmanaged<cef_domnode_t*, cef_dom_form_control_type_t> get_form_control_element_type;

    ///
    /// Returns true (1) if this object is pointing to the same handle as |that|
    /// object.
    ///
    public delegate* unmanaged<cef_domnode_t*, cef_domnode_t*, int> is_same;

    ///
    /// Returns the name of this node.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_domnode_t*, cef_string_t*> get_name;

    ///
    /// Returns the value of this node.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_domnode_t*, cef_string_t*> get_value;

    ///
    /// Set the value of this node. Returns true (1) on success.
    ///
    public delegate* unmanaged<cef_domnode_t*, cef_string_t*, int> set_value;

    ///
    /// Returns the contents of this node as markup.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_domnode_t*, cef_string_t*> get_as_markup;

    ///
    /// Returns the document associated with this node.
    ///
    public delegate* unmanaged<cef_domnode_t*, cef_domdocument_t*> get_document;

    ///
    /// Returns the parent node.
    ///
    public delegate* unmanaged<cef_domnode_t*, cef_domnode_t*> get_parent;

    ///
    /// Returns the previous sibling node.
    ///
    public delegate* unmanaged<cef_domnode_t*, cef_domnode_t*> get_previous_sibling;

    ///
    /// Returns the next sibling node.
    ///
    public delegate* unmanaged<cef_domnode_t*, cef_domnode_t*> get_next_sibling;

    ///
    /// Returns true (1) if this node has child nodes.
    ///
    public delegate* unmanaged<cef_domnode_t*, int> has_children;

    ///
    /// Return the first child node.
    ///
    public delegate* unmanaged<cef_domnode_t*, cef_domnode_t*> get_first_child;

    ///
    /// Returns the last child node.
    ///
    public delegate* unmanaged<cef_domnode_t*, cef_domnode_t*> get_last_child;

    ///
    /// Returns the tag name of this element.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_domnode_t*, cef_string_t*> get_element_tag_name;

    ///
    /// Returns true (1) if this element has attributes.
    ///
    public delegate* unmanaged<cef_domnode_t*, int> has_element_attributes;

    ///
    /// Returns true (1) if this element has an attribute named |attrName|.
    ///
    public delegate* unmanaged<cef_domnode_t*, cef_string_t*, int> has_element_attribute;

    ///
    /// Returns the element attribute named |attrName|.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_domnode_t*, cef_string_t*, cef_string_t*> get_element_attribute;

    ///
    /// Returns a map of all element attributes.
    ///
    public delegate* unmanaged<cef_domnode_t*, nint, void> get_element_attributes;

    ///
    /// Set the value for the element attribute named |attrName|. Returns true (1)
    /// on success.
    ///
    public delegate* unmanaged<cef_domnode_t*, cef_string_t*, cef_string_t*, int> set_element_attribute;

    ///
    /// Returns the inner text of the element.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_domnode_t*, cef_string_t*> get_element_inner_text;

    ///
    /// Returns the bounds of the element in device pixels. Use
    /// "window.devicePixelRatio" to convert to/from CSS pixels.
    ///
    public delegate* unmanaged<cef_domnode_t*, cef_rect_t> get_element_bounds;
}
