using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SirMartin : MonoBehaviour
{
    public PhysicsMovement movement;
    public AttackSword attack;
    public BlockShield block;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            Time.timeScale = 2;
        }
        else
        {
            Time.timeScale = 1;
        }

        movement.forward = Input.GetAxis("Vertical");
        movement.rotate = Input.GetAxis("Horizontal");

        if(Input.GetButtonDown("Fire1"))
        {
            attack.Attack();
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            block.Block();
        }

    }
}
