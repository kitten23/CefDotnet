namespace CefDotnet.CefApi.Types;

public enum cef_log_severity_t : uint
{
    ///
    /// Default logging (currently INFO logging).
    ///
    LOGSEVERITY_DEFAULT,

    ///
    /// Verbose logging.
    ///
    LOGSEVERITY_VERBOSE,

    ///
    /// DEBUG logging.
    ///
    LOGSEVERITY_DEBUG = LOGSEVERITY_VERBOSE,

    ///
    /// INFO logging.
    ///
    LOGSEVERITY_INFO,

    ///
    /// WARNING logging.
    ///
    LOGSEVERITY_WARNING,

    ///
    /// ERROR logging.
    ///
    LOGSEVERITY_ERROR,

    ///
    /// FATAL logging.
    ///
    LOGSEVERITY_FATAL,

    ///
    /// Disable logging to file for all messages, and to stderr for messages with
    /// severity less than FATAL.
    ///
    LOGSEVERITY_DISABLE = 99
}
