using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackTrigger : MonoBehaviour {

    public int dmg = 1;

    public bool atackable;

	// Use this for initialization
	void Start () {
        atackable = true;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.SendMessageUpwards("Damage", dmg);
            atackable = false;
        }
    }
}
