using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour {

    public float Ladderspeed;

    public bool isClimbingLadder;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag=="Player" && Input.GetAxisRaw("Vertical") > 0f)
        {
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, Ladderspeed);
            isClimbingLadder = true;
        }
        else if (collision.tag == "Player" && Input.GetAxisRaw("Vertical") < 0f)
        {
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -Ladderspeed);
            isClimbingLadder = true;
        }
        else if (collision.tag == "Player")
        {
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0.393f);
            isClimbingLadder = true;
        }
    }
}
