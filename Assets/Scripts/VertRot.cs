using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertRot : MonoBehaviour
{
    public float mouseSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float v = mouseSpeed * -Input.GetAxis("Mouse Y");
        transform.Rotate(v, 0, 0, Space.Self);
    }
}
