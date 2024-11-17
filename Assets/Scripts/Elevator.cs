using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Switch controlSwitch;
    public Animator animator;
    public Sensor sensorUp;
    public Sensor sensorDown;
    public Sensor sensorCabin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(sensorCabin.presence)
        {
            if (controlSwitch.on) { animator.SetBool("Up", true); }
            else { animator.SetBool("Up", false); }
        }
        else
        {
            if (sensorUp.presence) { animator.SetBool("Up", true); controlSwitch.on = true; }
            else if (sensorDown.presence) { animator.SetBool("Up", false); controlSwitch.on = false; }

        }
    }
}
