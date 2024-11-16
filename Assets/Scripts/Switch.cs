using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public Sensor sensor;
    public Animator animator;

    public bool on;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(sensor.entered)
        {
            if(on)
            {
                on = false;
            }
            else
            {
                on = true;
            }
        }

        animator.SetBool("On", on);

    }
}
