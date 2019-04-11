using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{

    public float moveSpeed;
    private bool canMove;

    private Rigidbody2D myRigidbody;

    public int curHealth;
    public int maxHealth;

    private Animator myAnim;


    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();

        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            myRigidbody.velocity = new Vector3(-moveSpeed, myRigidbody.velocity.y, 0f);
        }
        if (curHealth <= 0)
        {
            myAnim.Play("SlimeDeath");

            //Destroy(gameObject, 0.65f);
            gameObject.SetActive(false);
            curHealth = maxHealth;

        }
    }

    void OnBecameVisible()
    {
        canMove = true;
    }

    void OnEnable()
    {
        canMove = false;
    }
    public void Damage(int damage)
    {
        curHealth -= damage;
        myAnim.Play("SlimeHurt");
    }
}
