using CefDotnet.CefApi.Types;
using CefDotnet.CefApi.Values;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// Structure that wraps other data value types. Complex types (binary,
/// dictionary and list) will be referenced but not owned by this object. Can be
/// used on any process and thread.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_value_t
{
    ///
    /// Base structure.
    ///
    public cef_base_ref_counted_t @base;

    ///
    /// Returns true (1) if the underlying data is valid. This will always be true
    /// (1) for simple types. For complex types (binary, dictionary and list) the
    /// underlying data may become invalid if owned by another object (e.g. list
    /// or dictionary) and that other object is then modified or destroyed. This
    /// value object can be re-used by calling Set*() even if the underlying data
    /// is invalid.
    ///
    public delegate* unmanaged<cef_value_t*, int> is_valid;

    ///
    /// Returns true (1) if the underlying data is owned by another object.
    ///
    public delegate* unmanaged<cef_value_t*, int> is_owned;

    ///
    /// Returns true (1) if the underlying data is read-only. Some APIs may expose
    /// read-only objects.
    ///
    public delegate* unmanaged<cef_value_t*, int> is_read_only;

    ///
    /// Returns true (1) if this object and |that| object have the same underlying
    /// data. If true (1) modifications to this object will also affect |that|
    /// object and vice-versa.
    ///
    public delegate* unmanaged<cef_value_t*, cef_value_t*, int> is_same;

    ///
    /// Returns true (1) if this object and |that| object have an equivalent
    /// underlying value but are not necessarily the same object.
    ///
    public delegate* unmanaged<cef_value_t*, cef_value_t*, int> is_equal;

    ///
    /// Returns a copy of this object. The underlying data will also be copied.
    ///
    public delegate* unmanaged<cef_value_t*, cef_value_t*> copy;

    ///
    /// Returns the underlying value type.
    ///
    public delegate* unmanaged<cef_value_t*, cef_value_type_t> get_type;

    ///
    /// Returns the underlying value as type bool.
    ///
    public delegate* unmanaged<cef_value_t*, int> get_bool;

    ///
    /// Returns the underlying value as type int.
    ///
    public delegate* unmanaged<cef_value_t*, int> get_int;

    ///
    /// Returns the underlying value as type double.
    ///
    public delegate* unmanaged<cef_value_t*, double> get_double;

    ///
    /// Returns the underlying value as type string.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_value_t*, cef_string_t*> get_string;

    ///
    /// Returns the underlying value as type binary. The returned reference may
    /// become invalid if the value is owned by another object or if ownership is
    /// transferred to another object in the future. To maintain a reference to
    /// the value after assigning ownership to a dictionary or list pass this
    /// object to the set_value() function instead of passing the returned
    /// reference to set_binary().
    ///
    public delegate* unmanaged<cef_value_t*, cef_binary_value_t*> get_binary;

    ///
    /// Returns the underlying value as type dictionary. The returned reference
    /// may become invalid if the value is owned by another object or if ownership
    /// is transferred to another object in the future. To maintain a reference to
    /// the value after assigning ownership to a dictionary or list pass this
    /// object to the set_value() function instead of passing the returned
    /// reference to set_dictionary().
    ///
    public delegate* unmanaged<cef_value_t*, cef_dictionary_value_t*> get_dictionary;

    ///
    /// Returns the underlying value as type list. The returned reference may
    /// become invalid if the value is owned by another object or if ownership is
    /// transferred to another object in the future. To maintain a reference to
    /// the value after assigning ownership to a dictionary or list pass this
    /// object to the set_value() function instead of passing the returned
    /// reference to set_list().
    ///
    public delegate* unmanaged<cef_value_t*, cef_list_value_t*> get_list;

    ///
    /// Sets the underlying value as type null. Returns true (1) if the value was
    /// set successfully.
    ///
    public delegate* unmanaged<cef_value_t*, int> set_null;

    ///
    /// Sets the underlying value as type bool. Returns true (1) if the value was
    /// set successfully.
    ///
    public delegate* unmanaged<cef_value_t*, int, int> set_bool;

    ///
    /// Sets the underlying value as type int. Returns true (1) if the value was
    /// set successfully.
    ///
    public delegate* unmanaged<cef_value_t*, int, int> set_int;

    ///
    /// Sets the underlying value as type double. Returns true (1) if the value
    /// was set successfully.
    ///
    public delegate* unmanaged<cef_value_t*, double, int> set_double;

    ///
    /// Sets the underlying value as type string. Returns true (1) if the value
    /// was set successfully.
    ///
    public delegate* unmanaged<cef_value_t*, cef_string_t*, int> set_string;

    ///
    /// Sets the underlying value as type binary. Returns true (1) if the value
    /// was set successfully. This object keeps a reference to |value| and
    /// ownership of the underlying data remains unchanged.
    ///
    public delegate* unmanaged<cef_value_t*, cef_binary_value_t*, int> set_binary;

    ///
    /// Sets the underlying value as type dict. Returns true (1) if the value was
    /// set successfully. This object keeps a reference to |value| and ownership
    /// of the underlying data remains unchanged.
    ///
    public delegate* unmanaged<cef_value_t*, cef_dictionary_value_t*, int> set_dictionary;

    ///
    /// Sets the underlying value as type list. Returns true (1) if the value was
    /// set successfully. This object keeps a reference to |value| and ownership
    /// of the underlying data remains unchanged.
    ///
    public delegate* unmanaged<cef_value_t*, cef_list_value_t*, int> set_list;
}

///
/// Structure representing a binary value. Can be used on any process and
/// thread.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_binary_value_t
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
    public delegate* unmanaged<cef_binary_value_t*, int> is_valid;

    ///
    /// Returns true (1) if this object is currently owned by another object.
    ///
    public delegate* unmanaged<cef_binary_value_t*, int> is_owned;

    ///
    /// Returns true (1) if this object and |that| object have the same underlying
    /// data.
    ///
    public delegate* unmanaged<cef_binary_value_t*, cef_binary_value_t*, int> is_same;

    ///
    /// Returns true (1) if this object and |that| object have an equivalent
    /// underlying value but are not necessarily the same object.
    ///
    public delegate* unmanaged<cef_binary_value_t*, cef_binary_value_t*, int> is_equal;

    ///
    /// Returns a copy of this object. The data in this object will also be
    /// copied.
    ///
    public delegate* unmanaged<cef_binary_value_t*, cef_binary_value_t*> copy;

    ///
    /// Returns a pointer to the beginning of the memory block. The returned
    /// pointer is valid as long as the cef_binary_value_t is alive.
    ///
    public delegate* unmanaged<cef_binary_value_t*, void*> get_raw_data;

    ///
    /// Returns the data size.
    ///
    public delegate* unmanaged<cef_binary_value_t*, nuint> get_size;

    ///
    /// Read up to |buffer_size| number of bytes into |buffer|. Reading begins at
    /// the specified byte |data_offset|. Returns the number of bytes read.
    ///
    public delegate* unmanaged<cef_binary_value_t*, void*, nuint, nuint, nuint> get_data;
}

///
/// Structure representing a dictionary value. Can be used on any process and
/// thread.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_dictionary_value_t
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
    public delegate* unmanaged<cef_dictionary_value_t*, int> is_valid;

    ///
    /// Returns true (1) if this object is currently owned by another object.
    ///
    public delegate* unmanaged<cef_dictionary_value_t*, int> is_owned;

    ///
    /// Returns true (1) if the values of this object are read-only. Some APIs may
    /// expose read-only objects.
    ///
    public delegate* unmanaged<cef_dictionary_value_t*, int> is_read_only;

    ///
    /// Returns true (1) if this object and |that| object have the same underlying
    /// data. If true (1) modifications to this object will also affect |that|
    /// object and vice-versa.
    ///
    public delegate* unmanaged<cef_dictionary_value_t*, cef_dictionary_value_t*, int> is_same;

    ///
    /// Returns true (1) if this object and |that| object have an equivalent
    /// underlying value but are not necessarily the same object.
    ///
    public delegate* unmanaged<cef_dictionary_value_t*, cef_dictionary_value_t*, int> is_equal;

    ///
    /// Returns a writable copy of this object. If |exclude_NULL_children| is true
    /// (1) any NULL dictionaries or lists will be excluded from the copy.
    ///
    public delegate* unmanaged<cef_dictionary_value_t*, int, cef_dictionary_value_t*> copy;

    ///
    /// Returns the number of values.
    ///
    public delegate* unmanaged<cef_dictionary_value_t*, nuint> get_size;

    ///
    /// Removes all values. Returns true (1) on success.
    ///
    public delegate* unmanaged<cef_dictionary_value_t*, int> clear;

    ///
    /// Returns true (1) if the current dictionary has a value for the given key.
    ///
    public delegate* unmanaged<cef_dictionary_value_t*, cef_string_t*, int> has_key;

    ///
    /// Reads all keys for this dictionary into the specified vector.
    ///
    public delegate* unmanaged<cef_dictionary_value_t*, nint, int> get_keys;

    ///
    /// Removes the value at the specified key. Returns true (1) is the value was
    /// removed successfully.
    ///
    public delegate* unmanaged<cef_dictionary_value_t*, cef_string_t*, int> remove;

    ///
    /// Returns the value type for the specified key.
    ///
    public delegate* unmanaged<cef_dictionary_value_t*, cef_string_t*, cef_value_type_t> get_type;

    ///
    /// Returns the value at the specified key. For simple types the returned
    /// value will copy existing data and modifications to the value will not
    /// modify this object. For complex types (binary, dictionary and list) the
    /// returned value will reference existing data and modifications to the value
    /// will modify this object.
    ///
    public delegate* unmanaged<cef_dictionary_value_t*, cef_string_t*, cef_value_t> get_value;

    ///
    /// Returns the value at the specified key as type bool.
    ///
    public delegate* unmanaged<cef_dictionary_value_t*, cef_string_t*, int> get_bool;

    ///
    /// Returns the value at the specified key as type int.
    ///
    public delegate* unmanaged<cef_dictionary_value_t*, cef_string_t*, int> get_int;

    ///
    /// Returns the value at the specified key as type double.
    ///
    public delegate* unmanaged<cef_dictionary_value_t*, cef_string_t*, double> get_double;

    ///
    /// Returns the value at the specified key as type string.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_dictionary_value_t*, cef_string_t*, cef_string_t*> get_string;

    ///
    /// Returns the value at the specified key as type binary. The returned value
    /// will reference existing data.
    ///
    public delegate* unmanaged<cef_dictionary_value_t*, cef_string_t*, cef_binary_value_t*> get_binary;

    ///
    /// Returns the value at the specified key as type dictionary. The returned
    /// value will reference existing data and modifications to the value will
    /// modify this object.
    ///
    public delegate* unmanaged<cef_dictionary_value_t*, cef_string_t*, cef_dictionary_value_t*> get_dictionary;

    ///
    /// Returns the value at the specified key as type list. The returned value
    /// will reference existing data and modifications to the value will modify
    /// this object.
    ///
    public delegate* unmanaged<cef_dictionary_value_t*, cef_string_t*, cef_list_value_t*> get_list;

    ///
    /// Sets the value at the specified key. Returns true (1) if the value was set
    /// successfully. If |value| represents simple data then the underlying data
    /// will be copied and modifications to |value| will not modify this object.
    /// If |value| represents complex data (binary, dictionary or list) then the
    /// underlying data will be referenced and modifications to |value| will
    /// modify this object.
    ///
    public delegate* unmanaged<cef_dictionary_value_t*, cef_string_t*, cef_value_t*, int> set_value;

    ///
    /// Sets the value at the specified key as type null. Returns true (1) if the
    /// value was set successfully.
    ///
    public delegate* unmanaged<cef_dictionary_value_t*, cef_string_t*, int> set_null;

    ///
    /// Sets the value at the specified key as type bool. Returns true (1) if the
    /// value was set successfully.
    ///
    public delegate* unmanaged<cef_dictionary_value_t*, cef_string_t*, int, int> set_bool;

    ///
    /// Sets the value at the specified key as type int. Returns true (1) if the
    /// value was set successfully.
    ///
    public delegate* unmanaged<cef_dictionary_value_t*, cef_string_t*, int, int> set_int;

    ///
    /// Sets the value at the specified key as type double. Returns true (1) if
    /// the value was set successfully.
    ///
    public delegate* unmanaged<cef_dictionary_value_t*, cef_string_t*, double, int> set_double;

    ///
    /// Sets the value at the specified key as type string. Returns true (1) if
    /// the value was set successfully.
    ///
    public delegate* unmanaged<cef_dictionary_value_t*, cef_string_t*, cef_string_t*, int> set_string;

    ///
    /// Sets the value at the specified key as type binary. Returns true (1) if
    /// the value was set successfully. If |value| is currently owned by another
    /// object then the value will be copied and the |value| reference will not
    /// change. Otherwise, ownership will be transferred to this object and the
    /// |value| reference will be invalidated.
    ///
    public delegate* unmanaged<cef_dictionary_value_t*, cef_string_t*, cef_binary_value_t*, int> set_binary;

    ///
    /// Sets the value at the specified key as type dict. Returns true (1) if the
    /// value was set successfully. If |value| is currently owned by another
    /// object then the value will be copied and the |value| reference will not
    /// change. Otherwise, ownership will be transferred to this object and the
    /// |value| reference will be invalidated.
    ///
    public delegate* unmanaged<cef_dictionary_value_t*, cef_string_t*, cef_dictionary_value_t*, int> set_dictionary;

    ///
    /// Sets the value at the specified key as type list. Returns true (1) if the
    /// value was set successfully. If |value| is currently owned by another
    /// object then the value will be copied and the |value| reference will not
    /// change. Otherwise, ownership will be transferred to this object and the
    /// |value| reference will be invalidated.
    ///
    public delegate* unmanaged<cef_dictionary_value_t*, cef_string_t*, cef_list_value_t*, int> set_list;
}

///
/// Structure representing a list value. Can be used on any process and thread.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_list_value_t
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
