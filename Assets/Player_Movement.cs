using UnityEngine;
using System.Collections;
using BeardedManStudios.Forge.Networking.Generated;

//[RequireComponent(typeof(CharacterController))]

public class Player_Movement : PlayerCubeBehavior
{

    public float speed = 3.0F;
    public float rotateSpeed = 3.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    CharacterController controller;
    public GameObject playerObj;
    public GameObject cameraObj;
    // Use this for initialization
    protected override void NetworkStart()
    {
        base.NetworkStart();

        if (!networkObject.IsOwner)
        {
            return;
        }
        controller = playerObj.GetComponent<CharacterController>();

    }
	
	// Update is called once per frame
	void Update () {
        if(!networkObject.IsOwner)
        {
            playerObj.transform.position = networkObject.position;
            playerObj.transform.rotation = networkObject.rotation;
            return;
        }
        Debug.Log("sdafasdf");

        if (controller.isGrounded)
            {
           
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                moveDirection = playerObj.transform.TransformDirection(moveDirection);
                moveDirection *= speed;
                if (Input.GetButton("Jump"))
                    moveDirection.y = jumpSpeed;
            }
            playerObj.transform.Rotate(0, Input.GetAxis("Mouse X") * rotateSpeed, 0);
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
            networkObject.position = playerObj.transform.position;
            networkObject.rotation = playerObj.transform.rotation;
        
    }
}
