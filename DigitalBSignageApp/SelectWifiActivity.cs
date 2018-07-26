
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Net.Wifi;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Java.Interop;
using Java.Lang;

namespace DigitalBSignageApp
{



    [Activity(Label = "SelectWifiActivity", MainLauncher = false)]
    public class SelectWifiActivity : Activity
    {

        /// <summary>
        /// TO SCOTT: Remember to bring in the RecyclerView package from Nuget!!!
        /// </summary>

        WifiSSIDList WifiSSIDList;
        Android.Support.V7.Widget.RecyclerView wifiRecyclerView;
        RecyclerView.LayoutManager wifiLayoutManager;
        WifiListAdapter wifiListAdapter;
        WifiManager wifiManager;
        TextView wifiNetworkName;
        Button enterWifiManually;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SelectWifiLayout);

            //wifiManager = Application.Context.GetSystemService(Context.WifiService) as WifiManager;
            //wifiManager.StartScan();
            //IList<ScanResult> scanResults = wifiManager.ScanResults;



            wifiNetworkName = FindViewById<TextView>(Resource.Id.wifiNetworkName);

            var scannedResults = Intent.GetStringArrayListExtra("wifiScanResults");
            var networks = new List<System.String>();

            foreach (var result in scannedResults)
            {
                WifiGridAdapter.testStrings.Add(result);
            }

            GridView gridView = FindViewById<GridView>(Resource.Id.wifiGrid);
            gridView.Adapter = new WifiGridAdapter(this);

            enterWifiManually = FindViewById<Button>(Resource.Id.btnEnterManually);
            enterWifiManually.Click += delegate {

                wifiNetworkName.Text = "Manual Wifi Clicked";
            };

            //WifiSSIDList = new WifiSSIDList();

            //wifiRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerWifi);


            ////Set layout manager
            //wifiLayoutManager = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
            //wifiRecyclerView.SetLayoutManager(wifiLayoutManager);

            ////Set adapter
            //wifiListAdapter = new WifiListAdapter(networks);

            //wifiListAdapter.ItemClick += OnItemClick;

            //wifiRecyclerView.SetAdapter(wifiListAdapter);


            //int counter = 0;
            //for (var i = 0; i < wifiRecyclerView.ChildCount; i++)
            //{
            //    counter++;
            //}


        }


        void OnItemClick(object sender, int position)
        {
            //var view = (TextView)sender;
            //Button viewBtn = (Button)sender;
            //Toast.MakeText(this, $"You clicked number {position}. Also {viewBtn.Texthang}", ToastLength.Short).Show();
            StartActivity(typeof(EnterWifiPassActivity));
            //wifiNetworkName.Text = "Worked!";
            int wifiNum = position + 1;

        }
    }

    public class WifiGridAdapter : BaseAdapter
    {
        public static List<System.String> testStrings = new List<System.String>();
        public Context Context { get; set; }

        public WifiGridAdapter(Context context)
        {
            Context = context;
        }

        public override int Count
        {
            get { return testStrings.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Button textView;

            if (convertView == null)
            {  // if it's not recycled, initialize some attributes
                textView = new Button(Application.Context);
                textView.LayoutParameters = new GridView.LayoutParams(300, 200);
                //textView.SetPadding(2, 2, 2, 2);
                textView.Background = ContextCompat.GetDrawable(Application.Context, Resource.Drawable.wifi_buttons);
                textView.Click += gridview_ItemClick;
            }
            else
            {
                textView = (Button)convertView;
            }

            textView.Text = testStrings[position];
            return textView;
        }

        private void gridview_ItemClick(object sender, EventArgs e)
        {
            Button theSender = sender as Button;
            View view = new View(Context);
            Toast.MakeText(Context, $"Clicked Button {theSender.Text}", ToastLength.Short).Show();


            Intent intent = new Intent(Context, typeof(EnterWifiPassActivity));
            intent.SetFlags(ActivityFlags.NewTask);
            intent.PutExtra("PassedWifiName", theSender.Text);


            Application.Context.StartActivity(intent);
        }


    }


    //----------------------------------------------------------------------
    // VIEW HOLDER

    // Implement the ViewHolder pattern: each ViewHolder holds references
    // to the UI components (ImageView and TextView) within the CardView 
    // that is displayed in a row of the RecyclerView:
    public class WifiListHolder : RecyclerView.ViewHolder
    {
        public TextView WifiSSID { get; private set; }

        public WifiListHolder(View itemView, Action<int> listener) : base(itemView)
        {
            WifiSSID = itemView.FindViewById<Button>(Resource.Id.wifiSSIDTxt);

            itemView.Click += (sender, e) => listener(base.LayoutPosition);
        }
    }


    //----------------------------------------------------------------------
    // ADAPTER

    // Adapter to connect the data set (photo album) to the RecyclerView: 
    public class WifiListAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;

        //Underlying data set
        public List<string> WifiScanResults { get; set; }

        public WifiListAdapter(List<string> wifiSSIDList)
        {
            WifiScanResults = wifiSSIDList;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.RecyclerWifiLayout, parent, false);

            Button ssid = itemView.FindViewById<Button>(Resource.Id.wifiSSIDTxt);

            WifiListHolder vh = new WifiListHolder(itemView, OnClick);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            WifiListHolder vh = holder as WifiListHolder;
            vh.WifiSSID.Text = WifiScanResults[position];
        }

        public override int ItemCount
        {
            get { return WifiScanResults.Count; }
        }

        void OnClick(int position)
        {
            if (ItemClick != null)
                ItemClick(this, position);
        }
    }

}
