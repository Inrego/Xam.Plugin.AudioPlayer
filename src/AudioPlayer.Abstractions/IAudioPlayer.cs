using System;
using System.ComponentModel;
using System.IO;

namespace AudioPlayer.Abstractions
{
    public interface IAudioPlayer : IDisposable, INotifyPropertyChanged
    {
        void StartPlaying(string url);
        void StartPlaying(Stream stream);
        bool IsPlaying { get; }
        bool Paused { get; }
    }
}
