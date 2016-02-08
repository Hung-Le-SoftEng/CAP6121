using UnityEngine;
using System.Collections;

public class WaveGenerator : MonoBehaviour {

    public GameObject wave;
    public float speed = 5f;
    public float distance = 10;
    public float waveFreq = 1;

    private float timer = 0f;
	// Use this for initialization

    private Vector3 originalPos;

	void Start () {
        timer = 1/waveFreq;
        originalPos = transform.position;

        GetComponent<Rigidbody>().velocity = transform.forward * speed;
	}
	
	// Update is called once per frame
	void Update () {

        if (wave == null)
            return;

        // generate wave
        if (timer >= 1 / waveFreq) {

            Instantiate(wave, transform.position, transform.rotation);

            timer -= 1 / waveFreq;
        }
        timer += Time.deltaTime;

        //transform.position += transform.forward * speed * Time.deltaTime;


        // self-destruct after travel certain distance
        if ((transform.position - originalPos).magnitude > distance) {
            GameObject.Destroy(gameObject);
        }




	}
}
