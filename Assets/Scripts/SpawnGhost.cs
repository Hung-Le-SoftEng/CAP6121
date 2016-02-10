using UnityEngine;
using System.Collections;

public class SpawnGhost : MonoBehaviour {

    public GameObject ghostImage;

	// Use this for initialization
	void Start () {

        GameObject gameObj = (GameObject)Instantiate(ghostImage, transform.position, transform.rotation);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
