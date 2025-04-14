using CefDotnet.CefApi.Types;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi.Values;

///
/// Structure representing a list value. Can be used on any process and thread.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_list_value_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Returns true (1) if this object is valid. This object may become invalid
    /// if the underlying data is owned by another object (e.g. list or
    /// dictionary) and that other object is then modified or destroyed. Do not
    /// call any other functions if this function returns false (0).
    ///
    public delegate* unmanaged<cef_list_value_t*, int> is_valid;

    ///
    /// Returns true (1) if this object is currently owned by another object.
    ///
    public delegate* unmanaged<cef_list_value_t*, int> is_owned;

    ///
    /// Returns true (1) if the values of this object are read-only. Some APIs may
    /// expose read-only objects.
    ///
    public delegate* unmanaged<cef_list_value_t*, int> is_read_only;

    ///
    /// Returns true (1) if this object and |that| object have the same underlying
    /// data. If true (1) modifications to this object will also affect |that|
    /// object and vice-versa.
    ///
    public delegate* unmanaged<cef_list_value_t*, cef_list_value_t*, int> is_same;

    ///
    /// Returns true (1) if this object and |that| object have an equivalent
    /// underlying value but are not necessarily the same object.
    ///
    public delegate* unmanaged<cef_list_value_t*, cef_list_value_t*, int> is_equal;

    ///
    /// Returns a writable copy of this object.
    ///
    public delegate* unmanaged<cef_list_value_t*, cef_list_value_t*> copy;

    ///
    /// Sets the number of values. If the number of values is expanded all new
    /// value slots will default to type null. Returns true (1) on success.
    ///
    public delegate* unmanaged<cef_list_value_t*, nuint, int> set_size;

    ///
    /// Returns the number of values.
    ///
    public delegate* unmanaged<cef_list_value_t*, nuint> get_size;

    ///
    /// Removes all values. Returns true (1) on success.
    ///
    public delegate* unmanaged<cef_list_value_t*, int> clear;

    ///
    /// Removes the value at the specified index.
    ///
    public delegate* unmanaged<cef_list_value_t*, nuint, int> remove;

    ///
    /// Returns the value type at the specified index.
    ///
    public delegate* unmanaged<cef_list_value_t*, nuint, cef_value_type_t> get_type;

    ///
    /// Returns the value at the specified index. For simple types the returned
    /// value will copy existing data and modifications to the value will not
    /// modify this object. For complex types (binary, dictionary and list) the
    /// returned value will reference existing data and modifications to the value
    /// will modify this object.
    ///
    public delegate* unmanaged<cef_list_value_t*, nuint, cef_value_t*> get_value;

    ///
    /// Returns the value at the specified index as type bool.
    ///
    public delegate* unmanaged<cef_list_value_t*, nuint, int> get_bool;

    ///
    /// Returns the value at the specified index as type int.
    ///
    public delegate* unmanaged<cef_list_value_t*, nuint, int> get_int;

    ///
    /// Returns the value at the specified index as type double.
    ///
    public delegate* unmanaged<cef_list_value_t*, nuint, double> get_double;

    ///
    /// Returns the value at the specified index as type string.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_list_value_t*, nuint, cef_string_t*> get_string;

    ///
    /// Returns the value at the specified index as type binary. The returned
    /// value will reference existing data.
    ///
    public delegate* unmanaged<cef_list_value_t*, nuint, cef_binary_value_t*> get_binary;

    ///
    /// Returns the value at the specified index as type dictionary. The returned
    /// value will reference existing data and modifications to the value will
    /// modify this object.
    ///
    public delegate* unmanaged<cef_list_value_t*, nuint, cef_dictionary_value_t*> get_dictionary;

    ///
    /// Returns the value at the specified index as type list. The returned value
    /// will reference existing data and modifications to the value will modify
    /// this object.
    ///
    public delegate* unmanaged<cef_list_value_t*, nuint, cef_list_value_t*> get_list;

    ///
    /// Sets the value at the specified index. Returns true (1) if the value was
    /// set successfully. If |value| represents simple data then the underlying
    /// data will be copied and modifications to |value| will not modify this
    /// object. If |value| represents complex data (binary, dictionary or list)
    /// then the underlying data will be referenced and modifications to |value|
    /// will modify this object.
    ///
    public delegate* unmanaged<cef_list_value_t*, nuint, cef_value_t*, int> set_value;

    ///
    /// Sets the value at the specified index as type null. Returns true (1) if
    /// the value was set successfully.
    ///
    public delegate* unmanaged<cef_list_value_t*, nuint, int> set_null;

    ///
    /// Sets the value at the specified index as type bool. Returns true (1) if
    /// the value was set successfully.
    ///
    public delegate* unmanaged<cef_list_value_t*, nuint, int, int> set_bool;

    ///
    /// Sets the value at the specified index as type int. Returns true (1) if the
    /// value was set successfully.
    ///
    public delegate* unmanaged<cef_list_value_t*, nuint, int, int> set_int;

    ///
    /// Sets the value at the specified index as type double. Returns true (1) if
    /// the value was set successfully.
    ///
    public delegate* unmanaged<cef_list_value_t*, nuint, double, int> set_double;

    ///
    /// Sets the value at the specified index as type string. Returns true (1) if
    /// the value was set successfully.
    ///
    public delegate* unmanaged<cef_list_value_t*, nuint, cef_string_t*, int> set_string;

    ///
    /// Sets the value at the specified index as type binary. Returns true (1) if
    /// the value was set successfully. If |value| is currently owned by another
    /// object then the value will be copied and the |value| reference will not
    /// change. Otherwise, ownership will be transferred to this object and the
    /// |value| reference will be invalidated.
    ///
    public delegate* unmanaged<cef_list_value_t*, nuint, cef_binary_value_t*, int> set_binary;

    ///
    /// Sets the value at the specified index as type dict. Returns true (1) if
    /// the value was set successfully. If |value| is currently owned by another
    /// object then the value will be copied and the |value| reference will not
    /// change. Otherwise, ownership will be transferred to this object and the
    /// |value| reference will be invalidated.
    ///
    public delegate* unmanaged<cef_list_value_t*, nuint, cef_dictionary_value_t*, int> set_dictionary;

  ///
  /// Sets the value at the specified index as type list. Returns true (1) if
  /// the value was set successfully. If |value| is currently owned by another
  /// object then the value will be copied and the |value| reference will not
  /// change. Otherwise, ownership will be transferred to this object and the
  /// |value| reference will be invalidated.
  ///
  public delegate* unmanaged<cef_list_value_t*, nuint, cef_list_value_t*, int> set_list;
}