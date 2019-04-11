using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {


    public float waitToRespawn;
    public PlayerController thePlayer;

    public GameObject deathSplosion;

    public int coinCount;

    public AudioSource coinSound;

    public Text coinText;
    public Text potionText;

    public int maxHealth;
    public int healthCount;

    public int maxPotion;
    public int potionCount;

    public Slider myhealthBar;

    private bool respawning;

    public ResetOnRespawn[] objectsToReset;

    public bool invicible;

    public AudioSource levelMusic;
    public AudioSource gameOverMusic;

    public int healthToRestore;

    

    // Use this for initialization
    void Start () {
        thePlayer = FindObjectOfType<PlayerController>();

        objectsToReset = FindObjectsOfType<ResetOnRespawn>();

        if(PlayerPrefs.HasKey("CoinCount"))
        {
            coinCount = PlayerPrefs.GetInt("CoinCount");
        }

        coinText.text = "" + coinCount;

        /*
        if(PlayerPrefs.HasKey("HealthCount"))
        {
            healthCount = PlayerPrefs.GetInt("HealthCount");
        }
        else
        { */
            healthCount = maxHealth;
        // }
        myhealthBar.value = healthCount;

        /*
        if (PlayerPrefs.HasKey("PotionCount"))
        {
            potionCount = PlayerPrefs.GetInt("PotionCount");
        }
        else
        { */
            potionCount = maxPotion;
        // }
        potionText.text = "" + potionCount;
    }
	
	// Update is called once per frame
	void Update () {
		if(healthCount <= 0 && !respawning)
        {
            Respawn();
            respawning = true;
        }

        if(Input.GetButtonDown("Fire2") && potionCount > 0)
        {
            GiveHealth(healthToRestore);
        }

    }

    public void Respawn()
    {
       StartCoroutine("RespawnCo");
    }

    public IEnumerator RespawnCo()
    {
        thePlayer.gameObject.SetActive(false);

        Instantiate(deathSplosion, thePlayer.transform.position, thePlayer.transform.rotation);

        yield return new WaitForSeconds(waitToRespawn);

        healthCount = maxHealth;
        potionCount = maxPotion;

        respawning = false;
        myhealthBar.value = healthCount;

        /////////////// po smierci tracimy polowe pieniedzy
        coinCount = coinCount / 2;
        coinText.text = "" + coinCount;

        potionText.text = "" + potionCount;

        thePlayer.transform.position = thePlayer.respawnPosition;
        thePlayer.gameObject.SetActive(true);

        for(int i = 0; i < objectsToReset.Length; i++)
        {
            objectsToReset[i].gameObject.SetActive(true);
            objectsToReset[i].ResetObject();
        }
    }

    public void AddCoins(int coinsToAdd)
    {
        coinCount = coinCount + coinsToAdd;

        coinText.text = "" + coinCount;

        coinSound.Play();
    }

    public void HurtPlayer(int damageToTake)
    {
        if(!invicible)
        {
            healthCount = healthCount - damageToTake;
            myhealthBar.value = healthCount;

            thePlayer.Knockback();

            thePlayer.hurtSound.Play();
        }
    }

    public void GiveHealth(int healthToGive)
    {
        healthCount += healthToGive;

        potionCount--;
        potionText.text = "" + potionCount;

        if (healthCount > maxHealth)
        {
            healthCount = maxHealth;
        }
        coinSound.Play();

        myhealthBar.value = healthCount;
    }
}
