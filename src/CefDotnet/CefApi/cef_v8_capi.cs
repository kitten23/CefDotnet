using CefDotnet.CefApi.Internal;
using CefDotnet.CefApi.Types;
using System;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// V8 property attribute values.
///
public enum cef_v8_propertyattribute_t : uint
{
    ///
    /// Writeable, Enumerable, Configurable
    ///
    V8_PROPERTY_ATTRIBUTE_NONE = 0,

    ///
    /// Not writeable
    ///
    V8_PROPERTY_ATTRIBUTE_READONLY = 1 << 0,

    ///
    /// Not enumerable
    ///
    V8_PROPERTY_ATTRIBUTE_DONTENUM = 1 << 1,

    ///
    /// Not configurable
    ///
    V8_PROPERTY_ATTRIBUTE_DONTDELETE = 1 << 2
}

///
/// Structure representing a V8 context handle. V8 handles can only be accessed
/// from the thread on which they are created. Valid threads for creating a V8
/// handle include the render process main thread (TID_RENDERER) and WebWorker
/// threads. A task runner for posting tasks on the associated thread can be
/// retrieved via the cef_v8_context_t::get_task_runner() function.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_v8_context_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Returns the task runner associated with this context. V8 handles can only
    /// be accessed from the thread on which they are created. This function can
    /// be called on any render process thread.
    ///
    public delegate* unmanaged<cef_v8_context_t*, cef_task_runner_t*> get_task_runner;

    ///
    /// Returns true (1) if the underlying handle is valid and it can be accessed
    /// on the current thread. Do not call any other functions if this function
    /// returns false (0).
    ///
    public delegate* unmanaged<cef_v8_context_t*, int> is_valid;

    ///
    /// Returns the browser for this context. This function will return an NULL
    /// reference for WebWorker contexts.
    ///
    public delegate* unmanaged<cef_v8_context_t*, cef_browser_t*> get_browser;

    ///
    /// Returns the frame for this context. This function will return an NULL
    /// reference for WebWorker contexts.
    ///
    public delegate* unmanaged<cef_v8_context_t*, cef_frame_t*> get_frame;

    ///
    /// Returns the global object for this context. The context must be entered
    /// before calling this function.
    ///
    public delegate* unmanaged<cef_v8_context_t*, cef_v8_value_t*> get_global;

    ///
    /// Enter this context. A context must be explicitly entered before creating a
    /// V8 Object, Array, Function or Date asynchronously. exit() must be called
    /// the same number of times as enter() before releasing this context. V8
    /// objects belong to the context in which they are created. Returns true (1)
    /// if the scope was entered successfully.
    ///
    public delegate* unmanaged<cef_v8_context_t*, int> enter;

    ///
    /// Exit this context. Call this function only after calling enter(). Returns
    /// true (1) if the scope was exited successfully.
    ///
    public delegate* unmanaged<cef_v8_context_t*, int> exit;

    ///
    /// Returns true (1) if this object is pointing to the same handle as |that|
    /// object.
    ///
    public delegate* unmanaged<cef_v8_context_t*, cef_v8_context_t*, int> is_same;

    ///
    /// Execute a string of JavaScript code in this V8 context. The |script_url|
    /// parameter is the URL where the script in question can be found, if any.
    /// The |start_line| parameter is the base line number to use for error
    /// reporting. On success |retval| will be set to the return value, if any,
    /// and the function will return true (1). On failure |exception| will be set
    /// to the exception, if any, and the function will return false (0).
    ///
    public delegate* unmanaged<cef_v8_context_t*, cef_string_t*, cef_string_t*, int, cef_v8_value_t**, cef_v8_exception_t**, int> eval;
}

///
/// Structure that should be implemented to handle V8 function calls. The
/// functions of this structure will be called on the thread associated with the
/// V8 function.
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_v8_handler_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Handle execution of the function identified by |name|. |object| is the
    /// receiver ('this' object) of the function. |arguments| is the list of
    /// arguments passed to the function. If execution succeeds set |retval| to
    /// the function return value. If execution fails set |exception| to the
    /// exception that will be thrown. Return true (1) if execution was handled.
    ///
    public delegate* unmanaged<cef_v8_handler_t*, cef_string_t*, cef_v8_value_t*, nuint, cef_v8_value_t**, cef_v8_value_t**, cef_string_t*, int> execute;
}

///
/// Structure that should be implemented to handle V8 accessor calls. Accessor
/// identifiers are registered by calling cef_v8_value_t::set_value(). The
/// functions of this structure will be called on the thread associated with the
/// V8 accessor.
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_v8_accessor_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Handle retrieval the accessor value identified by |name|. |object| is the
    /// receiver ('this' object) of the accessor. If retrieval succeeds set
    /// |retval| to the return value. If retrieval fails set |exception| to the
    /// exception that will be thrown. Return true (1) if accessor retrieval was
    /// handled.
    ///
    public delegate* unmanaged<cef_v8_accessor_t*, cef_string_t*, cef_v8_value_t*, cef_v8_value_t**, cef_string_t*, int> get;

    ///
    /// Handle assignment of the accessor value identified by |name|. |object| is
    /// the receiver ('this' object) of the accessor. |value| is the new value
    /// being assigned to the accessor. If assignment fails set |exception| to the
    /// exception that will be thrown. Return true (1) if accessor assignment was
    /// handled.
    ///
    public delegate* unmanaged<cef_v8_accessor_t*, cef_string_t*, cef_v8_value_t*, cef_v8_value_t*, cef_string_t*, int> set;
}

///
/// Structure that should be implemented to handle V8 interceptor calls. The
/// functions of this structure will be called on the thread associated with the
/// V8 interceptor. Interceptor's named property handlers (with first argument
/// of type CefString) are called when object is indexed by string. Indexed
/// property handlers (with first argument of type int) are called when object
/// is indexed by integer.
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_v8_interceptor_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Handle retrieval of the interceptor value identified by |name|. |object|
    /// is the receiver ('this' object) of the interceptor. If retrieval succeeds,
    /// set |retval| to the return value. If the requested value does not exist,
    /// don't set either |retval| or |exception|. If retrieval fails, set
    /// |exception| to the exception that will be thrown. If the property has an
    /// associated accessor, it will be called only if you don't set |retval|.
    /// Return true (1) if interceptor retrieval was handled, false (0) otherwise.
    ///
    public delegate* unmanaged<cef_v8_interceptor_t*, cef_string_t*, cef_v8_value_t*, cef_v8_value_t**, cef_string_t*, int> get_byname;

    ///
    /// Handle retrieval of the interceptor value identified by |index|. |object|
    /// is the receiver ('this' object) of the interceptor. If retrieval succeeds,
    /// set |retval| to the return value. If the requested value does not exist,
    /// don't set either |retval| or |exception|. If retrieval fails, set
    /// |exception| to the exception that will be thrown. Return true (1) if
    /// interceptor retrieval was handled, false (0) otherwise.
    ///
    public delegate* unmanaged<cef_v8_interceptor_t*, int, cef_v8_value_t*, cef_v8_value_t**, cef_string_t*, int> get_byindex;

    ///
    /// Handle assignment of the interceptor value identified by |name|. |object|
    /// is the receiver ('this' object) of the interceptor. |value| is the new
    /// value being assigned to the interceptor. If assignment fails, set
    /// |exception| to the exception that will be thrown. This setter will always
    /// be called, even when the property has an associated accessor. Return true
    /// (1) if interceptor assignment was handled, false (0) otherwise.
    ///
    public delegate* unmanaged<cef_v8_interceptor_t*, cef_string_t*, cef_v8_value_t*, cef_v8_value_t*, cef_string_t*, int> set_byname;

    ///
    /// Handle assignment of the interceptor value identified by |index|. |object|
    /// is the receiver ('this' object) of the interceptor. |value| is the new
    /// value being assigned to the interceptor. If assignment fails, set
    /// |exception| to the exception that will be thrown. Return true (1) if
    /// interceptor assignment was handled, false (0) otherwise.
    ///
    public delegate* unmanaged<cef_v8_interceptor_t*, int, cef_v8_value_t*, cef_v8_value_t*, cef_string_t*, int> set_byindex;
}

///
/// Structure representing a V8 exception. The functions of this structure may
/// be called on any render process thread.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_v8_exception_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Returns the exception message.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_v8_exception_t*, cef_string_t*> get_message;

    ///
    /// Returns the line of source code that the exception occurred within.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_v8_exception_t*, cef_string_t*> get_source_line;

    ///
    /// Returns the resource name for the script from where the function causing
    /// the error originates.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_v8_exception_t*, cef_string_t*> get_script_resource_name;

    ///
    /// Returns the 1-based number of the line where the error occurred or 0 if
    /// the line number is unknown.
    ///
    public delegate* unmanaged<cef_v8_exception_t*, int> get_line_number;

    ///
    /// Returns the index within the script of the first character where the error
    /// occurred.
    ///
    public delegate* unmanaged<cef_v8_exception_t*, int> get_start_position;

    ///
    /// Returns the index within the script of the last character where the error
    /// occurred.
    ///
    public delegate* unmanaged<cef_v8_exception_t*, int> get_end_position;

    ///
    /// Returns the index within the line of the first character where the error
    /// occurred.
    ///
    public delegate* unmanaged<cef_v8_exception_t*, int> get_start_column;

    ///
    /// Returns the index within the line of the last character where the error
    /// occurred.
    ///
    public delegate* unmanaged<cef_v8_exception_t*, int> get_end_column;
}

///
/// Callback structure that is passed to cef_v8_value_t::CreateArrayBuffer.
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_v8_array_buffer_release_callback_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Called to release |buffer| when the ArrayBuffer JS object is garbage
    /// collected. |buffer| is the value that was passed to CreateArrayBuffer
    /// along with this object.
    ///
    public delegate* unmanaged<cef_v8_array_buffer_release_callback_t*, void*, void> release_buffer;
}

///
/// Structure representing a V8 value handle. V8 handles can only be accessed
/// from the thread on which they are created. Valid threads for creating a V8
/// handle include the render process main thread (TID_RENDERER) and WebWorker
/// threads. A task runner for posting tasks on the associated thread can be
/// retrieved via the cef_v8_context_t::get_task_runner() function.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_v8_value_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Returns true (1) if the underlying handle is valid and it can be accessed
    /// on the current thread. Do not call any other functions if this function
    /// returns false (0).
    ///
    public delegate* unmanaged<cef_v8_value_t*, int> is_valid;

    ///
    /// True if the value type is undefined.
    ///
    public delegate* unmanaged<cef_v8_value_t*, int> is_undefined;

    ///
    /// True if the value type is null.
    ///
    public delegate* unmanaged<cef_v8_value_t*, int> is_null;

    ///
    /// True if the value type is bool.
    ///
    public delegate* unmanaged<cef_v8_value_t*, int> is_bool;

    ///
    /// True if the value type is int.
    ///
    public delegate* unmanaged<cef_v8_value_t*, int> is_int;

    ///
    /// True if the value type is unsigned int.
    ///
    public delegate* unmanaged<cef_v8_value_t*, int> is_uint;

    ///
    /// True if the value type is double.
    ///
    public delegate* unmanaged<cef_v8_value_t*, int> is_double;

    ///
    /// True if the value type is Date.
    ///
    public delegate* unmanaged<cef_v8_value_t*, int> is_date;

    ///
    /// True if the value type is string.
    ///
    public delegate* unmanaged<cef_v8_value_t*, int> is_string;

    ///
    /// True if the value type is object.
    ///
    public delegate* unmanaged<cef_v8_value_t*, int> is_object;

    ///
    /// True if the value type is array.
    ///
    public delegate* unmanaged<cef_v8_value_t*, int> is_array;

    ///
    /// True if the value type is an ArrayBuffer.
    ///
    public delegate* unmanaged<cef_v8_value_t*, int> is_array_buffer;

    ///
    /// True if the value type is function.
    ///
    public delegate* unmanaged<cef_v8_value_t*, int> is_function;

    ///
    /// True if the value type is a Promise.
    ///
    public delegate* unmanaged<cef_v8_value_t*, int> is_promise;

    ///
    /// Returns true (1) if this object is pointing to the same handle as |that|
    /// object.
    ///
    public delegate* unmanaged<cef_v8_value_t*, cef_v8_value_t*, int> is_same;

    ///
    /// Return a bool value.
    ///
    public delegate* unmanaged<cef_v8_value_t*, int> get_bool_value;

    ///
    /// Return an int value.
    ///
    public delegate* unmanaged<cef_v8_value_t*, int> get_int_value;

    ///
    /// Return an unsigned int value.
    ///
    public delegate* unmanaged<cef_v8_value_t*, uint> get_uint_value;

    ///
    /// Return a double value.
    ///
    public delegate* unmanaged<cef_v8_value_t*, double> get_double_value;

    ///
    /// Return a Date value.
    ///
    public delegate* unmanaged<cef_v8_value_t*, cef_basetime_t> get_date_value;

    ///
    /// Return a string value.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_v8_value_t*, cef_string_t*> get_string_value;

    ///
    /// Returns true (1) if this is a user created object.
    ///
    public delegate* unmanaged<cef_v8_value_t*, int> is_user_created;

    ///
    /// Returns true (1) if the last function call resulted in an exception. This
    /// attribute exists only in the scope of the current CEF value object.
    ///
    public delegate* unmanaged<cef_v8_value_t*, int> has_exception;

    ///
    /// Returns the exception resulting from the last function call. This
    /// attribute exists only in the scope of the current CEF value object.
    ///
    public delegate* unmanaged<cef_v8_value_t*, cef_v8_exception_t*> get_exception;

    ///
    /// Clears the last exception and returns true (1) on success.
    ///
    public delegate* unmanaged<cef_v8_value_t*, int> clear_exception;

    ///
    /// Returns true (1) if this object will re-throw future exceptions. This
    /// attribute exists only in the scope of the current CEF value object.
    ///
    public delegate* unmanaged<cef_v8_value_t*, int> will_rethrow_exceptions;

    ///
    /// Set whether this object will re-throw future exceptions. By default
    /// exceptions are not re-thrown. If a exception is re-thrown the current
    /// context should not be accessed again until after the exception has been
    /// caught and not re-thrown. Returns true (1) on success. This attribute
    /// exists only in the scope of the current CEF value object.
    ///
    public delegate* unmanaged<cef_v8_value_t*, int, int> set_rethrow_exceptions;

    ///
    /// Returns true (1) if the object has a value with the specified identifier.
    ///
    public delegate* unmanaged<cef_v8_value_t*, cef_string_t*, int> has_value_bykey;

    ///
    /// Returns true (1) if the object has a value with the specified identifier.
    ///
    public delegate* unmanaged<cef_v8_value_t*, int, int> has_value_byindex;

    ///
    /// Deletes the value with the specified identifier and returns true (1) on
    /// success. Returns false (0) if this function is called incorrectly or an
    /// exception is thrown. For read-only and don't-delete values this function
    /// will return true (1) even though deletion failed.
    ///
    public delegate* unmanaged<cef_v8_value_t*, cef_string_t*, int> delete_value_bykey;

    ///
    /// Deletes the value with the specified identifier and returns true (1) on
    /// success. Returns false (0) if this function is called incorrectly,
    /// deletion fails or an exception is thrown. For read-only and don't-delete
    /// values this function will return true (1) even though deletion failed.
    ///
    public delegate* unmanaged<cef_v8_value_t*, int, int> delete_value_byindex;

    ///
    /// Returns the value with the specified identifier on success. Returns NULL
    /// if this function is called incorrectly or an exception is thrown.
    ///
    public delegate* unmanaged<cef_v8_value_t*, cef_string_t*, cef_v8_value_t*> get_value_bykey;

    ///
    /// Returns the value with the specified identifier on success. Returns NULL
    /// if this function is called incorrectly or an exception is thrown.
    ///
    public delegate* unmanaged<cef_v8_value_t*, int, cef_v8_value_t*> get_value_byindex;

    ///
    /// Associates a value with the specified identifier and returns true (1) on
    /// success. Returns false (0) if this function is called incorrectly or an
    /// exception is thrown. For read-only values this function will return true
    /// (1) even though assignment failed.
    ///
    public delegate* unmanaged<cef_v8_value_t*, cef_string_t*, cef_v8_value_t*, cef_v8_propertyattribute_t, int> set_value_bykey;

    ///
    /// Associates a value with the specified identifier and returns true (1) on
    /// success. Returns false (0) if this function is called incorrectly or an
    /// exception is thrown. For read-only values this function will return true
    /// (1) even though assignment failed.
    ///
    public delegate* unmanaged<cef_v8_value_t*, int, cef_v8_value_t*, int> set_value_byindex;

    ///
    /// Registers an identifier and returns true (1) on success. Access to the
    /// identifier will be forwarded to the cef_v8_accessor_t instance passed to
    /// cef_v8_value_t::cef_v8_value_create_object(). Returns false (0) if this
    /// function is called incorrectly or an exception is thrown. For read-only
    /// values this function will return true (1) even though assignment failed.
    ///
    public delegate* unmanaged<cef_v8_value_t*, cef_string_t*, cef_v8_propertyattribute_t, int> set_value_byaccessor;

    ///
    /// Read the keys for the object's values into the specified vector. Integer-
    /// based keys will also be returned as strings.
    ///
    public delegate* unmanaged<cef_v8_value_t*, nint, int> get_keys;

    ///
    /// Sets the user data for this object and returns true (1) on success.
    /// Returns false (0) if this function is called incorrectly. This function
    /// can only be called on user created objects.
    ///
    public delegate* unmanaged<cef_v8_value_t*, cef_base_ref_counted_t*, int> set_user_data;

    ///
    /// Returns the user data, if any, assigned to this object.
    ///
    public delegate* unmanaged<cef_v8_value_t*, cef_base_ref_counted_t*> get_user_data;

    ///
    /// Returns the amount of externally allocated memory registered for the
    /// object.
    ///
    public delegate* unmanaged<cef_v8_value_t*, int> get_externally_allocated_memory;

    ///
    /// Adjusts the amount of registered external memory for the object. Used to
    /// give V8 an indication of the amount of externally allocated memory that is
    /// kept alive by JavaScript objects. V8 uses this information to decide when
    /// to perform global garbage collection. Each cef_v8_value_t tracks the
    /// amount of external memory associated with it and automatically decreases
    /// the global total by the appropriate amount on its destruction.
    /// |change_in_bytes| specifies the number of bytes to adjust by. This
    /// function returns the number of bytes associated with the object after the
    /// adjustment. This function can only be called on user created objects.
    ///
    public delegate* unmanaged<cef_v8_value_t*, int, int> adjust_externally_allocated_memory;

    ///
    /// Returns the number of elements in the array.
    ///
    public delegate* unmanaged<cef_v8_value_t*, nuint> get_array_length;

    ///
    /// Returns the ReleaseCallback object associated with the ArrayBuffer or NULL
    /// if the ArrayBuffer was not created with CreateArrayBuffer.
    ///
    public delegate* unmanaged<cef_v8_value_t*, cef_v8_array_buffer_release_callback_t*> get_array_buffer_release_callback;

    ///
    /// Prevent the ArrayBuffer from using it's memory block by setting the length
    /// to zero. This operation cannot be undone. If the ArrayBuffer was created
    /// with CreateArrayBuffer then
    /// cef_v8_array_buffer_release_callback_t::ReleaseBuffer will be called to
    /// release the underlying buffer.
    ///
    public delegate* unmanaged<cef_v8_value_t*, int> neuter_array_buffer;

    ///
    /// Returns the length (in bytes) of the ArrayBuffer.
    ///
    public delegate* unmanaged<cef_v8_value_t*, nuint> get_array_buffer_byte_length;

    ///
    /// Returns a pointer to the beginning of the memory block for this
    /// ArrayBuffer backing store. The returned pointer is valid as long as the
    /// cef_v8_value_t is alive.
    ///
    public delegate* unmanaged<cef_v8_value_t*, void*> get_array_buffer_data;

    ///
    /// Returns the function name.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_v8_value_t*, cef_string_t*> get_function_name;

    ///
    /// Returns the function handler or NULL if not a CEF-created function.
    ///
    public delegate* unmanaged<cef_v8_value_t*, cef_v8_handler_t*> get_function_handler;

    ///
    /// Execute the function using the current V8 context. This function should
    /// only be called from within the scope of a cef_v8_handler_t or
    /// cef_v8_accessor_t callback, or in combination with calling enter() and
    /// exit() on a stored cef_v8_context_t reference. |object| is the receiver
    /// ('this' object) of the function. If |object| is NULL the current context's
    /// global object will be used. |arguments| is the list of arguments that will
    /// be passed to the function. Returns the function return value on success.
    /// Returns NULL if this function is called incorrectly or an exception is
    /// thrown.
    ///
    public delegate* unmanaged<cef_v8_value_t*, cef_v8_value_t*, nuint, cef_v8_value_t**, cef_v8_value_t*> execute_function;

    ///
    /// Execute the function using the specified V8 context. |object| is the
    /// receiver ('this' object) of the function. If |object| is NULL the
    /// specified context's global object will be used. |arguments| is the list of
    /// arguments that will be passed to the function. Returns the function return
    /// value on success. Returns NULL if this function is called incorrectly or
    /// an exception is thrown.
    ///
    public delegate* unmanaged<cef_v8_value_t*, cef_v8_context_t*, cef_v8_value_t*, nuint, cef_v8_value_t**, cef_v8_value_t*> execute_function_with_context;

    ///
    /// Resolve the Promise using the current V8 context. This function should
    /// only be called from within the scope of a cef_v8_handler_t or
    /// cef_v8_accessor_t callback, or in combination with calling enter() and
    /// exit() on a stored cef_v8_context_t reference. |arg| is the argument
    /// passed to the resolved promise. Returns true (1) on success. Returns false
    /// (0) if this function is called incorrectly or an exception is thrown.
    ///
    public delegate* unmanaged<cef_v8_value_t*, cef_v8_value_t*, int> resolve_promise;

    ///
    /// Reject the Promise using the current V8 context. This function should only
    /// be called from within the scope of a cef_v8_handler_t or cef_v8_accessor_t
    /// callback, or in combination with calling enter() and exit() on a stored
    /// cef_v8_context_t reference. Returns true (1) on success. Returns false (0)
    /// if this function is called incorrectly or an exception is thrown.
    ///
    public delegate* unmanaged<cef_v8_value_t*, cef_string_t*, int> reject_promise;
}


///
/// Structure representing a V8 stack trace handle. V8 handles can only be
/// accessed from the thread on which they are created. Valid threads for
/// creating a V8 handle include the render process main thread (TID_RENDERER)
/// and WebWorker threads. A task runner for posting tasks on the associated
/// thread can be retrieved via the cef_v8_context_t::get_task_runner()
/// function.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_v8_stack_trace_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Returns true (1) if the underlying handle is valid and it can be accessed
    /// on the current thread. Do not call any other functions if this function
    /// returns false (0).
    ///
    public delegate* unmanaged<cef_v8_stack_trace_t*, int> is_valid;

    ///
    /// Returns the number of stack frames.
    ///
    public delegate* unmanaged<cef_v8_stack_trace_t*, int> get_frame_count;

    ///
    /// Returns the stack frame at the specified 0-based index.
    ///
    public delegate* unmanaged<cef_v8_stack_trace_t*, int, cef_v8_stack_frame_t*> get_frame;
}

///
/// Structure representing a V8 stack frame handle. V8 handles can only be
/// accessed from the thread on which they are created. Valid threads for
/// creating a V8 handle include the render process main thread (TID_RENDERER)
/// and WebWorker threads. A task runner for posting tasks on the associated
/// thread can be retrieved via the cef_v8_context_t::get_task_runner()
/// function.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_v8_stack_frame_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Returns true (1) if the underlying handle is valid and it can be accessed
    /// on the current thread. Do not call any other functions if this function
    /// returns false (0).
    ///
    public delegate* unmanaged<cef_v8_stack_frame_t*, int> is_valid;

    ///
    /// Returns the name of the resource script that contains the function.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_v8_stack_frame_t*, cef_string_t*> get_script_name;

    ///
    /// Returns the name of the resource script that contains the function or the
    /// sourceURL value if the script name is undefined and its source ends with a
    /// "//@ sourceURL=..." string.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_v8_stack_frame_t*, cef_string_t*> get_script_name_or_source_url;

    ///
    /// Returns the name of the function.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_v8_stack_frame_t*, cef_string_t*> get_function_name;

    ///
    /// Returns the 1-based line number for the function call or 0 if unknown.
    ///
    public delegate* unmanaged<cef_v8_stack_frame_t*, int> get_line_number;

    ///
    /// Returns the 1-based column offset on the line for the function call or 0
    /// if unknown.
    ///
    public delegate* unmanaged<cef_v8_stack_frame_t*, int> get_column;

    ///
    /// Returns true (1) if the function was compiled using eval().
    ///
    public delegate* unmanaged<cef_v8_stack_frame_t*, int> is_eval;

    ///
    /// Returns true (1) if the function was called as a constructor via "new".
    ///
    public delegate* unmanaged<cef_v8_stack_frame_t*, int> is_constructor;
}
