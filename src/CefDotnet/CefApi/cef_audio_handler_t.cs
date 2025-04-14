using CefDotnet.CefApi.Internal;
using CefDotnet.CefApi.Types;
using System.Runtime.InteropServices;

namespace CefDotnet.CefApi;

///
/// Enumerates the various representations of the ordering of audio channels.
/// Must be kept synchronized with media::ChannelLayout from Chromium.
/// See media\base\channel_layout.h
///
public enum cef_channel_layout_t:int
{
    CEF_CHANNEL_LAYOUT_NONE,
    CEF_CHANNEL_LAYOUT_UNSUPPORTED,

    /// Front C
    CEF_CHANNEL_LAYOUT_MONO,

    /// Front L, Front R
    CEF_CHANNEL_LAYOUT_STEREO,

    /// Front L, Front R, Back C
    CEF_CHANNEL_LAYOUT_2_1,

    /// Front L, Front R, Front C
    CEF_CHANNEL_LAYOUT_SURROUND,

    /// Front L, Front R, Front C, Back C
    CEF_CHANNEL_LAYOUT_4_0,

    /// Front L, Front R, Side L, Side R
    CEF_CHANNEL_LAYOUT_2_2,

    /// Front L, Front R, Back L, Back R
    CEF_CHANNEL_LAYOUT_QUAD,

    /// Front L, Front R, Front C, Side L, Side R
    CEF_CHANNEL_LAYOUT_5_0,

    /// Front L, Front R, Front C, LFE, Side L, Side R
    CEF_CHANNEL_LAYOUT_5_1,

    /// Front L, Front R, Front C, Back L, Back R
    CEF_CHANNEL_LAYOUT_5_0_BACK,

    /// Front L, Front R, Front C, LFE, Back L, Back R
    CEF_CHANNEL_LAYOUT_5_1_BACK,

    /// Front L, Front R, Front C, Back L, Back R, Side L, Side R
    CEF_CHANNEL_LAYOUT_7_0,

    /// Front L, Front R, Front C, LFE, Back L, Back R, Side L, Side R
    CEF_CHANNEL_LAYOUT_7_1,

    /// Front L, Front R, Front C, LFE, Front LofC, Front RofC, Side L, Side R
    CEF_CHANNEL_LAYOUT_7_1_WIDE,

    /// Front L, Front R
    CEF_CHANNEL_LAYOUT_STEREO_DOWNMIX,

    /// Front L, Front R, LFE
    CEF_CHANNEL_LAYOUT_2POINT1,

    /// Front L, Front R, Front C, LFE
    CEF_CHANNEL_LAYOUT_3_1,

    /// Front L, Front R, Front C, LFE, Back C
    CEF_CHANNEL_LAYOUT_4_1,

    /// Front L, Front R, Front C, Back C, Side L, Side R
    CEF_CHANNEL_LAYOUT_6_0,

    /// Front L, Front R, Front LofC, Front RofC, Side L, Side R
    CEF_CHANNEL_LAYOUT_6_0_FRONT,

    /// Front L, Front R, Front C, Back L, Back R, Back C
    CEF_CHANNEL_LAYOUT_HEXAGONAL,

    /// Front L, Front R, Front C, LFE, Back C, Side L, Side R
    CEF_CHANNEL_LAYOUT_6_1,

    /// Front L, Front R, Front C, LFE, Back L, Back R, Back C
    CEF_CHANNEL_LAYOUT_6_1_BACK,

    /// Front L, Front R, LFE, Front LofC, Front RofC, Side L, Side R
    CEF_CHANNEL_LAYOUT_6_1_FRONT,

    /// Front L, Front R, Front C, Front LofC, Front RofC, Side L, Side R
    CEF_CHANNEL_LAYOUT_7_0_FRONT,

    /// Front L, Front R, Front C, LFE, Back L, Back R, Front LofC, Front RofC
    CEF_CHANNEL_LAYOUT_7_1_WIDE_BACK,

    /// Front L, Front R, Front C, Back L, Back R, Back C, Side L, Side R
    CEF_CHANNEL_LAYOUT_OCTAGONAL,

    /// Channels are not explicitly mapped to speakers.
    CEF_CHANNEL_LAYOUT_DISCRETE,

    /// Deprecated, but keeping the enum value for UMA consistency.
    /// Front L, Front R, Front C. Front C contains the keyboard mic audio. This
    /// layout is only intended for input for WebRTC. The Front C channel
    /// is stripped away in the WebRTC audio input pipeline and never seen outside
    /// of that.
    CEF_CHANNEL_LAYOUT_STEREO_AND_KEYBOARD_MIC,

    /// Front L, Front R, LFE, Side L, Side R
    CEF_CHANNEL_LAYOUT_4_1_QUAD_SIDE,

    /// Actual channel layout is specified in the bitstream and the actual channel
    /// count is unknown at Chromium media pipeline level (useful for audio
    /// pass-through mode).
    CEF_CHANNEL_LAYOUT_BITSTREAM,

    /// Front L, Front R, Front C, LFE, Side L, Side R,
    /// Front Height L, Front Height R, Rear Height L, Rear Height R
    /// Will be represented as six channels (5.1) due to eight channel limit
    /// kMaxConcurrentChannels
    CEF_CHANNEL_LAYOUT_5_1_4_DOWNMIX,

    /// Front C, LFE
    CEF_CHANNEL_LAYOUT_1_1,

    /// Front L, Front R, LFE, Back C
    CEF_CHANNEL_LAYOUT_3_1_BACK,

    CEF_CHANNEL_NUM_VALUES,
}

///
/// Structure representing the audio parameters for setting up the audio
/// handler.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct cef_audio_parameters_t
{
    public cef_audio_parameters_t()
    {
        size = (nuint)sizeof(cef_audio_parameters_t);
    }

    ///
    /// Size of this structure.
    ///
    nuint size;

    ///
    /// Layout of the audio channels
    ///
    public cef_channel_layout_t channel_layout;

    ///
    /// Sample rate
    //
    public int sample_rate;

    ///
    /// Number of frames per buffer
    ///
    public int frames_per_buffer;
}

///
/// Implement this structure to handle audio events.
///
/// NOTE: This struct is allocated client-side.
///
[StructLayout(LayoutKind.Sequential)]
unsafe public struct cef_audio_handler_t
{
    ///
    /// Base structure.
    ///
    cef_base_ref_counted_t @base;

    ///
    /// Called on the UI thread to allow configuration of audio stream parameters.
    /// Return true (1) to proceed with audio stream capture, or false (0) to
    /// cancel it. All members of |params| can optionally be configured here, but
    /// they are also pre-filled with some sensible defaults.
    ///
    public delegate* unmanaged<cef_audio_handler_t*, cef_browser_t*, cef_audio_parameters_t*, int> get_audio_parameters;

    ///
    /// Called on a browser audio capture thread when the browser starts streaming
    /// audio. OnAudioStreamStopped will always be called after
    /// OnAudioStreamStarted; both functions may be called multiple times for the
    /// same browser. |params| contains the audio parameters like sample rate and
    /// channel layout. |channels| is the number of channels.
    ///
    public delegate* unmanaged<cef_audio_handler_t*, cef_browser_t*, cef_audio_parameters_t*, int, void> on_audio_stream_started;

    ///
    /// Called on the audio stream thread when a PCM packet is received for the
    /// stream. |data| is an array representing the raw PCM data as a floating
    /// point type, i.e. 4-byte value(s). |frames| is the number of frames in the
    /// PCM packet. |pts| is the presentation timestamp (in milliseconds since the
    /// Unix Epoch) and represents the time at which the decompressed packet
    /// should be presented to the user. Based on |frames| and the
    /// |channel_layout| value passed to OnAudioStreamStarted you can calculate
    /// the size of the |data| array in bytes.
    ///
    public delegate* unmanaged<cef_audio_handler_t*, cef_browser_t*, float**, int, long, void> on_audio_stream_packet;

    ///
    /// Called on the UI thread when the stream has stopped. OnAudioSteamStopped
    /// will always be called after OnAudioStreamStarted; both functions may be
    /// called multiple times for the same stream.
    ///
    public delegate* unmanaged<cef_audio_handler_t*, cef_browser_t*, void> on_audio_stream_stopped;

    ///
    /// Called on the UI or audio stream thread when an error occurred. During the
    /// stream creation phase this callback will be called on the UI thread while
    /// in the capturing phase it will be called on the audio stream thread. The
    /// stream will be stopped immediately.
    ///
    public delegate* unmanaged<cef_audio_handler_t*, cef_browser_t*, cef_string_t*, void> on_audio_stream_error;
}
