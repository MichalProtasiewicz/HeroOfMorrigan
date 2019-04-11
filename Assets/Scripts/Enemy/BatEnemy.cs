using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemy : MonoBehaviour {

    public GameObject target;
    public float ratio = 0.02f;
    bool gotIt;
    public float range;

    public int curHealth;
    public int maxHealth;

    private Animator myAnim;


    // Use this for initialization
    void Start () {
		if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        myAnim = GetComponent<Animator>();
        curHealth = maxHealth;
    }
	
	// Update is called once per frame
	void Update () {

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range);

        foreach (Collider2D h in hits)
        {
            if (h.gameObject == target)
            {
                gotIt = true;
                break;
            }
        }

        if(gotIt)
        {
            float step;

            if (Vector3.Dot(target.transform.position - transform.position, target.transform.localScale.x * Vector3.right) > 0)
            {
                step = ratio * 2;
            }
            else
            {
                step = ratio;
            }
            
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        }

        if (curHealth <= 0)
        {
            myAnim.Play("batDeath");

            //Destroy(gameObject, 0.65f);
            gameObject.SetActive(false);

            curHealth = maxHealth;
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        Gizmos.DrawWireSphere(transform.position, range);
    }
    public void Damage(int damage)
    {
        curHealth -= damage;
        myAnim.Play("BatHurt");

    }
}
