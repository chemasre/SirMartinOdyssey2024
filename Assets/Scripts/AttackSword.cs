using UnityEngine;

public class AttackSword : MonoBehaviour
{
    public Animator animator;
    public Transform attackArea;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(1).IsName("Attack"))
        {
            attackArea.gameObject.SetActive(true);
        }
        else
        {
            attackArea.gameObject.SetActive(false);
        }
    }

    public void Attack()
    {
        if (animator.GetCurrentAnimatorStateInfo(1).IsName("Nothing"))
        {
            animator.SetTrigger("Attack");
        }
    }


}
