using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float offset;
    public float offsetSmoothing;
    private Vector3 playerPosition;

    // Update is called once per frame
    void Update()
    {
       

    }

    private void FixedUpdate()
    {
        playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);

        if (player.transform.localScale.x > 0.0f)
        {
            playerPosition = new Vector3(playerPosition.x + offset, playerPosition.y, -10.0f);
        }
        else
        {
            playerPosition = new Vector3(playerPosition.x - offset, playerPosition.y, -10.0f);
        }


        this.transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);
    }
}
