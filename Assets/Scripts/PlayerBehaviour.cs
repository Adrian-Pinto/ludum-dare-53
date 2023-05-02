using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    GameManager gameManager;

    public PlayerMovement movement;

    float horizontalMove = 0.0f;
    public float runSpeed = 40.0f;
    bool jump = false;
    bool crouch = false;

    // Soul Mechanics
    GameObject soul;
    bool canPickUpSoul = false;
    List<Soul> soulsCollected;

    Animator anim;

    private void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        soulsCollected = new List<Soul>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        anim.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            anim.SetBool("Jump", true);
            jump = true;
        }

        if (Input.GetButtonDown("Crouch"))
            crouch = true;
        else if (Input.GetButtonUp("Crouch"))
            crouch = false;


        // Soul mechanics
        if (Input.GetKeyDown(KeyCode.E) && canPickUpSoul)
        {
            anim.SetTrigger("Attack");


            // Spawn sound
            Instantiate(soul.GetComponent<SoulBehaviour>().soulSound, soul.transform.position, Quaternion.identity);
            
            // Add score
            gameManager.addSoul(0.0f);
            
            // Add soul to the player's soul list
            Soul newSoul = new Soul(soul.GetComponent<SoulBehaviour>().soul);
            newSoul.currentState = Soul.State.PLAYER;
            soulsCollected.Add(newSoul);

            // Destroy and reset the behaviour
            Destroy(soul);
            soul = null;
            canPickUpSoul = false;
        }

        for (int i = 0; i < soulsCollected.Count; ++i)
        {
            soulsCollected[i].DecaySoul();
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
            gameManager.finishLevel(soulsCollected);
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

    public void TouchedFloor()
    {
        anim.SetBool("Jump", false);
    }
}
