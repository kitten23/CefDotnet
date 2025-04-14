using CefDotnet.CefApi;
using CefDotnet.CefApi.Types;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CefDotnet.CefWrap;

unsafe public class CefDownloadHandler : CefBaseRefCounted<cef_download_handler_t>
{
    public CefDownloadHandler()
    {
        _CanDownload = (cef_download_handler_t* self, cef_browser_t* browser, cef_string_t* url, cef_string_t* request_method) =>
        {
            var ret = CanDownload?.Invoke(self, browser, url, request_method) ?? 0;
            Ref.Release(browser);
            return ret;
        };
        _OnBeforeDownload = (cef_download_handler_t* self, cef_browser_t* browser, cef_download_item_t* download_item, cef_string_t* suggested_name, cef_before_download_callback_t* callback) =>
        {
            var ret = OnBeforeDownload?.Invoke(self, browser, download_item, suggested_name, callback) ?? 0;
            Ref.Release(browser);
            Ref.Release(download_item);
            Ref.Release(callback);
            return ret;
        };
        _OnDownloadUpdated = (cef_download_handler_t* self, cef_browser_t* browser, cef_download_item_t* download_item, cef_download_item_callback_t* callback) =>
        {
            OnDownloadUpdated?.Invoke(self, browser, download_item, callback);
            Ref.Release(browser);
            Ref.Release(download_item);
            Ref.Release(callback);
        };

        Ptr->can_download = (delegate* unmanaged<cef_download_handler_t*, cef_browser_t*, cef_string_t*, cef_string_t*, int>)Marshal.GetFunctionPointerForDelegate(_CanDownload);
        Ptr->on_before_download = (delegate* unmanaged<cef_download_handler_t*, cef_browser_t*, cef_download_item_t*, cef_string_t*, cef_before_download_callback_t*, int>)Marshal.GetFunctionPointerForDelegate(_OnBeforeDownload);
        Ptr->on_download_updated = (delegate* unmanaged<cef_download_handler_t*, cef_browser_t*, cef_download_item_t*, cef_download_item_callback_t*, void>)Marshal.GetFunctionPointerForDelegate(_OnDownloadUpdated);
    }

    can_download _CanDownload;
    on_before_download _OnBeforeDownload;
    on_download_updated _OnDownloadUpdated;

    public can_download? CanDownload { get; set; }
    public on_before_download? OnBeforeDownload { get; set; }
    public on_download_updated? OnDownloadUpdated { get; set; }

    ///
    /// Called before a download begins in response to a user-initiated action
    /// (e.g. alt + link click or link click that returns a `Content-Disposition:
    /// attachment` response from the server). |url| is the target download URL
    /// and |request_function| is the target function (GET, POST, etc). Return
    /// true (1) to proceed with the download or false (0) to cancel the download.
    ///
    public delegate int can_download(cef_download_handler_t* self, cef_browser_t* browser, cef_string_t* url, cef_string_t* request_method);

    ///
    /// Called before a download begins. |suggested_name| is the suggested name
    /// for the download file. Return true (1) and execute |callback| either
    /// asynchronously or in this function to continue or cancel the download.
    /// Return false (0) to proceed with default handling (cancel with Alloy
    /// style, download shelf with Chrome style). Do not keep a reference to
    /// |download_item| outside of this function.
    ///
    public delegate int on_before_download(cef_download_handler_t* self, cef_browser_t* browser, cef_download_item_t* download_item, cef_string_t* suggested_name, cef_before_download_callback_t* callback);

    ///
    /// Called when a download's status or progress information has been updated.
    /// This may be called multiple times before and after on_before_download().
    /// Execute |callback| either asynchronously or in this function to cancel the
    /// download if desired. Do not keep a reference to |download_item| outside of
    /// this function.
    ///
    public delegate void on_download_updated(cef_download_handler_t* self, cef_browser_t* browser, cef_download_item_t* download_item, cef_download_item_callback_t* callback);
}
