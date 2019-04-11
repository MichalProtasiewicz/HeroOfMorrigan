using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    private LevelManager levelManager;

    public int coinValue;

    private Animator myAnim;

    public bool coinTaken;

    // Use this for initialization
    void Start () {
        levelManager = FindObjectOfType<LevelManager>();

        myAnim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            levelManager.AddCoins(coinValue);

            

            coinTaken = true;
            myAnim.SetBool("coinTaken", coinTaken);

            //yield return new WaitForSeconds(0.4f);
            //Destroy(gameObject, 0.4f);

            gameObject.SetActive(false);

        }
    }

}
