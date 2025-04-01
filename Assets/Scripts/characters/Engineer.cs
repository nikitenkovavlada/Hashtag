using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engineer : MonoBehaviour
{
    //standard movement variables
    public KeyCode jump;
    public KeyCode left;
    public KeyCode right;
    public KeyCode grab;


    public float speed;
    public float jumpForce;

    private Vector2 originalXScale;
    private int direction = 1;

    //standard bool triggers
    public bool grounded;
    static public Boolean wallTriggered = false;
    public static Boolean ableToGrabFlag = false;

    //All characters need it.
    public Transform trans;
    public Transform transSpawn;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    public Camera playerCam;
    public float camyoffset;


    //Engineer special feature
    public KeyCode teleport;
    public int throwForce;
    private Boolean ableToTelport = true;
    public GameObject TeleportBall;
    public GameObject TeleportBallSpawn;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        originalXScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        GrabFlag();
        ThrowTeleportBall();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.transform.tag.Equals("Ground"))
        {
            grounded = true;
            rb.velocity = new Vector2(rb.velocity.y, 0);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //print(collision.transform.tag);
        if (collision.transform.tag.Equals("Ground"))
        {
            grounded = false;
        }

    }


    private void Animations()
    {
        anim.SetBool("Grounded", grounded);
        anim.SetFloat("Velocity", Mathf.Abs(rb.velocity.x));

    }

    private void Move()
    {
        if (Input.GetKeyUp(right) || Input.GetKeyUp(left))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (Input.GetKey(left) && wallTriggered == false)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            sr.flipX = true;

        }
        else if (Input.GetKey(right) && wallTriggered == false)
        {
            sr.flipX = false;
            rb.velocity = new Vector2(speed, rb.velocity.y);

        }

        FlipX();
    }

    private void FlipX()
    {
        if (Input.GetKey(left))
        {
            direction = -1;
        }
        else if (Input.GetKey(right))
        {
            direction = 1;
        }
        transform.localScale = new Vector3(direction * originalXScale.x, originalXScale.y);

    }

    private void Jump()
    {
        if (grounded)
        {
            if (Input.GetKeyDown(jump))
            {
                rb.AddForce(new Vector2(0, jumpForce));
            }
        }
    }

    public void GrabFlag()
    {
        if (ableToGrabFlag && Input.GetKey(grab))
        {
            GameObject.FindGameObjectWithTag("Flag").transform.position = transSpawn.transform.position;
        }
    }

    public void ThrowTeleportBall()
    {
        // 1) Spawn the ball ONCE when the key is first pressed
        if (Input.GetKeyDown(teleport))
        {
            if (ableToTelport)
            {
                TeleportBallSpawn = Instantiate(TeleportBall);
                TeleportBallSpawn.transform.position = transSpawn.transform.position;
                ableToTelport = false;
            }
        }

        // 2) Apply force ONCE when the key is released
        if (Input.GetKeyUp(teleport))
        {
            if (TeleportBallSpawn != null)
            {
                TeleportBallSpawn.GetComponent<Rigidbody2D>().AddForce(
                    new Vector2(direction * throwForce, throwForce),
                    ForceMode2D.Impulse
                );
            }

            ableToTelport = true;
        }
    }

}
