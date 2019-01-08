using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DeadMosquito.AndroidGoodies;
using System;
using System.Collections;

public class Finder : MonoBehaviour
{
    public string bssid;

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
            bssid = Convert.ToString(AGNetwork.GetWifiConnectionInfo());

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
                    //Display a placeholder text
                    roomName.text = "Searching...";

                    //Couldn't find a known MAC address after 10s
                    if (roomName.text == "Searching...")
                    {
                        keepPlaying = true;
                        roomName.text = "Unknown location";
                        StartCoroutine(sfxError(0));
                    }
                }
            }
        }
        else
        {
            keepPlaying = true;
            roomName.text = "No WiFi Connection";
            StartCoroutine(sfxWiFi(0));
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
            AGUIMisc.ShowToast("Please connect to WiFi.",AGUIMisc.ToastLength.Long);
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
            AGUIMisc.ShowToast("Unable to detect MAC address.", AGUIMisc.ToastLength.Long);
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
            if (data[i].ContainsValue(bssid))
            {
                //Open Android Share dialogue, and share the matched room name
                AGShare.ShareText("UCLocation", "Hey there, I'm in " + Convert.ToString(data[i]["room"]));
            }
            else 
            {
                AGUIMisc.ShowToast("Unable to detect MAC address.", AGUIMisc.ToastLength.Long);
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
            if (data[i].ContainsValue(bssid))
            {
                //Open the map location based on latitude and longitude data
                AGMaps.OpenMapLocation(Convert.ToInt32(data[i]["lat"]), Convert.ToInt32(data[i]["lon"]), 20);
            }
            else 
            {
                AGUIMisc.ShowToast("Unable to detect MAC address.", AGUIMisc.ToastLength.Long);
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
}