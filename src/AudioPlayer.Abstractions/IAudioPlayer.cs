using System;
using System.ComponentModel;
using System.IO;

namespace AudioPlayer.Abstractions
{
    public interface IAudioPlayer : IDisposable, INotifyPropertyChanged
    {
        event EventHandler<State> OnStateChanged;
        void StartPlaying(string url);
        void StartPlaying(Stream stream);
        State State { get; }
    }
}
