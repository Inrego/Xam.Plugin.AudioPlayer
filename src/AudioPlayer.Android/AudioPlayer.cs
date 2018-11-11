using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Com.Google.Android.Exoplayer2.Upstream;
using Uri = Android.Net.Uri;

namespace AudioPlayer.Android
{
    public class AudioPlayer : IAudioPlayer
    {
        public event EventHandler<State> OnStateChanged; 
        private readonly SimpleExoPlayer _exoPlayer;
        private readonly AudioPlayerEventListener _listener;
        public AudioPlayer()
        {
            _listener = new AudioPlayerEventListener();
            _listener.PlayerStateChanged += _listener_PlayerStateChanged;
            _listener.LoadingChanged += _listener_LoadingChanged;
            
            _exoPlayer = ExoPlayerFactory.NewSimpleInstance(Application.Context, new DefaultTrackSelector());
            _exoPlayer.AddListener(_listener);
        }

        private void _listener_LoadingChanged(bool loading)
        {
            if (loading)
                State = State.Loading;
        }

        private void _listener_PlayerStateChanged(bool playWhenReady, Enums.PlaybackState state)
        {
            switch (state)
            {
                case PlaybackState.Buffering:
                    State = State.Loading;
                    break;
                case PlaybackState.Ended:
                case PlaybackState.Idle:
                case PlaybackState.Ready:
                    State = State.Stopped;
                    break;
            }
        }

        public void StartPlaying(string url)
        {
            State = State.Loading;
            Uri uri = Uri.Parse(url);
            IDataSourceFactory dataSourceFactory = new DefaultDataSourceFactory(Application.Context, Application.Context.PackageName);
            IMediaSource mediaSource = new ExtractorMediaSource.Factory(dataSourceFactory).CreateMediaSource(uri);
            _exoPlayer.Prepare(mediaSource);
        }

        public void StartPlaying(Stream stream)
        {
            throw new NotImplementedException();
        }

        private State _state = State.Initialized;

        public State State
        {
            get => _state;
            set
            {
                var newVal = _state != value;
                _state = value;
                if (newVal)
                {
                    OnPropertyChanged();
                    OnStateChanged?.Invoke(this, value);
                }
            }
        }
        
        public void Dispose()
        {
            _exoPlayer?.Release();
            _exoPlayer?.Dispose();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}