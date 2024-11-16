using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMovement : MonoBehaviour
{
    public Transform pivot;
    public Animator animator;
    public Rigidbody rigid;

    public float forward;
    public float rotate;

    // Start is called before the first frame update
    void Start()
    {
        rigid.maxAngularVelocity = 2;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = rigid.velocity;
        float velocityY = velocity.y;
        velocity.y = 0;
        if(velocity.magnitude > 5)
        {
            velocity = velocity.normalized * 5;
        }

        animator.SetFloat("Speed", velocity.magnitude);

        velocity.y = velocityY;
        rigid.velocity = velocity;
    }

    void FixedUpdate()
    {
        rigid.AddForce(transform.forward * forward * 30);
        rigid.AddTorque(Vector3.up * rotate * 3);
    }
}
