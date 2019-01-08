using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using DeadMosquito.AndroidGoodies;
using System.Collections;

public class Writer : MonoBehaviour {

    public InputField bssid;
    public InputField room;
    public InputField lat;
    public InputField lon;

    public void Save()
    {
        File.AppendAllText(@"Assets/Resources/data.csv", bssid.text + "," + room.text + "," + lat.text + "," + lon.text + Environment.NewLine);
        AGUIMisc.ShowToast("Saved.");
        bssid.text = "";
        room.text = "";
        lat.text = "";
        lon.text = "";
    }

    public void Auto()
    {
        StartCoroutine(Start());
    }

    IEnumerator Start()
    {
        bssid.text = Convert.ToString(AGNetwork.GetWifiConnectionInfo());

        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield break;

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            lat.text = Convert.ToString(Input.location.lastData.latitude);
            lon.text = Convert.ToString(Input.location.lastData.longitude);
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }
}
