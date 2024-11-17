using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizzardHit : MonoBehaviour
{
    public Animator animator;
    public Sensor hitSensor;

    public float holdTimer;

    public int hits;
    public GameObject dieParticlesPrefab;
    public GameObject hitParticlesPrefab;

    // Start is called before the first frame update
    void Start()
    {
        hits = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(hitSensor.entered)
        {
            ReceiveHit();
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

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
        {
            Destroy(gameObject);
        }

    }

    void ReceiveHit()
    {
        hits = hits - 1;

        if(hits > 0)
        {
            Instantiate(hitParticlesPrefab, transform.position, transform.rotation);
            animator.SetTrigger("Fall");
            holdTimer = 2;
        }
        else
        {
            animator.SetTrigger("Die");
            Instantiate(dieParticlesPrefab, transform.position, transform.rotation);
        }
    }
}
