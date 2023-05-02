using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int soulsCollected = 0;
    public float soulScore = 0;
    public float storedSoulDecayRate = 0.2f;
    public Text soulsCollectedText;
    public Text soulScoreTextDebug;
    public GameObject finishLevelScreen;
    public TMP_Text finalScoreText;
    public AudioSource levelMusic;
    public AudioSource endMusic;

    private int totalSoulAmount;

    private void Start()
    {
        GameObject[] souls = GameObject.FindGameObjectsWithTag("Soul");
        totalSoulAmount = souls.Length;
        soulsCollectedText.text = "Souls: " + 0 + "/" + totalSoulAmount;
    }

    private void Update()
    {
        //if (soulScore > 0.5)
        //{
        //    soulScore = soulScore - storedSoulDecayRate * Time.deltaTime * soulsCollected;
        //    soulScoreTextDebug.text = ((int)soulScore).ToString();
        //}
    }

    public void addSoul(float score)
    {
        soulsCollected += 1;
        soulsCollectedText.text = "Souls: " + soulsCollected + "/" + totalSoulAmount;
        //soulScore += score;
    }

    public void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void finishLevel(List<Soul> souls)
    {
        if(soulsCollected == totalSoulAmount)
        {
            int sum = 0;

            for (int i = 0; i < souls.Count; ++i)
            {
                sum += (int)souls[i].score;
            }


            //int finalScore = ((int)soulScore);
            finishLevelScreen.SetActive(true);
            finalScoreText.text = "Final score: " + sum;

            levelMusic.Stop();
            endMusic.Play();
        }
    }
}
