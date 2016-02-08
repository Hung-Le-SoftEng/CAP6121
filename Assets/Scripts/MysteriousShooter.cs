using UnityEngine;
using System.Collections;

public class MysteriousShooter : MonoBehaviour {

    public GameObject bullet;

    public float bulletOriginRadius = 30f;
    public float bulletOriginDeviation = 5f;



    public float bulletAccuracy = 50f;
    public float bulletSpeed = 5f;

    public float fireRate = 10f;
    public float fireRateDeviation = 5f;

    public bool shooting = false;
    private float timer = 5f;

    


	// Use this for initialization
	void Start () {
        timer = fireRate;
	}
	
	// Update is called once per frame
	void Update () {
        if (bullet == null) {
            return;
        }

        if (shooting) {
            // randomly generate position vector where the bullet will be spawn
            //Vector3 origin = new Vector3(Random.Range(transform.position.x - 30f, transform.position.x + 30f),
            //                            Random.Range(transform.position.y, transform.position.y + 30f),
            //                            Random.Range(transform.position.z - 30f, transform.position.z + 30f));

            Vector3 origin = transform.forward * (Random.Range(20f, 30f)) +
                             transform.right * (Random.Range(-15f, 15f)) +
                             transform.up * (Random.Range(0f, 10f));


            origin = origin.normalized * (Random.Range(bulletOriginRadius - bulletOriginDeviation, bulletOriginRadius + bulletOriginDeviation));
            origin += transform.position;

            
            Vector3 targetDirection = new Vector3();
            if (Random.Range(0, 100) < bulletAccuracy) { 
                // very accurate, direct at middle of player with randomize up and down
                targetDirection = (transform.position + transform.up * Random.Range(-.5f, .5f)) - origin;
                
            }
            else {
                // deviation for lower accuracy
                targetDirection = (transform.position + transform.up * Random.Range(-1f, 2f)
                    + transform.right * Random.Range(-5f, 5f)) - origin;
            }

            GameObject gameObj = (GameObject)Instantiate(bullet, origin, Quaternion.LookRotation(targetDirection));
            //gameObj.transform.rotation = Quaternion.LookRotation(targetDirection);

            Rigidbody rb = gameObj.GetComponent<Rigidbody>();
            rb.velocity = targetDirection.normalized * bulletSpeed;

            shooting = false;
        }

        timer -= Time.deltaTime;
        if (timer < 0) {
            shooting = true;
            timer = fireRate + Random.Range(-fireRateDeviation, fireRateDeviation);

        }
	}
}
