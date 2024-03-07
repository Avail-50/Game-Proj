using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertRot : MonoBehaviour
{
    public float mouseSpeedX;
    public float mouseSpeedY;

    public Transform orientation;

    float yRot;
    float xRot;
    float zRot;

    public float tiltAngle;
    private float reff; //just needed for smoothDampRotation. IDK why 
    public float tiltTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float v = mouseSpeed * -Input.GetAxis("Mouse Y");
        //transform.Rotate(v, 0, 0, Space.Self);

        float mouseX = Input.GetAxis("Mouse X") * mouseSpeedX;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSpeedY;
        
        float leftRight = Input.GetAxisRaw("Horizontal");

        yRot += mouseX;
        xRot -= mouseY;
        //to stop looking up/down to far and rotating back around
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        //camera tilt to feel FAAASST
        zRot = Mathf.SmoothDampAngle(transform.eulerAngles.z, tiltAngle * leftRight, ref reff, tiltTime);

        // Rotates camera
        transform.rotation = Quaternion.Euler(xRot, yRot, zRot);
        // Rotates player
        orientation.rotation = Quaternion.Euler(0, yRot, 0);
    }
}
