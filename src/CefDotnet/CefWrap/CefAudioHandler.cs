using CefDotnet.CefApi;
using CefDotnet.CefApi.Types;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Threading.Channels;
using System.Runtime.InteropServices;

namespace CefDotnet.CefWrap;

unsafe public class CefAudioHandler : CefBaseRefCounted<cef_audio_handler_t>
{
    public CefAudioHandler()
    {
        _get_audio_parameters = (cef_audio_handler_t* self, cef_browser_t* browser, cef_audio_parameters_t* param) =>
        {
            var ret = GetAudioParameters?.Invoke(self, browser, param) ?? 0;
            Ref.Release(browser);
            return ret;
        };
        _on_audio_stream_started = (cef_audio_handler_t* self, cef_browser_t* browser, cef_audio_parameters_t* param, int channels) =>
        {
            OnAudioStreamStarted?.Invoke(self, browser, param, channels);
            Ref.Release(browser);
        };
        _on_audio_stream_packet = (cef_audio_handler_t* self, cef_browser_t* browser, float** data, int frames, long pts) =>
        {
            OnAudioStreamPacket?.Invoke(self, browser, data, frames, pts);
            Ref.Release(browser);
        };
        _on_audio_stream_stopped = (cef_audio_handler_t* self, cef_browser_t* browser) =>
        {
            OnAudioStreamStopped?.Invoke(self, browser);
            Ref.Release(browser);
        };
        _on_audio_stream_error = (cef_audio_handler_t* self, cef_browser_t* browser, cef_string_t* message) =>
        {
            OnAudioStreamError?.Invoke(self, browser, message);
            Ref.Release(browser);
        };

        Ptr->get_audio_parameters = (delegate* unmanaged<cef_audio_handler_t*, cef_browser_t*, cef_audio_parameters_t*, int>)Marshal.GetFunctionPointerForDelegate(_get_audio_parameters);
        Ptr->on_audio_stream_started = (delegate* unmanaged<cef_audio_handler_t*, cef_browser_t*, cef_audio_parameters_t*, int, void>)Marshal.GetFunctionPointerForDelegate(_on_audio_stream_started);
        Ptr->on_audio_stream_packet = (delegate* unmanaged<cef_audio_handler_t*, cef_browser_t*, float**, int, long, void>)Marshal.GetFunctionPointerForDelegate(_on_audio_stream_packet);
        Ptr->on_audio_stream_stopped = (delegate* unmanaged<cef_audio_handler_t*, cef_browser_t*, void>)Marshal.GetFunctionPointerForDelegate(_on_audio_stream_stopped);
        Ptr->on_audio_stream_error = (delegate* unmanaged<cef_audio_handler_t*, cef_browser_t*, cef_string_t*, void>)Marshal.GetFunctionPointerForDelegate(_on_audio_stream_error);
    }

    get_audio_parameters _get_audio_parameters;
    on_audio_stream_started _on_audio_stream_started;
    on_audio_stream_packet _on_audio_stream_packet;
    on_audio_stream_stopped _on_audio_stream_stopped;
    on_audio_stream_error _on_audio_stream_error;

    public get_audio_parameters? GetAudioParameters { get; set; }
    public on_audio_stream_started? OnAudioStreamStarted { get; set; }
    public on_audio_stream_packet? OnAudioStreamPacket { get; set; }
    public on_audio_stream_stopped? OnAudioStreamStopped { get; set; }
    public on_audio_stream_error? OnAudioStreamError { get; set; }

    public delegate int get_audio_parameters(cef_audio_handler_t* self, cef_browser_t* browser, cef_audio_parameters_t* param);
    public delegate void on_audio_stream_started(cef_audio_handler_t* self, cef_browser_t* browser, cef_audio_parameters_t* param, int channels);
    public delegate void on_audio_stream_packet(cef_audio_handler_t* self, cef_browser_t* browser, float** data, int frames, long pts);
    public delegate void on_audio_stream_stopped(cef_audio_handler_t* self, cef_browser_t* browser);
    public delegate void on_audio_stream_error(cef_audio_handler_t* self, cef_browser_t* browser, cef_string_t* message);
}
