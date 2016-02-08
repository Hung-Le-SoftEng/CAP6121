using UnityEngine;
using System.Collections;

public class GunParticles : MonoBehaviour {
    //ParticleSystem gunParticles;
    public float scaleRate = 0.5f;
    float scale = 1f;
    private Vector3 originalScale;
	// Use this for initialization
	void Start () {
        //gunParticles = GetComponent<ParticleSystem> ();
        originalScale = transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {
        //gunParticles.
        scale += Time.deltaTime * scaleRate;
        scale = Mathf.Min(scale, 5f);
        transform.localScale = originalScale * scale;
    }
}
