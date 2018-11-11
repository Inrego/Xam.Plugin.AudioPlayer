using System;
using System.Collections.Generic;
using System.Text;

namespace AudioPlayer.Abstractions
{
    public enum State
    {
        Uninitialized,
        Initialized,
        Loading,
        Playing,
        Paused,
        Stopped
    }
}
