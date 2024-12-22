using UnityEngine;

public class AttackSlash : MonoBehaviour
{
    public Animator animator;

    public Transform attackArea;

    public bool testAttack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(testAttack)
        {
            Attack();

            testAttack = false;
        }

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Slash"))       
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
        animator.SetTrigger("AttackSlash");
    }
}
