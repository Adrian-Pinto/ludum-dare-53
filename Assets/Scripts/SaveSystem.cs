using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public void SaveVolume(float volume)
    {
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public float LoadVolume()
    {
        return PlayerPrefs.GetFloat("Volume");
    }
}
