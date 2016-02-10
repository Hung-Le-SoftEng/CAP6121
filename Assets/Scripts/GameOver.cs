using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public PlayerHealth playerhealth;
    private float restartDelay = 5f;
    
    private Animator anim;
    private float restartTimer = 0;                     // Timer to count up to restarting the level


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

            // .. increment a timer to count up to restarting.
            restartTimer += Time.deltaTime;

            // .. if it reaches the restart delay...
            if (restartTimer >= restartDelay) {
                // .. then reload the currently loaded level.
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                restartTimer = 0;
            }
            
        }
    }
}
