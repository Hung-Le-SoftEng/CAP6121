using UnityEngine;
using System.Collections;

public class LightSaber : MonoBehaviour {

    LineRenderer lineRenderer;
    public Transform startPos;
    public Transform endPos;

    private float textureOffset = 0f;
    public bool on = true;
    private Vector3 endPosExtendedPos;

	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        endPosExtendedPos = endPos.localPosition;

    }
	
	// Update is called once per frame
	void Update () {

        lineRenderer.SetPosition(0, startPos.position);
        lineRenderer.SetPosition(1, endPos.position);

        if (on)
        {
            endPos.localPosition = Vector3.Lerp(endPos.localPosition, endPosExtendedPos, Time.deltaTime * 5f);
        }
        else
        {
            endPos.localPosition = Vector3.Lerp(endPos.localPosition, startPos.localPosition, Time.deltaTime * 5f);
        }


        textureOffset -= Time.deltaTime * 2f;
        if(textureOffset < -10f)
        {
            textureOffset += 10f;
        }

        lineRenderer.sharedMaterials[1].SetTextureOffset("_MainTex", new Vector2(textureOffset, 0f));

	}
}
