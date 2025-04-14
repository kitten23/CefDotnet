using CefDotnet.CefApi.Types;
using System;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// Existing thread IDs.
///
public enum cef_thread_id_t : int
{
    // BROWSER PROCESS THREADS -- Only available in the browser process.

    ///
    /// The main thread in the browser. This will be the same as the main
    /// application thread if CefInitialize() is called with a
    /// CefSettings.multi_threaded_message_loop value of false. Do not perform
    /// blocking tasks on this thread. All tasks posted after
    /// CefBrowserProcessHandler::OnContextInitialized() and before CefShutdown()
    /// are guaranteed to run. This thread will outlive all other CEF threads.
    ///
    TID_UI,

    ///
    /// Used for blocking tasks like file system access where the user won't
    /// notice if the task takes an arbitrarily long time to complete. All tasks
    /// posted after CefBrowserProcessHandler::OnContextInitialized() and before
    /// CefShutdown() are guaranteed to run.
    ///
    TID_FILE_BACKGROUND,

    ///
    /// Used for blocking tasks like file system access that affect UI or
    /// responsiveness of future user interactions. Do not use if an immediate
    /// response to a user interaction is expected. All tasks posted after
    /// CefBrowserProcessHandler::OnContextInitialized() and before CefShutdown()
    /// are guaranteed to run.
    /// Examples:
    /// - Updating the UI to reflect progress on a long task.
    /// - Loading data that might be shown in the UI after a future user
    ///   interaction.
    ///
    TID_FILE_USER_VISIBLE,

    ///
    /// Used for blocking tasks like file system access that affect UI
    /// immediately after a user interaction. All tasks posted after
    /// CefBrowserProcessHandler::OnContextInitialized() and before CefShutdown()
    /// are guaranteed to run.
    /// Example: Generating data shown in the UI immediately after a click.
    ///
    TID_FILE_USER_BLOCKING,

    ///
    /// Used to launch and terminate browser processes.
    ///
    TID_PROCESS_LAUNCHER,

    ///
    /// Used to process IPC and network messages. Do not perform blocking tasks on
    /// this thread. All tasks posted after
    /// CefBrowserProcessHandler::OnContextInitialized() and before CefShutdown()
    /// are guaranteed to run.
    ///
    TID_IO,

    // RENDER PROCESS THREADS -- Only available in the render process.

    ///
    /// The main thread in the renderer. Used for all WebKit and V8 interaction.
    /// Tasks may be posted to this thread after
    /// CefRenderProcessHandler::OnWebKitInitialized but are not guaranteed to
    /// run before sub-process termination (sub-processes may be killed at any
    /// time without warning).
    ///
    TID_RENDERER,

    TID_NUM_VALUES,
}

///
/// Implement this structure for asynchronous task execution. If the task is
/// posted successfully and if the associated message loop is still running then
/// the execute() function will be called on the target thread. If the task
/// fails to post then the task object may be destroyed on the source thread
/// instead of the target thread. For this reason be cautious when performing
/// work in the task object destructor.
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_task_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Method that will be executed on the target thread.
    ///
    public delegate* unmanaged<cef_task_t*, void> execute;
}

///
/// Structure that asynchronously executes tasks on the associated thread. It is
/// safe to call the functions of this structure on any thread.
///
/// CEF maintains multiple internal threads that are used for handling different
/// types of tasks in different processes. The cef_thread_id_t definitions in
/// cef_types.h list the common CEF threads. Task runners are also available for
/// other CEF threads as appropriate (for example, V8 WebWorker threads).
///
/// NOTE: This struct is allocated DLL-side.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_task_runner_t
{
    ///
    /// Base structure.
    ///
    public cef_base_ref_counted_t @base;

    ///
    /// Returns true (1) if this object is pointing to the same task runner as
    /// |that| object.
    ///
    public delegate* unmanaged<cef_task_runner_t*, cef_task_runner_t*, int> is_same;

    ///
    /// Returns true (1) if this task runner belongs to the current thread.
    ///
    public delegate* unmanaged<cef_task_runner_t*, int> belongs_to_current_thread;

    ///
    /// Returns true (1) if this task runner is for the specified CEF thread.
    ///
    public delegate* unmanaged<cef_task_runner_t*, cef_thread_id_t, int> belongs_to_thread;

    ///
    /// Post a task for execution on the thread associated with this task runner.
    /// Execution will occur asynchronously.
    ///
    public delegate* unmanaged<cef_task_runner_t*, cef_task_t*, int> post_task;

    ///
    /// Post a task for delayed execution on the thread associated with this task
    /// runner. Execution will occur asynchronously. Delayed tasks are not
    /// supported on V8 WebWorker threads and will be executed without the
    /// specified delay.
    ///
    public delegate* unmanaged<cef_task_runner_t*, cef_task_t*, long, int> post_delayed_task;
}
