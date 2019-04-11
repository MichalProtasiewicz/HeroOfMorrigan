using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolController : MonoBehaviour {

    public Transform leftPoint;
    public Transform rightPoint;

    public float moveSpeed;

    private Rigidbody2D myRigidbody;

    public bool movingRight;

    public int curHealth;
    public int maxHealth;

    private Animator myAnim;

    // Use this for initialization
    void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        curHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
        if(movingRight && transform.position.x > rightPoint.position.x)
        {
            movingRight = false;
        }
        if(!movingRight && transform.position.x < leftPoint.position.x)
        {
            movingRight = true;
        }

        if(movingRight)
        {
            myRigidbody.velocity = new Vector3(moveSpeed, myRigidbody.velocity.y, 0f);
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            myRigidbody.velocity = new Vector3(-moveSpeed, myRigidbody.velocity.y, 0f);
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (curHealth<=0)
        {
            myAnim.Play("SlimeDeath");

            //Destroy(gameObject,0.65f);
            gameObject.SetActive(false);
            curHealth = maxHealth;

        }

    }

    public void Damage(int damage)
    {
        curHealth -= damage;
        myAnim.Play("SlimeHurt");

    }
}
