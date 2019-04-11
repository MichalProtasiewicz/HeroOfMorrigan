using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public Sprite checkPointSprite;
    public Sprite checkPointActiveSprite;

    private SpriteRenderer theSpriteRenderer;

    public bool checkPointActive;

    private Animator myAnim;

    // Use this for initialization
    void Start () {
        theSpriteRenderer = GetComponent<SpriteRenderer>();

        myAnim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && Input.GetButtonDown("Fire3"))
        {
            theSpriteRenderer.sprite = checkPointActiveSprite;
            checkPointActive = true;

            myAnim.SetBool("checkPointActive", checkPointActive);
        }
    }
}
