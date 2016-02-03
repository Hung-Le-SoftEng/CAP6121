using UnityEngine;
using System.Collections;

public class LightSaber : MonoBehaviour {

    LineRenderer lineRenderer;
    public Transform startPos;
    public Transform endPos;


	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        lineRenderer.SetPosition(0, startPos.position);
        lineRenderer.SetPosition(1, endPos.position);

	}
}
