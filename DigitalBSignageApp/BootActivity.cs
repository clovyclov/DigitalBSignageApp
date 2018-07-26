
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace DigitalBSignageApp
{
    [Activity(Label = "BootActivity", MainLauncher = true)]
    public class BootActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.BootLayout);

            new Thread(() =>
            {
                Thread.Sleep(7000);
                StartActivity(typeof(SelectNetworkActivity));

                this.Finish();
            }).Start();
        }
    }
}
