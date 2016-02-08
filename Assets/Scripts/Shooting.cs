using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

    public GameObject bullet;
    public Transform shootingPoint;

    public float fireSpeed = 5f;

    public float fireRate = 1f;



    public bool shoot;
    private float timer = 0f;
	// Use this for initialization
	void Start () {

        if (shootingPoint = null)
            shootingPoint = transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (bullet == null)
            return;

        // ready too shoot
        if (timer >= 1 / fireRate)
            shoot = true;

        if (shoot) {
            GameObject gameObj = (GameObject)Instantiate(bullet, shootingPoint.position, shootingPoint.rotation);

            
            if (gameObj.GetComponent<WaveGenerator>() != null) {
                gameObj.GetComponent<WaveGenerator>().speed = fireSpeed;
            }

            timer -= 1 / fireRate;
            timer = Mathf.Max(0, timer);
        }

        timer += Time.deltaTime;


	
	}
}
