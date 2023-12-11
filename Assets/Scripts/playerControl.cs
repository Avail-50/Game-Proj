using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    //movement
    public float startMoveSpeed;
    public float moveSpeed;
    public Transform orientation;
    private float forBack;
    private float leftRight;
    private Vector3 mDirection;
    public float groundDrag;
    public float airDrag;
    public float curSpeed; //displaying speed on the screen

    //jump + groundCheck
    public Vector3 boxSize;
    public float maxDistance;
    public LayerMask layerMask;
    public float jumpSpeed;
    private bool grounded;

    //dash
    [SerializeField] private float dashCooldown;
    private CooldownTimer dashTimer;
    public float dashSpeed;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        moveSpeed = startMoveSpeed;

        //Dash cooldown
        dashTimer = new CooldownTimer(this, dashCooldown);
        dashTimer.OnStart += (object sender, System.EventArgs e) => Dash();
    }

    // Update is called once per frame
    void Update()
    {
        //locking and unlocking cursor
        if (Input.GetKey(KeyCode.Escape))
            Cursor.lockState = CursorLockMode.None;
        else if (Cursor.lockState == CursorLockMode.None && Input.GetMouseButtonDown(0))
            Cursor.lockState = CursorLockMode.Locked;

        //wasd keys
        forBack = Input.GetAxisRaw("Vertical");
        leftRight = Input.GetAxisRaw("Horizontal");

        grounded = GroundCheck();
        
        
        if (Input.GetKeyDown(KeyCode.Space) && grounded) //jump
        {
            rb.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift)) //dash
        {
            dashTimer.Activate();
        }//else if (roll)
        else if(grounded && (rb.velocity.magnitude == 0)) //resets momentum
        {
            moveSpeed = startMoveSpeed;
        }
        
        //drag
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }

        curSpeed = rb.velocity.magnitude;
        
    }

    void FixedUpdate() 
    {
        //move
        mDirection = orientation.forward * forBack + orientation.right * leftRight;
        if (GroundCheck())
            rb.AddForce(mDirection.normalized * moveSpeed, ForceMode.Force);
        else if (!GroundCheck())
            rb.AddForce(mDirection.normalized * moveSpeed * airDrag, ForceMode.Force);

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

    void Dash() 
    {
        moveSpeed += 5;
        rb.AddForce(orientation.forward * dashSpeed, ForceMode.Impulse);
    }
}
