using UnityEngine;
using System.Collections;

public class WaveGenerator : MonoBehaviour {

    public GameObject wave;
    public float speed = 5f;
    public float distance = 10;
    public float waveFreq = 1;

    public float stunTime = 10f;

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

            // use overlap sphere to find monster and stunt them
            Collider[] objectsInRange = Physics.OverlapSphere(transform.position, distance);
            foreach (Collider obj in objectsInRange) {
                if (obj.tag == "Enemy") {
                    obj.GetComponent<MonsterController>().stunnedTimer = stunTime;
                }
            }


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
