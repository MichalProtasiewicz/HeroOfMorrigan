using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private bool attacking = false;

    private float attackTimer = 0;
    private float attackCd = 0.7f;

    public Collider2D attackTrigger;

    private Animator anim;


	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
        attackTrigger.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetButtonDown("Fire1") && !attacking)
        {
            attacking = true;
            attackTimer = attackCd;

            attackTrigger.enabled = true;
        }

        if(attacking)
        {
            if(attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {
                attacking = false;
                attackTrigger.enabled = false;
            }
        }

        anim.SetBool("Attacking", attacking);
	}
}
