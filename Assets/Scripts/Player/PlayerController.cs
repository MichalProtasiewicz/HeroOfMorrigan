using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    private float activeMoveSpeed;

    public bool canMove;

    public Rigidbody2D myRigidbody;

    public float jumpSpeed;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    public bool isGrounded;

    private Animator myAnim;

    public Vector3 respawnPosition;

    public LevelManager theLevelManager;


    public float knockbackForce;
    public float knockbackLength;
    private float knockbackCounter;

    public float invicibleLength;
    private float invicibleCounter;

    public AudioSource jumpSound;
    public AudioSource hurtSound;

    private bool onPlatform;
    public float onPlatformSpeed;

    private float isFalling;

    //

    public bool pushing;
    public bool pushingLeft;
    public bool pushingRight;

    /// <summary>
    private bool isJumping;
    private float jumpTimeCounter;
    public float jumpTime;
    /// </summary>
    /// 
    public bool isClimbingLadder;


    public float x;
    public float y;
    public float z;

    public Vector3 lastCheckpoint;



    // Use this for initialization
    void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();

        theLevelManager = FindObjectOfType<LevelManager>();

        activeMoveSpeed = moveSpeed;
        canMove = true;
        isFalling = 0;


        if ((PlayerPrefs.HasKey("x")) && (PlayerPrefs.HasKey("y")) && (PlayerPrefs.HasKey("z")))
        {
            x = PlayerPrefs.GetFloat("x");
            y = PlayerPrefs.GetFloat("y");
            z = PlayerPrefs.GetFloat("z");
            lastCheckpoint = new Vector3(x, y, z);
            respawnPosition = lastCheckpoint;
            transform.position = lastCheckpoint;
        }
        else
        {
            respawnPosition = transform.position;
        }

    }

    // Update is called once per frame
    void Update () {

        

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);


        if (knockbackCounter <= 0 && canMove)
        {

            if (onPlatform)
            {
                activeMoveSpeed = moveSpeed * onPlatformSpeed;
            }
            else
            {
                activeMoveSpeed = moveSpeed;
            }

            /////////////////////////

            if (Input.GetAxisRaw("Horizontal") > 0f)
            {
                myRigidbody.velocity = new Vector3(activeMoveSpeed, myRigidbody.velocity.y, 0f);
            }
            else if (Input.GetAxisRaw("Horizontal") < 0f)
            {
                myRigidbody.velocity = new Vector3(-activeMoveSpeed, myRigidbody.velocity.y, 0f);
            }
            else
            {
                myRigidbody.velocity = new Vector3(0f, myRigidbody.velocity.y, 0f);
            }


            if (Input.GetAxisRaw("Horizontal") > 0f && pushing == false)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (Input.GetAxisRaw("Horizontal") < 0f && pushing == false)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (Input.GetAxisRaw("Horizontal") > 0f && pushing == true && pushingLeft == true && pushingRight == false)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (Input.GetAxisRaw("Horizontal") < 0f && pushing == true && pushingLeft == true && pushingRight == false)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }

            else if (Input.GetAxisRaw("Horizontal") < 0f && pushing == true && pushingLeft == false && pushingRight == true)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (Input.GetAxisRaw("Horizontal") > 0f && pushing == true && pushingLeft == false && pushingRight == true)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }

            ///////////////////////
            /*
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, jumpSpeed, 0f);
                jumpSound.Play();
                isJumping = true;
                jumpTimeCounter = jumpTime;
            }
            if (isJumping = true && Input.GetButton("Jump"))
            {
                if(jumpTimeCounter > 0)
                {
                    myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, jumpSpeed, 0f);
                    jumpTimeCounter -= Time.deltaTime;
                }
                else if (jumpTimeCounter < 0)
                {
                    isJumping = false;
                }
            }
            if(Input.GetButtonUp("Jump"))
            {
                isJumping = false;
            }
            */

            if (Input.GetButtonDown("Jump") && isGrounded == true && pushing == false)
            {
                myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, jumpSpeed, 0f);               
                jumpSound.Play();
            }

            ///
            if (myRigidbody.velocity.y > 0 && isGrounded == false)
            {
                isFalling = 1;
            }
            else if (myRigidbody.velocity.y < 0 && isGrounded == false)
            {
                isFalling = -1;
            }
            else
            {
                isFalling = 0;
            }
        }

        if (knockbackCounter > 0)
        {
            knockbackCounter -= Time.deltaTime;

            if(transform.localScale.x > 0)
            {
                myRigidbody.velocity = new Vector3(-knockbackForce, knockbackForce, 0f);
            }
            else
            {
                myRigidbody.velocity = new Vector3(knockbackForce, knockbackForce, 0f);
            }
            
        }

        if(invicibleCounter > 0)
        {
            invicibleCounter -= Time.deltaTime;
        }

        if(invicibleCounter <= 0)
        {
            theLevelManager.invicible = false;
        }

        myAnim.SetFloat("SpeedX", Mathf.Abs(myRigidbody.velocity.x));
        myAnim.SetFloat("SpeedY", Mathf.Abs(myRigidbody.velocity.y));
        myAnim.SetBool("Grounded", isGrounded);
        myAnim.SetFloat("IsFalling", isFalling);
        myAnim.SetBool("isClimbingLadder", isClimbingLadder);


    }

    public void Knockback()
    {
        knockbackCounter = knockbackLength;
        invicibleCounter = invicibleLength;
        theLevelManager.invicible = true;

        myAnim.Play("PlayerTakeHit");
        
    }


     void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "MovingPlatform")
        {
            transform.parent = collision.transform;
            onPlatform = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "MovingPlatform")
        {
            transform.parent = null;
            onPlatform = false;
        }
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Ladder")
        {
            isClimbingLadder = true;
        }

        if (collision.tag == "Checkpoint" && Input.GetButtonDown("Fire3"))
        {
            respawnPosition = collision.transform.position;

            PlayerPrefs.SetInt("CoinCount", theLevelManager.coinCount);

            x = respawnPosition.x;
            y = respawnPosition.y;
            z = respawnPosition.z;

            PlayerPrefs.SetFloat("x", x);
            PlayerPrefs.SetFloat("y", y);
            PlayerPrefs.SetFloat("z", z);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ladder")
        {
            isClimbingLadder = false;
        }
    }
}
