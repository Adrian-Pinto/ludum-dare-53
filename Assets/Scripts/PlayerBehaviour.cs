using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    public PlayerMovement movement;

    float horizontalMove = 0.0f;
    float runSpeed = 40.0f;
    bool jump = false;
    bool crouch = false;

    public Text soulsText;
    GameObject soul;
    bool canPickUpSoul = false;

    [HideInInspector]
    int soulsCollected = 0;

    private void Start()
    {
        soulsText.text = "Souls: " + 0;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
            jump = true;

        if (Input.GetButtonDown("Crouch"))
            crouch = true;
        else if (Input.GetButtonUp("Crouch"))
            crouch = false;


        // Soul mechanics
        if (Input.GetKeyDown(KeyCode.E) && canPickUpSoul)
        {
            Destroy(soul);
            soul = null;
            canPickUpSoul = false;
            soulsCollected++;
            soulsText.text = "Souls: " + soulsCollected;
        }
    }

    void FixedUpdate()
    {
        movement.Move(horizontalMove * Time.deltaTime, crouch, jump);
        jump = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Soul")
        {
            canPickUpSoul = true;
            soul = collision.gameObject;
        }
    }
}
