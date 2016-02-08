using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {

    public float lifeTime = 10f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if(lifeTime < 0)
        {
            GameObject.Destroy(transform.gameObject);
        }

        lifeTime -= Time.deltaTime;

	}
}
