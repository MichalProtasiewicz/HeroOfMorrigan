using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPush : MonoBehaviour
{

    public float distance = 0.75f;
    public LayerMask boxMask;

    GameObject box;


    public bool pushing;

    private PlayerController player;

    private Animator myAnim;


    // Use this for initialization
    void Start()
    {
        player = GetComponent<PlayerController>();
        myAnim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance, boxMask);


        if (hit.collider != null && hit.collider.gameObject.tag == "Pushable" && Input.GetButtonDown("Fire3"))
        {
            box = hit.collider.gameObject;

            box.GetComponent<FixedJoint2D>().enabled = true;
            box.GetComponent<boxPull>().beingPushed = true;
            box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();

            pushing = true;
            player.pushing = true;
            myAnim.SetBool("Pushing", pushing);




            Vector2 D = transform.position - hit.transform.position;
                D.Normalize();
            if (Input.GetAxis("Horizontal") < 0 && D.x < 0)
            {
                // Pull from left // Pull animation
                myAnim.SetFloat("PushPull", Input.GetAxis("Horizontal"));
                player.pushingLeft = false;
                player.pushingRight = false;



            }
            else if (Input.GetAxisRaw("Horizontal") > 0 && D.x < 0)
            {
                // Push from left // Push animation
                //	Debug.Log ("Pushing from left");
                myAnim.SetFloat("PushPull", Input.GetAxis("Horizontal"));
                player.pushingLeft = true;
                player.pushingRight = false;


            }
            else if (Input.GetAxisRaw("Horizontal") < 0 && D.x > 0)
            {
                // Push from right // Push animation
                //	Debug.Log ("Pushing from right");
                myAnim.SetFloat("PushPull", -Input.GetAxis("Horizontal"));
                player.pushingLeft = false;
                player.pushingRight = true;

            }
            else if (Input.GetAxisRaw("Horizontal") > 0 && D.x > 0)
            {
                // Pull from right // Pull animation	
                myAnim.SetFloat("PushPull", -Input.GetAxis("Horizontal"));
                player.pushingLeft = false;
                player.pushingRight = false;

            }
            else if (Input.GetAxisRaw("Horizontal") == 0 && D.x == 0)
            {
                // idle animation
                myAnim.SetFloat("PushPull", 0);
                player.pushingLeft = false;
                player.pushingRight = false;

            }
        }
        else if (Input.GetButtonUp("Fire3"))
        {
            box.GetComponent<FixedJoint2D>().enabled = false;
            box.GetComponent<boxPull>().beingPushed = false;
            myAnim.SetFloat("PushPull", 0);

            pushing = false;
            player.pushing = false;
            player.pushingLeft = false;
            player.pushingRight = false;

            myAnim.SetBool("Pushing", pushing);
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x * distance);
    }
}
