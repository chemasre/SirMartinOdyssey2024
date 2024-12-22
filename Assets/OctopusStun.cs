using UnityEngine;

public class OctopusStun : MonoBehaviour
{
    public Bell bell;
    public Animator animator;

    float stunTimer;

    // Start is called before the first frame update
    void Start()
    {
        stunTimer = 0;    
    }

    // Update is called once per frame
    void Update()
    {
        if(bell.isRinging)
        {
            animator.SetBool("Stunned", true);
            stunTimer = 10;
        }

        stunTimer = stunTimer - Time.deltaTime;

        if(stunTimer > 0)
        {
            animator.SetBool("Stunned", true);
        }
        else
        {
            animator.SetBool("Stunned", false);
        }
    }
}
