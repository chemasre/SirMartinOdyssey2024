using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMovement : MonoBehaviour
{
    public Animator animator;
    public Rigidbody rigid;
    public Sensor groundSensor;

    // 0 - Rotate and forward/lateral (Controles tipo tanque)
    // 1 - Direction and forward (Controles tipo tercera persona)
    // 2 - Direction and lateral (Controles tipo primera persona)
    public int controlType = 0;

    public float forward;
    public float rotate;
    public float lateral;

    public Vector3 direction;

    float jumpCooldown;

    // Start is called before the first frame update
    void Start()
    {
        rigid.maxAngularVelocity = 2;
    }

    // Update is called once per frame
    void Update()
    {
        // Esto es para que el personaje no camine mientras cae

        // Guardamos la velocidad actual
        Vector3 velocity = rigid.velocity;

        // Guardamos la velocidad Y que tuviéramos
        float velocityY = velocity.y;

        // Ponemos como velocidad Y 0 
        velocity.y = 0;

        // Ahora que la velocidad Y es 0, esto nos dará la velocidad
        // del personaje sin tener en cuenta si cae o no
        if (velocity.magnitude > 7)
        {
            velocity = velocity.normalized * 7;
        }

        // Pasamos el valor de la velocidad al animator
        animator.SetFloat("Speed", velocity.magnitude);

        // Si estamos cayendo, pasamos la velocidad Y como fall speed
        if(groundSensor.presence == false || jumpCooldown > 0)
        { animator.SetFloat("FallSpeed", velocityY); }
        else
        { animator.SetFloat("FallSpeed", 0); }

        // Volvemos a poner la velocidad Y para que siga cayendo
        velocity.y = velocityY;
        rigid.velocity = velocity;

        // Actualizamos el cooldown

        jumpCooldown = jumpCooldown - Time.deltaTime;

    }

    void FixedUpdate()
    {
        if(controlType == 0)
        {
            rigid.AddForce(transform.right * lateral * 70);
            rigid.AddForce(transform.forward * forward * 70);
            rigid.AddTorque(Vector3.up * rotate * 3);
        }
        else if(controlType == 1)
        {
            if(forward > 0)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);

                rigid.MoveRotation(lookRotation);
                rigid.AddForce(transform.forward * forward * 70);
            }
        }
        else // controlType == 2
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            rigid.MoveRotation(lookRotation);
            rigid.AddForce(transform.forward * forward * 70);
            rigid.AddForce(transform.right * lateral * 70);
        }
    }

    public void Jump()
    {
        if(groundSensor.presence && jumpCooldown <= 0)
        {
            rigid.AddForce(Vector3.up * 30, ForceMode.VelocityChange);
            jumpCooldown = 0.3f;
        }
    }
}
