using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulBehaviour : MonoBehaviour
{

    public Soul soul;
    public GameObject infoText;
    public GameObject soulSound;
 

    // Start is called before the first frame update
    void Start()
    {
        soul = new Soul();
        infoText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        soul.DecaySoul();

        //if (score > 0.5)
        //{
        //    
        //}
    }

    public void ShowInfoText(bool enabled)
    {
        infoText.gameObject.SetActive(enabled);
    }

  
}
