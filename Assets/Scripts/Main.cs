using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DeadMosquito.AndroidGoodies;
using System;
using System.Collections;
using System.IO;
using System.Linq;

public class Main : MonoBehaviour
{
    #region WRITER
    public InputField bssid;
    public InputField room;
    public InputField lat;
    public InputField lon;

    public Text colBSSID;
    public Text colROOM;
    public Text colLAT;
    public Text colLON;

    public string outputData;

    public static string Path
    {
        get
        {
            switch (Application.platform)
            {
                case RuntimePlatform.IPhonePlayer:
                    return System.IO.Path.Combine(Application.persistentDataPath, "data.txt");

                case RuntimePlatform.Android:
                    return System.IO.Path.Combine(Application.temporaryCachePath, "data.txt");

                default:
                    return System.IO.Path.Combine(Directory.GetParent(Application.dataPath).FullName, "data.txt");
            }
        }
    }

    private void Start()
    {

        //Initialise the list of dictionaries from CSV file
        List<Dictionary<string, object>> data = CSVReader.Read("data");

        // Output the dictionary of dictionaries
        for (int i = 0; i < data.Count; i++)
        {
            var lines = data[i].Select(kvp => kvp.Key + ": " + kvp.Value.ToString());
            Debug.Log(string.Join(Environment.NewLine, lines));
        }
    }

    // Initialise entries dictionary
    public Dictionary<string, object> entry = new Dictionary<string, object>();

    public int counter = 1;

    public void Save()
    {
        // Initialise List of Dictionary
        List<Dictionary<string, object>> data = CSVReader.Read("data");

        counter++;

        Debug.Log("Step 0");

        // CANNOT DO THIS STEP TWICE SINCE DICTIONARY DOES NOT ALLOW DUPLICATE KEYS

        // Add values to Dictionary
        entry.Add("bssid" , bssid.text);
        entry.Add("room", room.text);
        entry.Add("lat", lat.text);
        entry.Add("lon", lon.text);

        Debug.Log("Step 1");

        //Maybe add a blank data.Add("") to account for index mismatch

        // Add dictionary entry to csv
        data.Add(entry);

        Debug.Log("Step 2");

        // Output to log what we just saved
        var lines = data[data.Count - 1].Select(kvp => kvp.Key + ": " + kvp.Value.ToString());
        Debug.Log(string.Join(Environment.NewLine, lines));

        Debug.Log("Step 3");

        // Confirm
        AGUIMisc.ShowToast("Saved.");

        // Clear fields
        bssid.text = "";
        room.text = "";
        lat.text = "";
        lon.text = "";

        //populate viewer with csv contents
        colBSSID.text += Convert.ToString(entry["bssid"]) + Environment.NewLine;
        colROOM.text += Convert.ToString(entry["room"]) + Environment.NewLine;
        colLAT.text += Convert.ToString(entry["lat"]) + Environment.NewLine;
        colLON.text += Convert.ToString(entry["lon"]) + Environment.NewLine;

        // Build a string for sharing
        string str = string.Empty;

        for (int i = 0; i < data.Count; i++)
        {
            str += data[i]["bssid"] + ", " + data[i]["room"] + ", " + data[i]["lat"] + ", " + data[i]["lon"] + Environment.NewLine;
        }

        // Fill string with the updated values
        outputData = str;
    }

    //public void AddRecord(string bssid, string room, string lat, string lon)
    //{
    //    //ask for external write permission
    //    AndroidRuntimePermissions.Permission result = AndroidRuntimePermissions.RequestPermission("android.permission.WRITE_EXTERNAL_STORAGE");

    //    //check if granted
    //    if (result == AndroidRuntimePermissions.Permission.Granted)
    //    {
    //        try
    //        {
    //            using (StreamWriter file = new StreamWriter(Path, true))
    //            {
    //                file.WriteLine(bssid + ", " + room + ", " + lat + ", " + lon);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new ApplicationException("This program did an oopsie :", ex);
    //        }
    //    }
    //}

    public void Auto()
    {
        AndroidRuntimePermissions.Permission result = AndroidRuntimePermissions.RequestPermission("android.permission.ACCESS_FINE_LOCATION");
        if (result == AndroidRuntimePermissions.Permission.Granted)
            StartCoroutine(StartProcess());
        else
            Debug.Log("Permission state: " + result);
    }

    IEnumerator StartProcess()
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
    #endregion

    #region READER

    public void View()
    {

    }

    private void Awake()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("data");

        // empty the strings
        colBSSID.text = "";
        colROOM.text = "";
        colLAT.text = "";
        colLON.text = "";

        // fill or refill the strings
        for (var i = 0; i < data.Count; i++)
        {
            //populate viewer with csv contents
            colBSSID.text += Convert.ToString(data[i]["bssid"]) + Environment.NewLine;
            colROOM.text += Convert.ToString(data[i]["room"]) + Environment.NewLine;
            colLAT.text += Convert.ToString(data[i]["lat"]) + Environment.NewLine;
            colLON.text += Convert.ToString(data[i]["lon"]) + Environment.NewLine;
        }
    }



    public void Send()
    {
        new NativeShare().SetText(outputData).Share();
    }

    #endregion

    #region FINDER
    public string bssid_str;

    public Text roomName;

    public Animator animConfig;
    public Animator animConfig2;

    public GameObject roomTextObj;
    public GameObject configPanel;
    public GameObject configPanel2;
    public GameObject addPanel;
    public GameObject viewPanel;

    public AudioSource _audioSource;
    public AudioClip[] audioClipArray;

    bool keepPlaying = true;

    //CSV file must be in the /Assets/Resources folder
    private string filename = "data";

    // Update is called once per frame
    void Update()
    {
        //Initialise the list of dictionaries from CSV file
        List<Dictionary<string, object>> data = CSVReader.Read(filename);

        if (AGNetwork.IsWifiConnected() == true)
        {
            //Put the BSSID inside a string
            bssid_str = Convert.ToString(AGNetwork.GetWifiConnectionInfo());

            // Search through the dictionary
            for (var i = 0; i < data.Count; i++)
            {
                //Check for a BSSID match
                if (entry.ContainsValue(bssid_str))
                {
                    //Display the matched room name
                    roomName.text = Convert.ToString(data[i]["room"]);
                    //roomName.text = "Matched!";
                }
                else
                {
                    roomName.text = "Unknown Location";
                }
            }
        }
        else
        {
            roomName.text = "No WiFi Connection";
        }
    }

    IEnumerator sfxWiFi(float secondsBetweenAudioShot)
    {
        // Repeat until keepPlaying == false or this GameObject is disabled/destroyed.
        while (keepPlaying)
        {
            // Put this coroutine to sleep until the next time the audio plays
            yield return new WaitForSeconds(secondsBetweenAudioShot);

            if (!_audioSource.isPlaying)
            {
                _audioSource.clip = audioClipArray[1];
                _audioSource.PlayOneShot(_audioSource.clip);
            }
            //AGUIMisc.ShowToast("Please connect to WiFi.",AGUIMisc.ToastLength.Short);
            keepPlaying = false;
        }
    }

    IEnumerator sfxError(float secondsBetweenAudioShot)
    {
        // Repeat until keepPlaying == false or this GameObject is disabled/destroyed.
        while (keepPlaying)
        {
            // Put this coroutine to sleep until the next time the audio plays
            yield return new WaitForSeconds(secondsBetweenAudioShot);

            if (!_audioSource.isPlaying)
            {
                _audioSource.clip = audioClipArray[2];
                _audioSource.PlayOneShot(_audioSource.clip);
            }
            //AGUIMisc.ShowToast("Unable to detect MAC address.", AGUIMisc.ToastLength.Short);
            keepPlaying = false;
        }
    }

    public void Share()
    {
        //Initialise the list of dictionaries from CSV file
        List<Dictionary<string, object>> data = CSVReader.Read(filename);

        for (var i = 0; i < data.Count; i++)
        {
            //Check for a BSSID match
            if (entry.ContainsValue(bssid_str))
            {
                //Open Android Share dialogue, and share the matched room name
                AGShare.ShareText("UCLocation", "Hey there, I'm in " + Convert.ToString(data[i]["room"]));
            }
            else
            {
                //AGUIMisc.ShowToast("Unable to detect MAC address.", AGUIMisc.ToastLength.Short);
                SFX.toast = true;
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
            if (entry.ContainsValue(bssid_str))
            {
                //Open the map location based on latitude and longitude data
                AGMaps.OpenMapLocation(Convert.ToInt32(data[i]["lat"]), Convert.ToInt32(data[i]["lon"]), 20);
            }
            else
            {
                AGUIMisc.ShowToast("Unable to detect MAC address.", AGUIMisc.ToastLength.Short);
                SFX.toast = true;
            }
        }
    }

    //Misc UI Buttons
    public void openConfig()
    {
        configPanel.SetActive(true);
        animConfig.SetBool("openMe", true);
        animConfig.SetBool("closeMe", false);
        roomTextObj.SetActive(false);
        configPanel2.SetActive(false);
    }

    public void closeConfig()
    {
        animConfig.SetBool("openMe", false);
        animConfig.SetBool("closeMe", true);
        roomTextObj.SetActive(true);
    }

    public void backConfig()
    {
        addPanel.SetActive(false);
        viewPanel.SetActive(false);
        configPanel2.SetActive(true);
        configPanel.SetActive(false);
    }

    public void closeConfig2()
    {
        animConfig2.SetBool("closeMe", true);
        roomTextObj.SetActive(true);
    }

    public void openAdd()
    {
        configPanel.SetActive(false);
        configPanel2.SetActive(false);
        addPanel.SetActive(true);
    }

    public void openView()
    {
        configPanel.SetActive(false);
        configPanel2.SetActive(false);
        viewPanel.SetActive(true);
    }
    #endregion
}
