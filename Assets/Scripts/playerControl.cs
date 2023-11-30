using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float forBack = Input.GetAxis("Vertical") * speed;
        float leftRight = Input.GetAxis("Horizontal") * speed;


        Vector3 movement = new Vector3(leftRight, 0, forBack);
        movement = Vector3.ClampMagnitude(movement, 1);
        transform.Translate(movement);
    }
}
