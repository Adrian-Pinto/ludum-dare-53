using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] Slider soundSlider;
    [SerializeField] AudioMixer masterMixer;

    void Start()
    {
        setVolume(PlayerPrefs.GetFloat("SavedMasterVolume", 100));
    }

    public void setVolume(float value)
    {
        if (value < 1)
        {
            value = .001f;
        }

        RefreshSlider(value);
        PlayerPrefs.SetFloat("SavedMasterVolume", value);
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(value / 100) * 20f);
    }

    public void SetVolumeFromSlider()
    {
        setVolume(soundSlider.value);
    }

    public void RefreshSlider(float value)
    {
        soundSlider.value = value;
    }
}
