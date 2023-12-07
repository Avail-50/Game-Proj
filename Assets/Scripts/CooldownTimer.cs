using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CooldownTimer
{
    public float cooldown;
    private MonoBehaviour mono;

    public EventHandler OnStart;
    public EventHandler OnStop;

    // can be "get" by other objects, can only be "set" in this script
    public bool ready { get; private set; } = true;

    public CooldownTimer(MonoBehaviour calling_mono, float cooldown_time)
    {
        mono = calling_mono;
        cooldown = cooldown_time;
    }

    public bool Activate()
    {
        if (ready == true)
        {
            ready = false;
            Debug.Log("Dash True");
            mono.StartCoroutine(CountdownTimer());
            return true;
        }
        return false;
    }

    IEnumerator CountdownTimer()
    {
        OnStart?.Invoke(this, EventArgs.Empty);

        yield return new WaitForSeconds(cooldown);
        ready = true;

        OnStop?.Invoke(this, EventArgs.Empty);
    }


}
