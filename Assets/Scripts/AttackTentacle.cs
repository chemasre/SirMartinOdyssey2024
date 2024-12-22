using UnityEngine;

public class AttackTentacle : MonoBehaviour
{
    public Sensor attackSensor;
    public Transform attackArea;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            attackArea.gameObject.SetActive(true);
        }
        else
        {
            attackArea.gameObject.SetActive(false);
        }

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Agressive"))
        {
            attackSensor.gameObject.SetActive(true);
        }
        else
        {
            attackSensor.gameObject.SetActive(false);
        }

        if (attackSensor.entered)
        {
            animator.SetTrigger("Attack");
        }
    }
}
