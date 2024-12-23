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
        hitSensor.gameObject.SetActive(false);

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("FirePrepare"))
        {
            hitSensor.gameObject.SetActive(true);
        }

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("FireHold"))
        {
            hitSensor.gameObject.SetActive(true);
        }

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("FireStart"))
        {
            hitSensor.gameObject.SetActive(true);
        }

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Movement"))
        {
            hitSensor.gameObject.SetActive(true);
        }

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Slash"))
        {
            hitSensor.gameObject.SetActive(true);
        }


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
