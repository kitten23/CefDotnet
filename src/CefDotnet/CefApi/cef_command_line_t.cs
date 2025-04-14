using CefDotnet.CefApi.Types;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// Structure used to create and/or parse command line arguments. Arguments with
/// "--", "-" and, on Windows, "/" prefixes are considered switches. Switches
/// will always precede any arguments without switch prefixes. Switches can
/// optionally have a value specified using the "=" delimiter (e.g.
/// "-switch=value"). An argument of "--" will terminate switch parsing with all
/// subsequent tokens, regardless of prefix, being interpreted as non-switch
/// arguments. Switch names should be lowercase ASCII and will be converted to
/// such if necessary. Switch values will retain the original case and UTF8
/// encoding. This structure can be used before cef_initialize() is called.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_command_line_t
{
    ///
    /// Base structure.
    ///
    public cef_base_ref_counted_t @base;

    ///
    /// Returns true (1) if this object is valid. Do not call any other functions
    /// if this function returns false (0).
    ///
    public delegate* unmanaged<cef_command_line_t*, int> is_valid;

    ///
    /// Returns true (1) if the values of this object are read-only. Some APIs may
    /// expose read-only objects.
    ///
    public delegate* unmanaged<cef_command_line_t*, int> is_read_only;

    ///
    /// Returns a writable copy of this object.
    ///
    public delegate* unmanaged<cef_command_line_t*, cef_command_line_t*> copy;

    ///
    /// Initialize the command line with the specified |argc| and |argv| values.
    /// The first argument must be the name of the program. This function is only
    /// supported on non-Windows platforms.
    ///    
    public delegate* unmanaged<cef_command_line_t*, int, char**, void> init_from_argv;

    ///
    /// Initialize the command line with the string returned by calling
    /// GetCommandLineW(). This function is only supported on Windows.
    ///
    public delegate* unmanaged<cef_command_line_t*, cef_string_t*, void> init_from_string;

    ///
    /// Reset the command-line switches and arguments but leave the program
    /// component unchanged.
    ///
    public delegate* unmanaged<cef_command_line_t*, void> reset;

    ///
    /// Retrieve the original command line string as a vector of strings. The argv
    /// array: `{ program, [(--|-|/)switch[=value]]*, [--], [argument]* }`
    ///
    public delegate* unmanaged<cef_command_line_t*, nint, void> get_argv;

    ///
    /// Constructs and returns the represented command line string. Use this
    /// function cautiously because quoting behavior is unclear.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_command_line_t*, cef_string_t*> get_command_line_string;

    ///
    /// Get the program part of the command line string (the first item).
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_command_line_t*, cef_string_t*, void> get_program;

    ///
    /// Set the program part of the command line string (the first item).
    ///
    public delegate* unmanaged<cef_command_line_t*, cef_string_t*, void> set_program;

    ///
    /// Returns true (1) if the command line has switches.
    ///
    public delegate* unmanaged<cef_command_line_t*, int> has_switches;

    ///
    /// Returns true (1) if the command line contains the given switch.
    ///
    public delegate* unmanaged<cef_command_line_t*, cef_string_t*, int> has_switch;

    ///
    /// Returns the value associated with the given switch. If the switch has no
    /// value or isn't present this function returns the NULL string.
    ///
    // The resulting string must be freed by calling cef_string_userfree_free().
    public delegate* unmanaged<cef_command_line_t*, cef_string_t*, cef_string_t*> get_switch_value;

    ///
    /// Returns the map of switch names and values. If a switch has no value an
    /// NULL string is returned.
    ///
    public delegate* unmanaged<cef_command_line_t*, nint, void> get_switches;

    ///
    /// Add a switch to the end of the command line.
    ///
    public delegate* unmanaged<cef_command_line_t*, cef_string_t*, void> append_switch;

    ///
    /// Add a switch with the specified value to the end of the command line. If
    /// the switch has no value pass an NULL value string.
    ///
    public delegate* unmanaged<cef_command_line_t*, cef_string_t*, cef_string_t*, void> append_switch_with_value;

    ///
    /// True if there are remaining command line arguments.
    ///
    public delegate* unmanaged<cef_command_line_t*, int> has_arguments;

    ///
    /// Get the remaining command line arguments.
    ///
    public delegate* unmanaged<cef_command_line_t*, nint, void> get_arguments;

    ///
    /// Add an argument to the end of the command line.
    ///
    public delegate* unmanaged<cef_command_line_t*, cef_string_t*, void> append_argument;

    ///
    /// Insert a command before the current command. Common for debuggers, like
    /// "valgrind" or "gdb --args".
    ///
    public delegate* unmanaged<cef_command_line_t*, cef_string_t*, void> prepend_wrapper;
}
