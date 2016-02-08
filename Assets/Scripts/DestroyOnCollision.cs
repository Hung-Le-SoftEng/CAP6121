using UnityEngine;
using System.Collections;

public class DestroyOnCollision : MonoBehaviour {


    public int score = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }


    void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.tag == "Player")
        {
            collision.collider.gameObject.GetComponent<PlayerHealth>().TakeDamage();
        }

        if (collision.collider.tag == "Weapon")
        {
            ScoreManager.score += score;
        }


        GameObject.Destroy(transform.gameObject);
    }

}
