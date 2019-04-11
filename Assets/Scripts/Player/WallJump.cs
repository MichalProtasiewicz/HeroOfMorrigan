using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour
{

    PlayerController player;
    public float distance;
    public float speed = 2f;

    public bool wallJumping;

    // Use this for initialization
    void Start()
    {
        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        Physics2D.queriesStartInColliders = false;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);

        if (Input.GetButtonDown("Jump") && !player.isGrounded && hit.collider!=null)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed * hit.normal.x,speed);

            wallJumping = true;

            transform.localScale = transform.localScale.x == 1 ? new Vector2(-1, 1) : Vector2.one;
        }
        else if ( hit.collider != null && wallJumping)
        {
            wallJumping = false;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

}
