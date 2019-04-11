using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotingEnemy : MonoBehaviour {

    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    private Transform player;
    private GameObject target;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject arrow;

    // Use this for initialization
    void Start () {
        target = GameObject.FindGameObjectWithTag("Player");
        player = target.transform;

        timeBtwShots = startTimeBtwShots;
    }

    // Update is called once per frame
    void Update () {

        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }

        if(Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }

        if(timeBtwShots <= 0)
        {
            Instantiate(arrow, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

	}
}
