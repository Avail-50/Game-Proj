using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Display : MonoBehaviour
{
    public GameObject player;
    private playerControl speed;
    public TMPro.TextMeshProUGUI speedDisplay;

    void Start()
    {
        speed = player.GetComponent<playerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        speedDisplay.text = "Speed: " + speed.curSpeed.ToString();
    }
}
