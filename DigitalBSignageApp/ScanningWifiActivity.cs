
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Net.Wifi;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace DigitalBSignageApp
{
    [Activity(Label = "ScanningWifiActivity", MainLauncher = false)]
    public class ScanningWifiActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ScanningWifiLayout);

            new Thread(() =>
            {
                WifiManager wifiManager = Application.Context.GetSystemService(Context.WifiService) as WifiManager;
                wifiManager.StartScan();
                IList<ScanResult> scanResults = wifiManager.ScanResults;

                var networks = new List<String>();

                Thread.Sleep(5000);

                foreach (var network in scanResults)
                {
                    networks.Add(network.Ssid);
                }


                Intent intent = new Intent(this, typeof(SelectWifiActivity));
                intent.PutStringArrayListExtra("wifiScanResults", networks);

                StartActivity(intent);
                this.Finish();
            }).Start();
        }
    }
}
