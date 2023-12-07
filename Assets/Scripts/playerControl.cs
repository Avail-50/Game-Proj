using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{

    public float speed;


    public Vector3 boxSize;
    public float maxDistance;
    public LayerMask layerMask;
    public float jumpSpeed;
    public float mouseSpeed;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //wasd keys
        float forBack = Input.GetAxis("Vertical") * speed;
        float leftRight = Input.GetAxis("Horizontal") * speed;

        //mouse rotation
        //float v = mouseSpeed * -Input.GetAxis("Mouse Y");
        float h = mouseSpeed * Input.GetAxis("Mouse X");
        //float angle = (v + h)

        // jump
        if (Input.GetKeyDown(KeyCode.Space) && GroundCheck())
        {
            Debug.Log("jump true");
            rb.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);

        }


        forBack *= Time.deltaTime;
        leftRight *= Time.deltaTime;
        transform.Translate(leftRight, 0, forBack);
        transform.Rotate(0, h, 0, Space.Self);

        //locking and unlocking cursor
        if (Input.GetKey(KeyCode.Escape))
            Cursor.lockState = CursorLockMode.None;
        else if (Cursor.lockState == CursorLockMode.None && Input.GetMouseButtonDown(0))
            Cursor.lockState = CursorLockMode.Locked;
    }

    bool GroundCheck()
    {
        //creates boxcast below the cube to check if it is on the ground
        if (Physics.BoxCast(transform.position - transform.up * 0.7f, boxSize, -transform.up, transform.rotation, maxDistance, layerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
