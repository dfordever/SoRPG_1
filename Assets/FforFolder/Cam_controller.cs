using UnityEngine;
using System.Collections;
using BeardedManStudios.Forge.Networking.Generated;

public class Cam_controller : MonoBehaviour
{


    public Transform lookAt;
    public Transform camTransform;
    private Camera cam;
    private float distance = 10.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float sensitivityX = 3.0f;
    private float sensitivityT = 4.0f;
    private const float Y_angle_min = -45.0f;
    private const float Y_angle_max = 50.0f;

    private void Start()
    {
       // if (!networkObject.IsOwner)
      //  {
      //      return;
      //  }
       
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
       // if (!networkObject.IsOwner)
     //   {
     //       return;
     //   }
        currentX += Input.GetAxis("Mouse X")* sensitivityX;
        currentY += -Input.GetAxis("Mouse Y");
        currentY = Mathf.Clamp(currentY, Y_angle_min, Y_angle_max);
    }
    private void LateUpdate()
    {
     //   if (!networkObject.IsOwner)
      //  {
       //     return;
        //}
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(lookAt.position);
    }

}
