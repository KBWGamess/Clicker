using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class M : MonoBehaviour
{
    public Slider slider;
    public AudioMixer mixer;


    private void Start()
    {
        if (PlayerPrefs.HasKey("lastReality"))
        {
            LoadVolume();
        }
        else
        {
            SetVolume();
        }
    }
    public void SetVolume()
    {
        float volume = slider.value;
        mixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("lastReality", volume);
    }
    public void LoadVolume()
    {
        slider.value = PlayerPrefs.GetFloat("lastReality");

        SetVolume();
    }

}
