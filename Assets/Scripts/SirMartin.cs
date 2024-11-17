using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SirMartin : MonoBehaviour
{

    // 0 - Rotate and forward (Controles tipo tanque)
    // 1 - Direction and forward (Controles tipo tercera persona)
    public int controlType = 0;
    public Transform mainCamera;

    public PhysicsMovement movement;
    public AttackSword attack;
    public BlockShield block;

    public CameraSelector cameraSelector;


    // Start is called before the first frame update
    void Start()
    {
        controlType = 1;
        cameraSelector.selected = 0;

        Cursor.lockState = CursorLockMode.Locked;
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

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            controlType = 1;
            cameraSelector.selected = 0;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            controlType = 0;
            cameraSelector.selected = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            controlType = 1;
            cameraSelector.selected = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            controlType = 2;
            cameraSelector.selected = 3;
        }

        float inputVertical = Input.GetAxis("Vertical");
        float inputHorizontal = Input.GetAxis("Horizontal");

        if (controlType == 0)
        {
            movement.controlType = 0;

            if(Input.GetButton("Fire2"))
            {
                movement.rotate = 0;
                movement.lateral = inputHorizontal;
            }
            else
            {
                movement.rotate = inputHorizontal;
                movement.lateral = 0;
            }

            movement.forward = inputVertical;
        }
        else if(controlType == 1)
        {
            movement.controlType = 1;

            Vector3 inputDirection = new Vector3(inputHorizontal, 0, inputVertical);
            float inputMagnitude = inputDirection.magnitude;

            Vector3 cameraDirection = mainCamera.TransformDirection(inputDirection);

            Vector3 movementDirection = new Vector3(cameraDirection.x, 0, cameraDirection.z);
            movementDirection = movementDirection.normalized * inputMagnitude;

            if(inputMagnitude > 0) { movement.direction = movementDirection; }
            movement.forward = inputMagnitude;
        }
        else
        {
            movement.controlType = 2;

            Vector3 cameraForward = new Vector3(0, 0, 1);
            Vector3 cameraDirection = mainCamera.TransformDirection(cameraForward);
            Vector3 movementDirection = new Vector3(cameraDirection.x, 0, cameraDirection.z);
            movementDirection = movementDirection.normalized;
            movement.direction = movementDirection;
            movement.forward = inputVertical;
            movement.lateral = inputHorizontal;
        }

        if (Input.GetButtonDown("Jump"))
        {
            movement.Jump();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            attack.Attack();
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            block.Block();
        }

    }

    public void CinemachineCameraActivated(ICinemachineCamera next, ICinemachineCamera previous)
    {
        Debug.Log("Activated camera " + next.Name);

        if(next.Priority == 20)
        {
            controlType = 1;
        }
        else if(next.Priority == 10)
        {
            if (cameraSelector.selected == 0)
            {
                controlType = 1;
            }
            else if (cameraSelector.selected == 1)
            {
                controlType = 0;
            }
            else if (cameraSelector.selected == 2)
            {
                controlType = 1;
            }
            else // cameraSelector.selected == 3
            {
                controlType = 2;
            }
        }
    }
}
