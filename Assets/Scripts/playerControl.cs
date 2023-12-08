using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    //movement
    public float moveSpeed;
    public Transform orientation;
    private float forBack;
    private float leftRight;
    private Vector3 mDirection;
    public float groundDrag;
    public float airDrag;

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
        
        // jump
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dashTimer.Activate();
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
    }

    void FixedUpdate() 
    {
        //move
        mDirection = orientation.forward * forBack + orientation.right * leftRight;
        if (GroundCheck())
            rb.AddForce(mDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else if (!GroundCheck())
            rb.AddForce(mDirection.normalized * moveSpeed * 10f * airDrag, ForceMode.Force);

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
        //leftRight = 0;
        rb.AddForce(transform.forward * dashSpeed, ForceMode.Impulse);
    }
}
