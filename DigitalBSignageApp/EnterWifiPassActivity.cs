
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Support.V7.GridLayout;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Views.InputMethods;
using System.Threading;
using Android.Net.Wifi;
using Android.Net;
using Java.Interop;

namespace DigitalBSignageApp
{
    [Activity(Label = "EnterWifiPassActivity", MainLauncher =false)]
    public class EnterWifiPassActivity : Activity
    {

        public string EnteredWifiName { get; set; }
        public string EnteredWifiPass { get; set; }
        TextView wifiName;
        TextView enteredWifiPassword;

        LinearLayout keyboardKeysMain;
        LinearLayout keyboardKeysMainLowered;
        LinearLayout keyboardKeysSecondary;
        Button CapsButton;
        Button CapsButtonSecondary;

        [Export("ChangeText")]
        public void ChangeText(View view)
        {
            Button clicked_button = (Button)view;
            enteredWifiPassword.Text += clicked_button.Text;
        }

        [Export("BackBtnPressed")]
        public void BackBtnPressed(View view)
        {
            if (!String.IsNullOrEmpty(enteredWifiPassword.Text))
            {
                enteredWifiPassword.Text = enteredWifiPassword.Text.Remove(enteredWifiPassword.Text.Length - 1);
            }
        }

        [Export("SpaceBtn")]
        public void SpaceBtn(View view)
        {
            enteredWifiPassword.Text = enteredWifiPassword.Text + " ";
        }

        [Export("CapsBtn")]
        public void CapsBtn(View view)
        {
            if (keyboardKeysMainLowered.Visibility == ViewStates.Visible)
            {

                keyboardKeysMainLowered.Visibility = ViewStates.Gone;
                keyboardKeysMain.Visibility = ViewStates.Visible;
                CapsButton.Focusable = true;
                CapsButton.FocusableInTouchMode = true;
                CapsButton.RequestFocus();

            }
            else
            {

                keyboardKeysMainLowered.Visibility = ViewStates.Visible;
                CapsButtonSecondary.Focusable = true;
                CapsButtonSecondary.FocusableInTouchMode = true;
                CapsButtonSecondary.RequestFocus();
                keyboardKeysMain.Visibility = ViewStates.Gone;

            }
        }

        [Export("ShiftBtn")]
        public void ShiftBtn(View view)
        {
            if(keyboardKeysMain.Visibility == ViewStates.Visible){
                
                keyboardKeysMain.Visibility = ViewStates.Invisible;
                keyboardKeysSecondary.Visibility = ViewStates.Visible;

            }else {
                
                keyboardKeysMain.Visibility = ViewStates.Visible;
                keyboardKeysSecondary.Visibility = ViewStates.Invisible;

            }
        }


        protected override void OnCreate(Bundle savedInstanceState)
         {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.EnterWifiPassLayout);

            keyboardKeysMain = FindViewById<LinearLayout>(Resource.Id.keyboardKeysMain);
            keyboardKeysMainLowered = FindViewById<LinearLayout>(Resource.Id.keyboardKeysMainLowered);
            keyboardKeysSecondary = FindViewById<LinearLayout>(Resource.Id.keyboardKeysSecondary);
            CapsButton = FindViewById<Button>(Resource.Id.CapsButtonMain);
            CapsButtonSecondary = FindViewById<Button>(Resource.Id.CapsButtonSecondary);

            //int keyCount = keyboardKeysMain.ChildCount;

            //Toast.MakeText(this, keyInd.ToString(), ToastLength.Short);

            wifiName = FindViewById<TextView>(Resource.Id.wifiName);
            enteredWifiPassword = FindViewById<TextView>(Resource.Id.enterWifiPassword);

            string passedWifiName = Intent.GetStringExtra("PassedWifiName");
            wifiName.Text = passedWifiName;

            Button connBtn = FindViewById<Button>(Resource.Id.connBtn);

            connBtn.Click += delegate {

                Thread thread = new Thread(() =>
                {

                    var wifiConfig = new WifiConfiguration();
                    wifiConfig.Ssid = string.Format("\"{0}\"", wifiName.Text);
                    wifiConfig.PreSharedKey = string.Format("\"{0}\"", enteredWifiPassword.Text);

                    using (WifiManager wifiManager2 = (WifiManager)Application.Context.GetSystemService(Context.WifiService))
                    {
                        int netId = wifiManager2.AddNetwork(wifiConfig);
                        wifiManager2.Disconnect();
                        wifiManager2.EnableNetwork(netId, true);
                        wifiManager2.Reconnect();
                    }

                    Thread.Sleep(7000);

                    ConnectivityManager connectivityManager = (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);
                    var netInfo = connectivityManager.ActiveNetworkInfo;

                    Toast.MakeText(this, "Scanning Wifi Networks...", ToastLength.Long).Show();

                });
                thread.Start();
            };

            var _broadcastReceiver = new NetworkStatusBroadcastReceiver();
            _broadcastReceiver.ConnectionStatusChanged += OnNetworkStatusChanged;
            Application.Context.RegisterReceiver(_broadcastReceiver, new IntentFilter(ConnectivityManager.ConnectivityAction));
        }

        public bool IsOnline()
        {
            var cm = (ConnectivityManager)GetSystemService(ConnectivityService);
            return cm.ActiveNetworkInfo == null ? false : cm.ActiveNetworkInfo.IsConnected;
        }

        private void OnNetworkStatusChanged(object sender, EventArgs e)
        {
            if (IsOnline())
            {
                Toast.MakeText(this, "You are connected to Wifi", ToastLength.Short).Show();
            }
        }
    }

    [BroadcastReceiver()]
    public class NetworkStatusBroadcastReceiver : BroadcastReceiver
    {
        public event EventHandler ConnectionStatusChanged;

        public override void OnReceive(Context context, Intent intent)
        {
            if (ConnectionStatusChanged != null)
                ConnectionStatusChanged(this, EventArgs.Empty);
        }
    }
}
