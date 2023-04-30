using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float offset;
    public float offsetSmoothing;
    public float distance = -10.0f;
    private Vector3 playerPosition;

    private void FixedUpdate()
    {
        playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);

        if (player.transform.localScale.x > 0.0f)
        {
            playerPosition = new Vector3(playerPosition.x + offset, playerPosition.y, distance);
        }
        else
        {
            playerPosition = new Vector3(playerPosition.x - offset, playerPosition.y, distance);
        }


        this.transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);
    }
}
