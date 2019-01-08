using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Viewer : MonoBehaviour
{
    public Text colBSSID;
    public Text colROOM;
    public Text colLAT;
    public Text colLON;

    public void View()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("data");
        for (var i = 0; i < data.Count; i++)
        {
            colBSSID.text += Convert.ToString(data[i]["bssid"]) + Environment.NewLine;
            colROOM.text += Convert.ToString(data[i]["room"]) + Environment.NewLine;
            colLAT.text += Convert.ToString(data[i]["lat"]) + Environment.NewLine;
            colLON.text += Convert.ToString(data[i]["lon"]) + Environment.NewLine;
        }
    }
    
    public void Send()
    {
        StartCoroutine(Share());
    }

    private IEnumerator Share()
    {
        string filePath = getPath();
        yield return new WaitForEndOfFrame();
        new NativeShare().AddFile(filePath).SetSubject("UCLocation").SetText("Here's my WiFi Data!").Share();
    }

    public string getPath()
    {
        #if UNITY_EDITOR
        return Application.dataPath + "/Resources/" + "data.csv";
        #elif UNITY_ANDROID
        return Application.persistentDataPath+"data.csv";
        #elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+"data.csv";
        #else
        return Application.dataPath +"/"+"data.csv";
        #endif
    }
}
