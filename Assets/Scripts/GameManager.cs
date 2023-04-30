using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int soulsCollected = 0;
    public Text soulsCollectedText;
    public Text soulScoreTextDebug;
    public float soulScore = 0;
    public float storedSoulDecayRate = 0.2f;

    private void Start()
    {
        soulsCollectedText.text = "Souls: " + 0;
    }

    private void Update()
    {
        if (soulScore > 0.5)
        {
            soulScore = soulScore - storedSoulDecayRate * Time.deltaTime * soulsCollected;
            soulScoreTextDebug.text = ((int)soulScore).ToString();
        }
    }

    public void addSoul(float score)
    {
        soulsCollected += 1;
        soulsCollectedText.text = "Souls: " + soulsCollected;
        soulScore += score;
    }
}
