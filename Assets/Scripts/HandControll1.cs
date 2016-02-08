using UnityEngine;
using System.Collections;

public class HandControll : MonoBehaviour
{
    private GameObject RightHand; // I create variable
    Vector3 OriginalPosition;

    void Start()
    {
        OriginalPosition = gameObject.transform.position;
        //Move();
        //Turning();
    }
    void Update()
    {
        // If Right Hand is empty null it finding else it changing hand position
        if (RightHand == null)
        {
            RightHand = GameObject.Find("HandTipRight");
        }
        else
        {

            Debug.Log(RightHand.transform.position);
            gameObject.transform.position = new Vector3(-RightHand.transform.position.x,
            RightHand.transform.position.y,
            RightHand.transform.position.z
            ) + OriginalPosition;
        }
    }

    //void Move(float h, float v)
    //{
    //    // Set the movement vector based on the axis input.
    //    movement.Set(h, 0f, v);

    //    // Normalise the movement vector and make it proportional to the speed per second.
    //    movement = movement.normalized * speed * Time.deltaTime;

    //    // Move the player to it's current position plus the movement.
    //    playerRigidbody.MovePosition(transform.position + movement);
    //}

    //void Turning()
    //{
    //    // Create a ray from the mouse cursor on screen in the direction of the camera.
    //    Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

    //    // Create a RaycastHit variable to store information about what was hit by the ray.
    //    RaycastHit floorHit;

    //    // Perform the raycast and if it hits something on the floor layer...
    //    if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
    //    {
    //        // Create a vector from the player to the point on the floor the raycast from the mouse hit.
    //        Vector3 playerToMouse = floorHit.point - transform.position;

    //        // Ensure the vector is entirely along the floor plane.
    //        playerToMouse.y = 0f;

    //        // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
    //        Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

    //        // Set the player's rotation to this new rotation.
    //        playerRigidbody.MoveRotation(newRotation);
    //    }
    //}
}
