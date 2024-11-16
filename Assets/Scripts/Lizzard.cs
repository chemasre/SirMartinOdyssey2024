using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizzard : MonoBehaviour
{
    public AttackFire attackFire;
    public AttackSlash attackSlash;
    public NavmeshFollow follow;
    public Fall fall;
    public Sensor attackSensor;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Movement"))
        {
            // Estamos en movement
            Debug.Log("Estamos en state movement");
            follow.pause = false;

            if(attackSensor.presence == true)
            {
                if(Random.Range(0,2) == 0)
                {
                    attackSlash.Attack();
                }
                else
                {
                    attackFire.Attack();
                }
            }

        }
        else
        {
            Debug.Log("No estamos en state movement");
            follow.pause = true;

        }
    }
}
