using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public Animator animator;
    public Sensor hitSensor;

    public bool testHit;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(testHit)
        {
            ReceiveHit();
        }

        if(hitSensor.entered)
        {
            ReceiveHit();
        }

        testHit = false;
    }

    public void ReceiveHit()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Movement"))
        {
            animator.SetTrigger("Hit");
        }
    }
}
