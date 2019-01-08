using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SFX : MonoBehaviour {

    public AudioSource _audioSource;
    public AudioClip[] audioClipArray;

    public Text main;
    public static bool toast;
    public static bool text;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Back()
    {
        _audioSource.clip = audioClipArray[0];
        _audioSource.PlayOneShot(_audioSource.clip);
    }

    public void Select()
    {
        if (toast == false)
        {
            _audioSource.clip = audioClipArray[2];
            _audioSource.PlayOneShot(_audioSource.clip);
        }
    }

    public void Send()
    {
        _audioSource.clip = audioClipArray[3];
        _audioSource.PlayOneShot(_audioSource.clip);
    }

    public void Share()
    {
        if (toast == false)
        {
            _audioSource.clip = audioClipArray[4];
            _audioSource.PlayOneShot(_audioSource.clip);
        }
    }
}
