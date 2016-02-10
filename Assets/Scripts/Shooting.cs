using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

    public GameObject bullet;
    public GameObject testShootingPoint;

    private Vector3 shootingPosition;
    private Quaternion shootingRotation;


    public float fireSpeed = 5f;

    public float fireRate = 1f;

    

    public bool shoot;
    private float timer = 0f;

    public bool testShooting = false;
	// Use this for initialization
	void Start () {

        if (testShootingPoint == null)
            testShootingPoint = gameObject;

	}
	
	// Update is called once per frame
	void Update () {
        if (testShooting)
            TestShoot();
	}

    void TestShoot() {
        if(bullet == null)
            return;

        shootingPosition = testShootingPoint.transform.position + testShootingPoint.transform.forward;
        shootingRotation = testShootingPoint.transform.rotation;

        Shoot();
    }

    public void Shoot(GameObject bullet, Vector3 shootingPoint, Quaternion shootingDir) {
        this.bullet = bullet;
        shootingPosition = shootingPoint;
        shootingRotation = shootingDir;
        Shoot();
    }


    public void Shoot() {

        
        if (bullet == null)
            return;

        timer += Time.deltaTime;

        // ready too shoot
        if (timer >= 1 / fireRate)
            shoot = true;

        if (shoot) {

            Vector3 forwardDir = shootingRotation * Vector3.forward;
            GameObject gameObj = (GameObject)Instantiate(bullet, shootingPosition + forwardDir, shootingRotation);
            Rigidbody rb = gameObj.GetComponent<Rigidbody>();

            rb.velocity = forwardDir * fireSpeed;
        
            if (gameObj.GetComponent<WaveGenerator>() != null) {
                gameObj.GetComponent<WaveGenerator>().speed = fireSpeed;
            }

            timer -= 1 / fireRate;
            timer = Mathf.Max(0, timer);
            shoot = false;
        }

        
    }
}
