using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// Structure the client can implement to provide a custom stream reader. The
/// functions of this structure may be called on any thread.
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_read_handler_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Read raw binary data.
    ///
    public delegate* unmanaged<cef_read_handler_t*, void*, nuint, nuint, nuint> read;

    ///
    /// Seek to the specified offset position. |whence| may be any one of
    /// SEEK_CUR, SEEK_END or SEEK_SET. Return zero on success and non-zero on
    /// failure.
    ///
    public delegate* unmanaged<cef_read_handler_t*, long, int, int> seek;

    ///
    /// Return the current offset position.
    ///
    public delegate* unmanaged<cef_read_handler_t*, long> tell;

    ///
    /// Return non-zero if at end of file.
    ///
    public delegate* unmanaged<cef_read_handler_t*, int> eof;

    ///
    /// Return true (1) if this handler performs work like accessing the file
    /// system which may block. Used as a hint for determining the thread to
    /// access the handler from.
    ///
    public delegate* unmanaged<cef_read_handler_t*, int> may_block;
}

///
/// Structure used to read data from a stream. The functions of this structure
/// may be called on any thread.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_stream_reader_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Read raw binary data.
    ///
    public delegate* unmanaged<cef_stream_reader_t*, void*, nuint, nuint, nuint> read;

    ///
    /// Seek to the specified offset position. |whence| may be any one of
    /// SEEK_CUR, SEEK_END or SEEK_SET. Returns zero on success and non-zero on
    /// failure.
    ///
    public delegate* unmanaged<cef_stream_reader_t*, long, int, int> seek;

    ///
    /// Return the current offset position.
    ///
    public delegate* unmanaged<cef_stream_reader_t*, long> tell;

    ///
    /// Return non-zero if at end of file.
    ///
    public delegate* unmanaged<cef_stream_reader_t*, int> eof;

    ///
    /// Returns true (1) if this reader performs work like accessing the file
    /// system which may block. Used as a hint for determining the thread to
    /// access the reader from.
    ///
    public delegate* unmanaged<cef_stream_reader_t*, int> may_block;
}

///
/// Structure the client can implement to provide a custom stream writer. The
/// functions of this structure may be called on any thread.
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_write_handler_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Write raw binary data.
    ///
    public delegate* unmanaged<cef_read_handler_t*, void*, nuint, nuint, nuint> write;

    ///
    /// Seek to the specified offset position. |whence| may be any one of
    /// SEEK_CUR, SEEK_END or SEEK_SET. Return zero on success and non-zero on
    /// failure.
    ///
    public delegate* unmanaged<cef_read_handler_t*, long, int, int> seek;

    ///
    /// Return the current offset position.
    ///
    public delegate* unmanaged<cef_read_handler_t*, long> tell;

    ///
    /// Flush the stream.
    ///
    public delegate* unmanaged<cef_read_handler_t*, int> flush;

    ///
    /// Return true (1) if this handler performs work like accessing the file
    /// system which may block. Used as a hint for determining the thread to
    /// access the handler from.
    ///
    public delegate* unmanaged<cef_read_handler_t*, int> may_block;
}

///
/// Structure used to write data to a stream. The functions of this structure
/// may be called on any thread.
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_stream_writer_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Write raw binary data.
    ///
    public delegate* unmanaged<cef_read_handler_t*, void*, nuint, nuint, nuint> write;

    ///
    /// Seek to the specified offset position. |whence| may be any one of
    /// SEEK_CUR, SEEK_END or SEEK_SET. Returns zero on success and non-zero on
    /// failure.
    ///
    public delegate* unmanaged<cef_read_handler_t*, long, int, int> seek;

    ///
    /// Return the current offset position.
    ///
    public delegate* unmanaged<cef_read_handler_t*, long> tell;

    ///
    /// Flush the stream.
    ///
    public delegate* unmanaged<cef_read_handler_t*, int> flush;

    ///
    /// Returns true (1) if this writer performs work like accessing the file
    /// system which may block. Used as a hint for determining the thread to
    /// access the writer from.
    ///
    public delegate* unmanaged<cef_read_handler_t*, int> may_block;
}