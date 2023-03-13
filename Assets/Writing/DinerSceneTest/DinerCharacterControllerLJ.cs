using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinerCharacterControllerLJ : MonoBehaviour
{
    //quick sketch up of a character controller for the diner scene. 
    //This SHOULDNT be used in the final version of the game, its messy and buggy. Its purpose is for the demo scene and for having something whilst i put the diner scene together

    //Player moves in a direction corrosponding to another objects transform (this could be the camera. or it could be something that defines a constant forward angle, as we cant be sure we always want the world forward)
    //slow the player down if they approach a wall or character
    //note to self. I could try my "dissasociation character controller" for this game? idk if its too avant garde 



    [Header("Game Objects")]
    [SerializeField] Transform myCamera;
    [SerializeField, Tooltip("Holding forward will move the player in the direction this object faces. Make it the camera to follow the camera")] Transform referenceForward;//this should rly be the camera i think. feels way better!
    [SerializeField] CharacterController playerBody;

    [Header("Movement Values")]
    [SerializeField] float movementSpeed = 11f;
    [SerializeField] float obstacleDetectionDistance = 1f;
    [SerializeField] LayerMask obstacleLayerMask;

    [Header("Camera")]
    [SerializeField] Vector2 cameraRestraints;

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        //i think i need to make sure the reference forward isnt pointing upwards at all. cus the player ends up turning upwards 
        Vector3 moveDirection = moveX * referenceForward.right + moveZ * referenceForward.forward;


        if (moveDirection.magnitude > 1f)
            moveDirection = moveDirection.normalized;

        playerBody.transform.LookAt(transform.position + moveDirection);


        float currentSpeed = movementSpeed;
        RaycastHit hit;
        Physics.Raycast(playerBody.transform.position, moveDirection, out hit, obstacleDetectionDistance, obstacleLayerMask);
        if (hit.collider != null)
            currentSpeed = movementSpeed * (hit.distance / obstacleDetectionDistance);

        playerBody.Move(moveDirection * currentSpeed * Time.deltaTime);

        ControlCameraAngle();
        //myCamera.LookAt(playerBody.transform);//This could be massivly improved. for one i want a camera clamp, and i only want it rotating along one axis. but figuring out rotations is complicated 
    }

    void ControlCameraAngle()
    {
        Vector3 targetPos = new Vector3(playerBody.transform.position.x, myCamera.transform.position.y + 2f, playerBody.transform.position.z);
        myCamera.LookAt(targetPos);

        if (myCamera.eulerAngles.y < cameraRestraints.x)
        {
            myCamera.eulerAngles = new Vector3(0f, cameraRestraints.x, 0f);
        }
        if (myCamera.eulerAngles.y > cameraRestraints.y)
        {
            myCamera.eulerAngles = new Vector3(0f, cameraRestraints.y, 0f);
        }
    }
}
