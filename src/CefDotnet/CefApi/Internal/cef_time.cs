using System.Runtime.InteropServices;

namespace CefDotnet.CefApi.Internal;

///
/// Represents a wall clock time in UTC. Values are not guaranteed to be
/// monotonically non-decreasing and are subject to large amounts of skew.
/// Time is stored internally as microseconds since the Windows epoch (1601).
///
/// This is equivalent of Chromium `base::Time` (see base/time/time.h).
///
[StructLayout(LayoutKind.Sequential)]
unsafe public ref struct cef_basetime_t
{
   public long val;
}

///
/// Time information. Values should always be in UTC.
///
[StructLayout(LayoutKind.Sequential)]
unsafe public ref struct cef_time_t
{
    ///
    /// Four or five digit year "2007" (1601 to 30827 on Windows, 1970 to 2038 on
    /// 32-bit POSIX)
    ///
    public int year;

    ///
    /// 1-based month (values 1 = January, etc.)
    ///
    public int month;

    ///
    /// 0-based day of week (0 = Sunday, etc.)
    ///
    public int day_of_week;

    ///
    /// 1-based day of month (1-31)
    ///
    public int day_of_month;

    ///
    /// Hour within the current day (0-23)
    ///
    public int hour;

    ///
    /// Minute within the current hour (0-59)
    ///
    public int minute;

    ///
    /// Second within the current minute (0-59 plus leap seconds which may take
    /// it up to 60).
    ///
    public int second;

    ///
    /// Milliseconds within the current second (0-999)
    ///
    public int millisecond;
}
