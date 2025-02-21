using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Main : MonoBehaviour
{
    public Transform trans;
    public Transform transSpawn;
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    public bool grounded;
    public int health = 100;
    private Vector2 originalXScale;
    static public Boolean trig=false ;
    private int dir = 1;

    private SpriteRenderer sr;
    public GameObject wall;

    public KeyCode right;
    public KeyCode left;
    public KeyCode grab;
    public KeyCode teleport;
    public Camera playerCam;
    public float camyoffset;

    private Animator anim;

    public static Boolean able_tograb_flag = false;

    public GameObject TeleportBall;
    public GameObject TeleportingBall;
    private Boolean ableToTelport = true;
    public int ThrowForce;

    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        originalXScale = transform.localScale;
        anim = GetComponent<Animator>();

        
        
    }

    // Update is called once per frame
    void Update()
    {

        Animations();

        Jump();
        Move();
        createWall();
        GrabFlag();
        ThrowTeleportBall();
        
        //2D Vector(x,y);



    }

    private void Jump()
    {
        if (grounded)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                rb.AddForce(new Vector2(0, jumpForce));
            }
        }
    }

    private void createWall()
    {
       
    }

        private void Move()
    {
        if (Input.GetKeyUp(right) || Input.GetKeyUp(left) )
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (Input.GetKey(left) && trig==false )
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            sr.flipX = true;
            
        }
        else if (Input.GetKey(right) && trig==false)
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
            dir = -1;
        }
        else if (Input.GetKey(right))
        {
            dir = 1;
        }
        transform.localScale = new Vector3(dir * originalXScale.x, originalXScale.y);

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.transform.tag.Equals("Ground"))
        {
            grounded = true;
            rb.velocity = new Vector2 (rb.velocity.y, 0);
        }
    }

   


    private void OnCollisionExit2D(Collision2D collision)
    {
        print(collision.transform.tag);
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

    public void GrabFlag()
    {
        if (able_tograb_flag && Input.GetKey(grab))
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
                TeleportingBall = Instantiate(TeleportBall);
                TeleportingBall.transform.position = transSpawn.transform.position;
                ableToTelport = false;
            }
        }

        // 2) Apply force ONCE when the key is released
        if (Input.GetKeyUp(teleport))
        {
            if (TeleportingBall != null)
            {
                TeleportingBall.GetComponent<Rigidbody2D>().AddForce(
                    new Vector2(dir * ThrowForce, ThrowForce),
                    ForceMode2D.Impulse
                );
            }

            ableToTelport = true;
        }
    }



}
