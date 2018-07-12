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
using Com.Google.Android.Exoplayer2;
using Com.Google.Android.Exoplayer2.Source;
using Com.Google.Android.Exoplayer2.Trackselection;
using Com.Google.Android.Exoplayer2.Upstream;
using Uri = Android.Net.Uri;

namespace AudioPlayer.Android
{
    public class AudioPlayer : IAudioPlayer
    {
        private SimpleExoPlayer _exoPlayer;
        private AudioPlayerEventListener _listener;
        public AudioPlayer()
        {
            _listener = new AudioPlayerEventListener();
            _exoPlayer = ExoPlayerFactory.NewSimpleInstance(Application.Context, new DefaultTrackSelector());
            _exoPlayer.AddListener(_listener);
        }
        public void StartPlaying(string url)
        {
            Uri uri = Uri.Parse(url);
            IDataSourceFactory dataSourceFactory = new DefaultDataSourceFactory(Application.Context, Application.Context.PackageName);
            IMediaSource mediaSource = new ExtractorMediaSource.Factory(dataSourceFactory).CreateMediaSource(uri);
            _exoPlayer.Prepare(mediaSource);
        }

        public void StartPlaying(Stream stream)
        {
            throw new NotImplementedException();
        }

        public bool IsPlaying { get; }
        public bool Paused { get; }

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