using UnityEngine;
using UnityEngine.AI;

public class NavmeshFollow : MonoBehaviour
{
    public bool pause;

    public Transform target;

    public NavMeshAgent agent;

    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pause)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }

        agent.SetDestination(target.position);

        animator.SetFloat("Speed", agent.velocity.magnitude);
    }
}
