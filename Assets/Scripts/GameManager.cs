using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int soulsCollected = 0;
    public Text soulsCollectedText;

    private void Start()
    {
        soulsCollectedText.text = "Souls: " + 0;
    }

    public void addSoul()
    {
        soulsCollected += 1;
        soulsCollectedText.text = "Souls: " + soulsCollected;
    }
}
