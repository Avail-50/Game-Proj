using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headBob : MonoBehaviour
{

    public float bobSpeed;
    public float bobAmount;
    public float speed;
    public playerControl playerControl;

    float defaultY = 0;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        defaultY = transform.localPosition.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerControl.momentum > 1f && playerControl.grounded)
        {
            timer += bobSpeed;
            transform.localPosition = new Vector3(transform.localPosition.x, defaultY + Mathf.Sin(timer) * bobAmount, transform.localPosition.z);
        }
        else
        {
            timer = 0;
            //transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, defaultY, Time.deltaTime * bobSpeed), transform.localPosition.z);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(transform.localPosition.x, defaultY, transform.localPosition.z), speed);
        }
    }
}
