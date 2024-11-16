using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Animator animator;
    public Sensor sensor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(sensor.entered)
        {
            animator.SetTrigger("Next");
        }
    }
}
