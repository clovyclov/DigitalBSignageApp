﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace DigitalBSignageApp
{
    public class KeyboardView : LinearLayout
    {
        public KeyboardView(Context context) :
            base(context)
        {
            Initialize();
        }

        public KeyboardView(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize();
        }

        public KeyboardView(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
            Initialize();
        }

        void Initialize()
        {
        }
    }
}
