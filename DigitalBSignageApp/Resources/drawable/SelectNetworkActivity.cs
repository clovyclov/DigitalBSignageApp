
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Android.App;
using Android.Content;
using Android.InputMethodServices;
using Android.Net;
using Android.Net.Wifi;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Airbnb.Lottie;
using Java.Interop;

namespace DigitalBSignageApp
{

    [Activity(Label = "SelectNetworkActivity", MainLauncher = true)]
    public class SelectNetworkActivity : Activity
    {
        TextView connStatus;

        [Export("ChangeText")]
        public void ChangeText(View view)
        {
            Button clicked_button = (Button)view;
            connStatus.Text += clicked_button.Text;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SelectNetworkTypeLayout);

            RelativeLayout b_notch = (RelativeLayout)FindViewById(Resource.Id.bottomNotch);
            LinearLayout mainBox = (LinearLayout)FindViewById(Resource.Id.mainBox);

            RelativeLayout bottomNotchLower = (RelativeLayout)FindViewById(Resource.Id.bottomNotchLower);
            Timer timer = new Timer(900);

            RelativeLayout.LayoutParams lp = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, 0);
            lp.AddRule(LayoutRules.AlignParentBottom);
            b_notch.Click += delegate {
                if(bottomNotchLower.Height == 600){
                    lp = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, 0);
                    lp.AddRule(LayoutRules.AlignParentBottom);
                    bottomNotchLower.LayoutParameters = lp;
                }else {
                    for (var i = 0; i < 600; i++){
                        lp = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, i);
                        lp.AddRule(LayoutRules.AlignParentBottom);
                        bottomNotchLower.LayoutParameters = lp;

                        timer.Start();
                    }
                    timer.Stop();

                    //lp = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, 600);
                    //lp.AddRule(LayoutRules.AlignParentBottom);
                    //bottomNotchLower.LayoutParameters = lp;


                }
            };

            //Button b_notchView = FindViewById<Button>(Resource.Id.bottomNotchView);



            //Keyboard keyboard = new Keyboard(this, Resource.Layout.abc_action_bar_title_item);
            //KeyboardView kbd = FindViewById<KeyboardView>(Resource.Id.keyboardView);

            connStatus = FindViewById<TextView>(Resource.Id.currentConnStatus);
            Button wifiBtn = FindViewById<Button>(Resource.Id.wifiBtn);
            Button lanBtn = FindViewById<Button>(Resource.Id.lanBtn);

            wifiBtn.Click += delegate {
                Toast.MakeText(this, "Picked", ToastLength.Long);
                StartActivity(typeof(ScanningWifiActivity));
            };

            LottieDrawable lottieDrawable = new LottieDrawable();

            if (IsOnline()){

                ConnectivityManager connectivityManager = (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);
                var netInfo = connectivityManager.ActiveNetworkInfo;

                //netInfo.IsConnected && 
                if (netInfo.Type == ConnectivityType.Wifi)
                {
                    connStatus.Text = "Connection Status: Connected To Wifi.";
                }
                else if (netInfo.Type == ConnectivityType.Ethernet)
                {
                    connStatus.Text = "Connection Status: Connected To Ethernet.";
                }

            }else {
                connStatus.Text = "Connection Status: Not connected.";
            }
        }

        public bool IsOnline()
        {
            var cm = (ConnectivityManager)GetSystemService(ConnectivityService);
            return cm.ActiveNetworkInfo == null ? false : cm.ActiveNetworkInfo.IsConnected;
        }
    }
}
