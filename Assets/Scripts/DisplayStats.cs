using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayStats : MonoBehaviour
{
    public GameObject player;
    private playerControl speed;
    //private playerControl momentum;
    public TMPro.TextMeshProUGUI speedDisplay;

    void Start()
    {
        speed = player.GetComponent<playerControl>();
        //speed = player.GetComponent<playerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        speedDisplay.text = "Speed: " + speed.curSpeed.ToString() +"\nMomentum: " + speed.momentum.ToString();
    }
}