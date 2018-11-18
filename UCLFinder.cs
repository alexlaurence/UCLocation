namespace AndroidGoodiesExamples
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using DeadMosquito.AndroidGoodies;
    using JetBrains.Annotations;
    using System.IO;
    using System;

    public class UCLFinder : MonoBehaviour
    {

        /// <summary>
        /// Google api
        /// </summary>
        public RawImage img;

        string url;

        public float lat;
        public float lon;

        LocationInfo li;

        public int zoom = 20;
        public int mapWidth = 640;
        public int mapHeight = 640;

        public enum mapType { roadmap, satellite, hybrid, terrain }
        public mapType mapSelected;
        public int scale;

        IEnumerator Map()
        {
            url = "https://maps.googleapis.com/maps/api/staticmap?center=" + lat + "," + lon +
                "&zoom=" + zoom + "&size=" + mapWidth + "x" + mapHeight + "&scale=" + scale
                + "&maptype=" + mapSelected +
                "&markers=color:blue%7Clabel:S%7C40.702147,-74.015794&markers=color:green%7Clabel:G%7C40.711614,-74.012318&markers=color:red%7Clabel:C%7C40.718217,-73.998284&key=XXXX";
            WWW www = new WWW(url);
            yield return www;
            img.texture = www.texture;
            img.SetNativeSize();

        }
        //

        [SerializeField]
        private string bssid = "";

        public Text bssid_text;
        public Text lat_text;
        public Text long_text;
        public Text room_text;
        public Text list;


        Dictionary<string, Info> hash = new Dictionary<string, Info>();

        struct Info
        {
            public float latitude;
            public float longitude;
            public string room;
        }

        private void Start()
        {
            hash.Add("c4:14:3c:be:52:91", new Info() { latitude = 51.52424f, longitude = -0.13326f, room = "South Wing" });
            hash.Add("5c:50:15:91:38:ce", new Info() { latitude = 51.52429f, longitude = -0.13317f, room = "South Junction" });
            hash.Add("1c:de:a7:40:42:41", new Info() { latitude = 51.52436f, longitude = -0.13352f, room = "Main Quad" });
            hash.Add("80:e0:1d:d9:ec:8e", new Info() { latitude = 51.52486f, longitude = -0.13318f, room = "JBR" });

            lat = 0.0f;
            lon = 0.0f;

            StartCoroutine(Map());
        }

        // Update is called once per frame
        void Update()
        {
            bssid = "" + AGNetwork.GetWifiConnectionInfo();

            Info findLocale;
            if (hash.TryGetValue(bssid, out findLocale))
            {
                lat = findLocale.latitude;
                lon = findLocale.longitude;
            }

            //debugging
            bssid_text.text = bssid;
            lat_text.text = "" + findLocale.latitude;
            long_text.text = "" + findLocale.longitude;
            room_text.text = "" + findLocale.room;

            lat = findLocale.latitude;
            lon = findLocale.longitude;
        }

        public void FindMe()
        {
            //To lookup by key
            Info findLocale;
            if (hash.TryGetValue(bssid, out findLocale))
            {
                //share the information
                AGShare.ShareText("UCLocation", "Hey there, I'm in room: " + findLocale.room);
            }
        }

        public void OpenMaps()
        {

            //To lookup by key
            Info findLocale;
            if (hash.TryGetValue(bssid, out findLocale))
            {
                //open the map
                AGMaps.OpenMapLocation(findLocale.latitude, findLocale.longitude, 20);
            }

        }
    }
}