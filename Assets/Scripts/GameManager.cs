using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int soulsCollected = 0;
    public Text soulsCollectedText;

    public void addSoul()
    {
        soulsCollected += 1;
        soulsCollectedText.text = soulsCollected.ToString();
    }
}
