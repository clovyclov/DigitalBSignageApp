using System;
using System.Collections.Generic;

namespace DigitalBSignageApp
{
    public class WifiSSIDList
    {
        public string SSID { get; set; }
        public string Strength { get; set; }
        public List<WifiSSIDList> ScanList { get; set; }

        public static List<WifiSSIDList> ScanResults = new List<WifiSSIDList>(){
            new WifiSSIDList(){SSID = "PrivateWifi", Strength = "Good"},
            new WifiSSIDList(){SSID = "BHN Secure", Strength = "Good"},
            new WifiSSIDList(){SSID = "Bright House Networks", Strength = "Good"},
            new WifiSSIDList(){SSID = "Cable Wifi", Strength = "Good"},
            new WifiSSIDList(){SSID = "l-Tech", Strength = "Good"},
            new WifiSSIDList(){SSID = "l-Tech1", Strength = "Good"},
            new WifiSSIDList(){SSID = "DIRECT-4w-BRAVIA", Strength = "Good"},
        };

        //Allows you to index wifi list
        public WifiSSIDList this[int i]
        {
            get { return ScanResults[i]; }
        }


        public int NumWifiList
        {
            get
            {
                return ScanResults.Count;
            }
        }

        public WifiSSIDList()
        {
            ScanList = ScanResults;
        }


    }
}
