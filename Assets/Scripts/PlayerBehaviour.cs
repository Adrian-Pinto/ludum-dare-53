using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    GameManager gameManager;

    public PlayerMovement movement;

    float horizontalMove = 0.0f;
    float runSpeed = 40.0f;
    bool jump = false;
    bool crouch = false;

    GameObject soul;
    bool canPickUpSoul = false;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
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
            Instantiate(soul.GetComponent<SoulBehaviour>().soulSound, soul.transform.position, Quaternion.identity);
            gameManager.addSoul(soul.GetComponent<SoulBehaviour>().score);

            Destroy(soul);
            soul = null;
            canPickUpSoul = false;
        }
    }

    void FixedUpdate()
    {
        movement.Move(horizontalMove * Time.deltaTime, crouch, jump);
        jump = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Soul"))
        {
            collision.gameObject.GetComponent<SoulBehaviour>().ShowInfoText(true);
            canPickUpSoul = true;
            soul = collision.gameObject;
        }
        if (collision.name == "Finish")
        {
            gameManager.finishLevel();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Soul"))
        {
            collision.gameObject.GetComponent<SoulBehaviour>().ShowInfoText(false);
            canPickUpSoul = false;
            soul = null;
        }
    }
}
