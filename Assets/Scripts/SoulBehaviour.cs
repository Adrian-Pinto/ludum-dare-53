using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulBehaviour : MonoBehaviour
{
    public GameObject player;
    public GameObject infoText;
    public GameObject soulSound;

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
    }

    public void ShowInfoText(bool enabled)
    {
        infoText.gameObject.SetActive(enabled);
    }
}
