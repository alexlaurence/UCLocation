namespace AndroidGoodiesExamples
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using DeadMosquito.AndroidGoodies;
    using JetBrains.Annotations;
    using System;

    public class UCLFinder : MonoBehaviour
    {
        [SerializeField]
        private string bssid;

        public static float _lat;
        public static float _long;

        Dictionary<string, Info> hash = new Dictionary<string, Info>();

        struct Info
        {
            public float latitude;
            public float longitude;
            public string room;
        }

        private void Awake()
        {
            //To add to the hash
            hash.Add("c4:14:3c:be:52:91", new Info() { latitude = 51.52424f, longitude = -0.13326f, room = "Example Room" });
        }

        // Update is called once per frame
        void Update()
        {
            bssid = "" + AGNetwork.GetWifiConnectionInfo();

            Info findLocale;
            if (hash.TryGetValue(bssid, out findLocale))
            {
                _lat = findLocale.latitude;
                _long = findLocale.longitude;
            }
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