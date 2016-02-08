using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;
using System.Runtime.InteropServices;


public class GrabThrow : MonoBehaviour {

    public GameObject bodySourceManager;
    private BodySourceManager bodyManager;

    private Dictionary<ulong, GameObject> bodies = new Dictionary<ulong, GameObject>();

    private Kinect.Body currentTrackedObject;


    public GameObject testGrabObject;



    private Kinect.HandState previousHandState = Kinect.HandState.Unknown;
    private GameObject currentlyGrabbedObj;
    private bool grabbed = false;

    public Vector3 speed;
    private Vector3 originalObjectPos;
    private Vector3 prevObjectPos;
    private Vector3 originalHandPos;

    private int counter = 0;

    private GameObject lightSaber;

    // Use this for initialization
    void Start () {

        currentlyGrabbedObj = null;
        currentTrackedObject = null;

    }
	
	// Update is called once per frame
	void Update () {


        if (bodySourceManager == null)
        {
            return;
        }
        bodyManager = bodySourceManager.GetComponent<BodySourceManager>();
        if(bodyManager == null)
        {
            return;
        }

        // get data from kinect
        Kinect.Body[] data = bodyManager.GetData();
        if(data == null)
        {
            return;
        }

        // get list of currently tracked bodies
        Kinect.Body potentialNewTrackedBody = null;
        bool stillTrackingPreviousBody = false;     // check if the previously tracked body is still there
        
        foreach(Kinect.Body body in data)
        {
            if(body == null)
            {
                continue;
            }

            if (body.IsTracked)
            {
                
                // no body being track, get the first body found
                if (currentTrackedObject == null)
                {
                    currentTrackedObject = body;
                }

                // the previous body may already disappearred, save the first body found just in case
                if (potentialNewTrackedBody == null)
                {
                    potentialNewTrackedBody = body;
                }

                // still tracking previous body
                if (body.TrackingId == currentTrackedObject.TrackingId)
                {
                    stillTrackingPreviousBody = true;
                    break;
                }
            }
        }

        // 

        // the previously tracked body have disappear, get the new trackid found
        if(!stillTrackingPreviousBody)
        {
            currentTrackedObject = potentialNewTrackedBody;
        }

        if(currentTrackedObject == null)
        {
            return;
        }


        //////////////////////////////////////////
        // Everything after this is getting data of the body and manipulate the world



        // still holding the object
        if (currentTrackedObject.HandRightState == Kinect.HandState.Closed && grabbed)
        {
            Debug.Log("Holding Object");
            DragObject();
            
        }

        // if specific action is detected, attempt to grab the object
        else if (!grabbed && currentTrackedObject.HandRightState == Kinect.HandState.Closed)
        {
            Debug.Log("Trying to Grab Object");
            GrabObject();
        }

        // release hand while holding something will initiate throw
        else if(currentTrackedObject.HandRightState == Kinect.HandState.Open && grabbed)
        {
            Debug.Log("Throwing Object");
            ThrowObject();
        }

        // other action should reset holding state
        else
        {
            //currentlyGrabbedObj = null;
            Debug.Log("Unknow Action");
            grabbed = false;
        }

        //Debug.Log(currentTrackedObject.HandRightState);
        //Debug.Log(previousHandState);
        previousHandState = currentTrackedObject.HandRightState;

        CursorDraw();
    }


    void GrabObject()
    {
        
        originalHandPos = new Vector3(currentTrackedObject.Joints[Kinect.JointType.HandRight].Position.X,
                                        currentTrackedObject.Joints[Kinect.JointType.HandRight].Position.Y,
                                        currentTrackedObject.Joints[Kinect.JointType.HandRight].Position.Z);


        //Debug.Log("Transform Pos: " + transform.position);
        //Debug.Log("Hand Right Vector3: " + originalHandPos);
        //Debug.Log("Hand Right Joint Orientation: " + currentTrackedObject.JointOrientations[Windows.Kinect.JointType.HandRight].Orientation);
        //Debug.Log("Hand Right Joint Pos: " + currentTrackedObject.Joints[Windows.Kinect.JointType.HandRight].Position.ToString());

        //RaycastHit hit;
        //Ray ray = new Ray(transform.position, new Vector3());
        //if(Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "Draggable")
        //{
        //    currentlyGrabbedObj = hit.collider.gameObject;
        //}



        /////////////////////////////////////////////////////
        // Testing Section
        originalObjectPos = testGrabObject.transform.position;
        grabbed = true;
        

    }

    void DragObject()
    {
        //currentlyGrabbedObj.transform.position += new Vector3();

        Vector3 deltaDir = new Vector3(currentTrackedObject.Joints[Kinect.JointType.HandRight].Position.X - originalHandPos.x,
                                        currentTrackedObject.Joints[Kinect.JointType.HandRight].Position.Y - originalHandPos.y,
                                        currentTrackedObject.Joints[Kinect.JointType.HandRight].Position.Z - originalHandPos.z);

        if (testGrabObject != null)
        {
            //Debug.Log("Original Pos: " + testGrabObject.transform.position);
            //Debug.Log("Delta Pos: " + deltaDir);
            //testGrabObject.transform.position = originalObjectPos + transform.right * deltaDir.x * speed.x + transform.up * deltaDir.y * speed.y + transform.forward * -deltaDir.z * speed.z;
            Vector3 forceDir = transform.right * deltaDir.x * speed.x + transform.up * deltaDir.y * speed.y + transform.forward * -deltaDir.z * speed.z;

            //testGrabObject.GetComponent<Rigidbody>().AddForce(transform.right * deltaDir.x * speed.x + transform.up * deltaDir.y * speed.y + transform.forward * -deltaDir.z * speed.z);
            testGrabObject.GetComponent<Rigidbody>().MovePosition(originalObjectPos + forceDir);

            if (counter % 5 == 0)
            {
                prevObjectPos = testGrabObject.transform.position;
                counter = 0;
            }
            counter++;
            //Debug.Log("Changed Pos: " + testGrabObject.transform.position);
        }

    }

    void ThrowObject()
    {
        // calculate the velocity
        Vector3 velocity = testGrabObject.transform.position - prevObjectPos;
        testGrabObject.GetComponent<Rigidbody>().velocity = (velocity) / Time.deltaTime;
        Debug.Log(velocity);


        grabbed = false;
    }


    void CursorDraw()
    {

        Vector3 HandPos = transform.right * currentTrackedObject.Joints[Kinect.JointType.HandRight].Position.X + 
            transform.up * currentTrackedObject.Joints[Kinect.JointType.HandRight].Position.Y + 
            transform.forward * currentTrackedObject.Joints[Kinect.JointType.HandRight].Position.Z;
        HandPos += transform.position;

        Vector3 WristPos = transform.right * currentTrackedObject.Joints[Kinect.JointType.WristRight].Position.X +
            transform.up * currentTrackedObject.Joints[Kinect.JointType.WristRight].Position.Y +
            transform.forward * -currentTrackedObject.Joints[Kinect.JointType.WristRight].Position.Z;
        WristPos += transform.position;

        Vector3 TipPos = transform.right * currentTrackedObject.Joints[Kinect.JointType.HandTipRight].Position.X +
            transform.up * currentTrackedObject.Joints[Kinect.JointType.HandTipRight].Position.Y +
            transform.forward * -currentTrackedObject.Joints[Kinect.JointType.HandTipRight].Position.Z;
        TipPos += transform.position;


        Debug.DrawRay(WristPos + (transform.forward * currentTrackedObject.Joints[Kinect.JointType.WristRight].Position.Z * 2), (TipPos - WristPos).normalized * 3, Color.red, 1f);
        //Debug.DrawRay(WristPos, (HandPos - WristPos).normalized * 3, Color.red, 1f);
        //Debug.DrawRay(transform.position, (HandPos - transform.position).normalized * 3, Color.green, 1f);
    }
}
