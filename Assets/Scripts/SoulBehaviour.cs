using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulBehaviour : MonoBehaviour
{
    public GameObject player;
    public GameObject infoText;
    public GameObject soulSound;
    public float score = 1000;
    public float decayRate = 1;

    bool canMove = false;

    // Start is called before the first frame update
    void Start()
    {
        infoText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Vector3.Lerp(this.transform.position, player.transform.position, 0.5f);
        }

        if (score > 0.5)
        {
            score = score - decayRate * Time.deltaTime;
        }
    }

    public void ShowInfoText(bool enabled)
    {
        infoText.gameObject.SetActive(enabled);
    }
}
