using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

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

    Vector3 cameraDirection;
    float inputVertical;
    float inputHorizontal;
    bool holdCameraDirection;

    bool controlEnabled;

    // Start is called before the first frame update
    void Start()
    {
        controlType = 1;
        cameraSelector.selected = 0;

        Cursor.lockState = CursorLockMode.Locked;

        controlEnabled = true;
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

        if(inputVertical != Input.GetAxis("Vertical") || inputHorizontal != Input.GetAxis("Horizontal"))
        {
            holdCameraDirection = false;
        }

        if(controlEnabled)
        {
            inputVertical = Input.GetAxis("Vertical");
            inputHorizontal = Input.GetAxis("Horizontal");
        }
        else
        {
            inputVertical = 0;
            inputHorizontal = 0;
        }


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

            if(holdCameraDirection == false)
            {
                cameraDirection = mainCamera.TransformDirection(inputDirection);
            }

            Vector3 movementDirection = new Vector3(cameraDirection.x, 0, cameraDirection.z);
            movementDirection = movementDirection.normalized * inputMagnitude;

            if(inputMagnitude > 0) { movement.direction = movementDirection; }
            movement.forward = inputMagnitude;
        }
        else
        {
            movement.controlType = 2;

            Vector3 cameraForward = new Vector3(0, 0, 1);

            if(holdCameraDirection == false)
            {
                cameraDirection = mainCamera.TransformDirection(cameraForward);
            }
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

    public void CinemachineCameraActivated(ICinemachineMixer mixer, ICinemachineCamera activated)
    {
        Debug.Log("Activated camera " + activated.Name);
        CinemachineVirtualCameraBase activatedCamera = (CinemachineVirtualCameraBase)activated;

        holdCameraDirection = true;

        if(activatedCamera.Priority.Value == 30)
        {
            controlEnabled = false;
        }
        else if (activatedCamera.Priority.Value == 20)
        {
            controlEnabled = true;
            controlType = 1;
        }
        else if (activatedCamera.Priority.Value == 10)
        {
            controlEnabled = true;

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
