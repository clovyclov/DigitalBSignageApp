
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Net;
using Android.Net.Wifi;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Airbnb.Lottie;

namespace DigitalBSignageApp
{
    [Activity(Label = "SelectNetworkActivity", MainLauncher = false)]
    public class SelectNetworkActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SelectNetworkTypeLayout);

            //WifiManager wifiManager = 

            TextView connStatus = FindViewById<TextView>(Resource.Id.currentConnStatus);
            Button wifiBtn = FindViewById<Button>(Resource.Id.wifiBtn);
            Button lanBtn = FindViewById<Button>(Resource.Id.lanBtn);

            wifiBtn.Click += delegate {
                Toast.MakeText(this, "Picked", ToastLength.Long);
                StartActivity(typeof(ScanningWifiActivity));
            };

            LottieDrawable lottieDrawable = new LottieDrawable();

            if (IsOnline())
                connStatus.Text = "Connection Status: Currently online.";
            else
                connStatus.Text = "Connection Status: Not connected.";
        }

        public bool IsOnline()
        {
            var cm = (ConnectivityManager)GetSystemService(ConnectivityService);
            return cm.ActiveNetworkInfo == null ? false : cm.ActiveNetworkInfo.IsConnected;
        }
    }
}
