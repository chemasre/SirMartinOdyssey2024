using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFire : MonoBehaviour
{
    public Transform fireEffect;
    public Animator animator;

    public Transform attackArea;

    public bool testAttack;

    public float prepareTimer;
    public float fireTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("FireProgress"))
        {
            fireEffect.gameObject.SetActive(true);
            attackArea.gameObject.SetActive(true);
        }
        else
        {
            fireEffect.gameObject.SetActive(false);
            attackArea.gameObject.SetActive(false);
        }

        if (testAttack)
        {
            Attack();

            testAttack = false;
        }

        prepareTimer = prepareTimer - Time.deltaTime;
        fireTimer = fireTimer - Time.deltaTime;

        if(prepareTimer <= 0)
        {
            animator.SetBool("FirePrepareHold", false);
        }

        if (fireTimer <= 0)
        {
            animator.SetBool("FireAttackHold", false);
        }
    }

    public void Attack()
    {
        animator.SetTrigger("AttackFire");
        animator.SetBool("FirePrepareHold", true);
        animator.SetBool("FireAttackHold", true);
        prepareTimer = 0.2f;
        fireTimer = 3;
    }

}
