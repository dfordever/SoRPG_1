using UnityEngine;
using System.Collections;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
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
            //Если не владеем объектом то ничего не делаем
            return;
        }
        controller = playerObj.GetComponent<CharacterController>();

    }
   

    // Update is called once per frame
    void Update () {
        
        if(!networkObject.IsOwner)
        {
            //Если код считывается не на владельце этого объекта то выполнить только синхронизацию положения с сервера
            playerObj.transform.position = networkObject.position;
            playerObj.transform.rotation = networkObject.rotation;
            return;
        }
        // Если владеем объектом то просчитываем его передвижения и  отправляем наше положение на сервер
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
    // Этот оверрайд почему то необходимо прописывать для всех классов которые являются дочерними классами для PlayerCubeBehavior , используется только в атак системе
    public override void Attack(RpcArgs args)
    {
        throw new System.NotImplementedException();
    }



}
