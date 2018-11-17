#if UNITY_ANDROID
namespace AndroidGoodiesExamples
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using DeadMosquito.AndroidGoodies;


    public class UCLFinder : MonoBehaviour
    {
        [SerializeField]
        private Text placeholder;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            placeholder.text = "BSSID: " + AGNetwork.GetWifiConnectionInfo();
        }
    }
}
#endif
