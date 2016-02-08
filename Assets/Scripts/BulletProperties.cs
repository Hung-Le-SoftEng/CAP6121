using UnityEngine;
using System.Collections;

public class BulletProperties : MonoBehaviour {

    public enum BulletType {
        normal,
        wave
    }

    public BulletType bulletType = BulletType.normal;
    public float damamge = 10f;
    


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



    void OnCollisionEnter(Collision collision) {
        // do something




        // destroy itself
        GameObject.Destroy(gameObject);
    }
}
