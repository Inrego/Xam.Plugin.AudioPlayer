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

namespace AudioPlayer.Android.Enums
{
    public enum TimelineChangedReason
    {
        Dynamic = 2,
        Prepared = 0,
        Reset = 1
    }
}