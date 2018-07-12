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
    public enum DiscontinuityReason
    {
        AdInsertion = 3,
        Internal = 4,
        PeriodTransition = 0,
        Seek = 1,
        SeekAdjustment = 2
    }
}