using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulBehaviour : MonoBehaviour
{
    public GameObject player;

    bool canMove = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Vector3.Lerp(this.transform.position, player.transform.position, 0.5f);
        }
    }
}
