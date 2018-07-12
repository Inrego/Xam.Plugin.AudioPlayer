using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AudioPlayer.Abstractions;
using AudioPlayer.Android.Enums;
using Com.Google.Android.Exoplayer2;
using Com.Google.Android.Exoplayer2.Source;
using Com.Google.Android.Exoplayer2.Trackselection;
using Object = Java.Lang.Object;

namespace AudioPlayer.Android
{
    public class AudioPlayerEventListener : IPlayerEventListener
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IntPtr Handle { get; }

        public delegate void LoadingChangedDelegate(bool loading);

        public event LoadingChangedDelegate LoadingChanged;
        public void OnLoadingChanged(bool p0)
        {
            LoadingChanged?.Invoke(p0);
        }

        public delegate void PlaybackParametersChangedDelegate(PlaybackParameters parameters);

        public event PlaybackParametersChangedDelegate PlaybackParametersChanged;
        public void OnPlaybackParametersChanged(PlaybackParameters p0)
        {
            PlaybackParametersChanged?.Invoke(p0);
        }

        public delegate void PlayerErrorDelegate(ExoPlaybackException exception);

        public event PlayerErrorDelegate PlayerError;
        public void OnPlayerError(ExoPlaybackException p0)
        {
            PlayerError?.Invoke(p0);
        }

        public delegate void PlayerStateChangedDelegate(bool playWhenReady, PlaybackState state);

        public event PlayerStateChangedDelegate PlayerStateChanged;
        public void OnPlayerStateChanged(bool p0, int p1)
        {
            PlayerStateChanged?.Invoke(p0, (PlaybackState) p1);
        }

        public delegate void PositionDiscontinuityDelegate(DiscontinuityReason reason);

        public event PositionDiscontinuityDelegate PositionDiscontinuity;
        public void OnPositionDiscontinuity(int p0)
        {
            PositionDiscontinuity?.Invoke((DiscontinuityReason) p0);
        }

        public delegate void RepeatModeChangedDelegate(RepeatMode mode);

        public event RepeatModeChangedDelegate RepeatModeChanged;
        public void OnRepeatModeChanged(int p0)
        {
            RepeatModeChanged?.Invoke((RepeatMode) p0);
        }

        public delegate void SeekProcessedDelegate();

        public event SeekProcessedDelegate SeekProcessed;
        public void OnSeekProcessed()
        {
            SeekProcessed?.Invoke();
        }

        public delegate void ShuffleModeEnabledChangedDelegate(bool shuffleModeEnabled);

        public event ShuffleModeEnabledChangedDelegate ShuffleModeEnabledChanged;
        public void OnShuffleModeEnabledChanged(bool p0)
        {
            ShuffleModeEnabledChanged?.Invoke(p0);
        }

        public delegate void TimelineChangedDelegate(Timeline timeline, TimelineChangedReason reason);

        public event TimelineChangedDelegate TimelineChanged;
        public void OnTimelineChanged(Timeline p0, Object p1, int p2)
        {
            TimelineChanged?.Invoke(p0, (TimelineChangedReason) p2);
        }

        public delegate void TracksChangedDelegate(TrackGroup[] trackGroups, ITrackSelection[] trackSelections);

        public event TracksChangedDelegate TracksChanged;
        public void OnTracksChanged(TrackGroupArray p0, TrackSelectionArray p1)
        {
            var trackGroupArray = new TrackGroup[p0.Length];
            for (var i = 0; i < p0.Length; i++)
            {
                trackGroupArray[i] = p0.Get(i);
            }

            var trackSelectionArray = new ITrackSelection[p1.Length];
            for (var i = 0; i < p1.Length; i++)
            {
                trackSelectionArray[i] = p1.Get(i);
            }
            TracksChanged?.Invoke(trackGroupArray, trackSelectionArray);
        }
    }
}