namespace CefDotnet.CefApi.Types;

///
/// Log items prepended to each log line.
///
public enum cef_log_items_t : uint
{
    ///
    /// Prepend the default list of items.
    ///
    LOG_ITEMS_DEFAULT = 0,

    ///
    /// Prepend no items.
    ///
    LOG_ITEMS_NONE = 1,

    ///
    /// Prepend the process ID.
    ///
    LOG_ITEMS_FLAG_PROCESS_ID = 1 << 1,

    ///
    /// Prepend the thread ID.
    ///
    LOG_ITEMS_FLAG_THREAD_ID = 1 << 2,

    ///
    /// Prepend the timestamp.
    ///
    LOG_ITEMS_FLAG_TIME_STAMP = 1 << 3,

    ///
    /// Prepend the tickcount.
    ///
    LOG_ITEMS_FLAG_TICK_COUNT = 1 << 4,

}
