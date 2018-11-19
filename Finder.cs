using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DeadMosquito.AndroidGoodies;
using System;

public class Finder : MonoBehaviour
{
    [SerializeField]
    public string bssid;
    public Text roomName;

    //CSV file must be in the /Assets/Resources folder
    private string filename = "data";

    // Update is called once per frame
    void Update()
    {
        //Initialise the list of dictionaries from CSV file
        List<Dictionary<string, object>> data = CSVReader.Read(filename);

        //Put the BSSID inside a string
        bssid = "" + AGNetwork.GetWifiConnectionInfo();

        //Search the entire list
        for (var i = 0; i < data.Count; i++)
        {
            //Check for a BSSID match
            if (data[i].ContainsValue(bssid))
            {
                //Display the matched room name
                roomName.text = Convert.ToString(data[i]["room"]);
            }
            else
            {
                roomName.text = "Searching...";
            }
        }
    }

    public void Share()
    {
        //Initialise the list of dictionaries from CSV file
        List<Dictionary<string, object>> data = CSVReader.Read(filename);

        for (var i = 0; i < data.Count; i++)
        {
            //Check for a BSSID match
            if (data[i].ContainsValue(bssid))
            {
                //Open Android Share dialogue, and share the matched room name
                AGShare.ShareText("UCLocation", "Hey there, I'm in " + Convert.ToString(data[i]["room"]));
            }
        }
    }

    public void OpenMaps()
    {
        //Initialise the list of dictionaries from CSV file
        List<Dictionary<string, object>> data = CSVReader.Read(filename);

        //Search the entire list
        for (var i = 0; i < data.Count; i++)
        {
            //Check for a BSSID match
            if (data[i].ContainsValue(bssid))
            {
                //Open the map location based on latitude and longitude data
                AGMaps.OpenMapLocation(Convert.ToInt32(data[i]["lat"]), Convert.ToInt32(data[i]["lon"]), 20);
            }
        }
    }
}