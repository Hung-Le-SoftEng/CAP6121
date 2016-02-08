using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

    public PlayerHealth playerhealth;
    Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {


        // If the player has run out of health...
        if (playerhealth.currentHealth <= 0)
        {
            // ... tell the animator the game is over.
            //Debug.Log("Game Over");
            anim.SetTrigger("GameOver");
        }
    }
}
