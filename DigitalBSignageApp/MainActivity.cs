using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Animation;
using Com.Airbnb.Lottie;
using Com.Airbnb.Lottie.Animation;
using Android.Graphics.Drawables;
using Android.Content;
using Android.Util;

namespace DigitalBSignageApp
{
    [Activity(Label = "DigitalB Signage App", MainLauncher = false, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        //int count = 1;

        protected override void OnCreate(Bundle savedInstanceState)
            {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);


            //lottieDottieDa = FindViewById<LottieAnimationView>(Resource.Id.animation_view);

        }
    }
}

