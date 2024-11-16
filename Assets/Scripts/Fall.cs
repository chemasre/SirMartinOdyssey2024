using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    public Animator animator;
    public Sensor hitSensor;

    public float holdTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hitSensor.entered)
        {
            StartFall();
        }

        holdTimer = holdTimer - Time.deltaTime;

        if(holdTimer > 0)
        {
            animator.SetBool("FallHold", true);
        }
        else
        {
            animator.SetBool("FallHold", false);
        }

    }

    void StartFall()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Movement"))
        {
            animator.SetTrigger("Fall");
            holdTimer = 4;
        }
    }
}
