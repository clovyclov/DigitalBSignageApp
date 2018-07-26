
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

namespace DigitalBSignageApp
{
    [Activity(Label = "EnterWifiPassActivity", MainLauncher =false)]
    public class EnterWifiPassActivity : Activity
    {
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.EnterWifiPassLayout);

            TextView wifiName = FindViewById<TextView>(Resource.Id.wifiName);

            string passedWifiName = Intent.GetStringExtra("PassedWifiName");
            wifiName.Text = passedWifiName;

            //grid = FindViewById<GridLayout>(Resource.Id.gridLayoutKeys);
            //int glChildCount = grid.ChildCount;

            //for (int i = 0; i < glChildCount; i++){
            //    LinearLayout container = (LinearLayout)grid.GetChildAt(i);
            //    container.SetOnClickListener(new View.IOnClickListener()
            //    {
            //        public void onClick(View view){

            //        }
            //    });
            //}



        }


    }
}
