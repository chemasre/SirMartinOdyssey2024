using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class SirMartin : MonoBehaviour
{
    // Tipos de control
    // 0 - Teclas AD controlan el rotate y WS el forward (Controles tipo tanque)
    // 1 - Teclas WASD se aplican a la direcci�n y al forward seg�n la c�mara (Controles tipo tercera persona)
    // 2 - Teclas AD controlan el lateral, WS el forward y la direcci�n la tomamos de la c�mara (Control tipo primera persona)
    public int controlType = 0;
    public Transform mainCamera;

    public PhysicsMovement movement;
    public AttackSword attack;
    public BlockShield block;

    public CameraSelector cameraSelector;

    // Direcci�n en que mira la c�mara
    Vector3 cameraDirection;

    // Direcci�n en que el jugador mueve el mando
    Vector3 inputDirection;

    // Esto controla que el jugador se mantenga mirando en
    // la misma direcci�n cuando hay un cambio brusco de c�mara
    // y estamos en modo tercera persona
    bool holdCameraDirection;
    Vector3 holdedCameraDirection;
    Vector3 holdedInputDirection;

    // Esto permite bloquear el input cuando hay una c�mara
    // de evento activa
    bool freezeControls;

    // Start is called before the first frame update
    void Start()
    {
        controlType = 1;
        cameraSelector.selected = 0;

        Cursor.lockState = CursorLockMode.Locked;

        freezeControls = false;
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

        // En esta secci�n miramos si el jugador
        // quiere cambiar la c�mara

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            // El jugador quiere la c�mara a�rea

            controlType = 1; // seleccionamos control en 3a persona
            cameraSelector.selected = 0;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            // El jugador quiere la c�mara follow

            controlType = 0; // seleccionamos control tipo tanque
            cameraSelector.selected = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // El jugador quiere la c�mara orbital

            controlType = 1; // seleccionamos control en 3a persona
            cameraSelector.selected = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            // El jugador quiere la c�mara POV

            controlType = 2; // seleccionamos control tipo 1a persona
            cameraSelector.selected = 3;
        }

        // En esta secci�n obtenemos el input del jugador

        float inputVertical;
        float inputHorizontal;

        if (freezeControls)
        {
            // En este momento queremos que el personaje
            // no responda a los movimientos del jugador
            inputVertical = 0;
            inputHorizontal = 0;
        }
        else
        {
            // Obtenemos el input
            inputVertical = Input.GetAxis("Vertical");
            inputHorizontal = Input.GetAxis("Horizontal");
        }

        // A partir de aqu� interpretamos el input del jugador
        // de una forma diferente seg�n el modo de control

        if (controlType == 0)
        {
            // Estamos en modo tanque

            movement.controlType = 0;


            if(Input.GetButton("Fire2"))
            {
                // Si el jugador tiene pulsado el bot�n secundario
                // hacemos que el input horizontal se use para moverlo
                // hacia los lados

                movement.rotate = 0;
                movement.lateral = inputHorizontal;
            }
            else
            {
                // Si el jugador no tiene pulsado el bot�n secundario
                // hacemos que el input horizontal se use para girarlo

                movement.rotate = inputHorizontal;
                movement.lateral = 0;
            }

            // El input vertical en este modo siempre lo usamos para moverlo
            // hacia adelante o atr�s

            movement.forward = inputVertical;
        }
        else if(controlType == 1)
        {
            // Este es el control en 3a persona que con diferencia es el m�s dif�cil

            movement.controlType = 1;

            // Primero obtenemos un vector que indica hacia d�nde est� moviendo el mando.
            // como el jugador s�lo puede moverse en el plano XZ ponemos un 0 en la Y.
            // A este vector lo llamaremos el input del jugador.

            inputDirection = new Vector3(inputHorizontal, 0, inputVertical);
            float inputMagnitude = inputDirection.magnitude;

            // Este paso es la clave. Cogemos el input del jugador y le preguntamos
            // a la c�mara a qu� direcci�n en el sistema global corresponde la
            // direcci�n del input interpretada seg�n su sistema local.
            // Dicho de otro modo, le preguntamos a la c�mara a qu� direcci�n del sistema
            // global corresponde el input del jugador desde su punto de vista.

            cameraDirection = mainCamera.TransformDirection(inputDirection);

            // Esto es un peque�o sistema para que cuando hay un cambio de c�mara el jugador
            // mantenga la direcci�n que le daba la camara anterior
            if (holdCameraDirection) { cameraDirection = holdedCameraDirection; }

            // Como la c�mara puede estar haciendo un picado o un contrapicado y queremos que
            // el jugador s�lo se mueva en el plano XZ, tenemos que ignorar la coordenada Y.
            // El problema es que al hacer esto el vector se nos puede haber acortado. Si lo dej�semos
            // as� el jugador se mover�a m�s lentamente cuando la c�mara est� en un picado o contrapicado.
            // Para evitarlo, lo normalizamos (es decir, le damos longitud 1) y lo multiplicamos por
            // la longitud que ten�a el vector en el que guardamos el input.

            Vector3 movementDirection = new Vector3(cameraDirection.x, 0, cameraDirection.z);
            movementDirection = movementDirection.normalized * inputMagnitude;

            // Ahora le pasamos al script que controla el movimiento del jugador la direcci�n
            // que hemos calculado y cuanto tiene que moverse en esa direcci�n.
            // el if lo ponemos para que si el jugador no toca el mando el personaje se quede
            // mirando hacia la �ltima direcci�n que le hab�amos pasado

            if(inputMagnitude > 0) { movement.direction = movementDirection; }
            movement.forward = inputMagnitude;


            // Esta �ltima parte es para ver si ya no es necesario seguir aguantando la
            // direcci�n de la c�mara anterior. Hay bastantes casos en que dejaremos de hacerlo
            if (holdCameraDirection)
            {
                // Si la direcci�n que estamos manteniendo es cero,
                // dejamos de mantenerla
                if (holdedCameraDirection.magnitude == 0) { holdCameraDirection = false;  }

                // Si el jugador no estaba tocando el mando cuando empezamos a mantener la
                // direcci�n de c�mara, dejamos de hacerlo
                if (holdedInputDirection.magnitude == 0) { holdCameraDirection = false; }

                // Si el jugador ha cambiado bastante la direcci�n de su input,
                // dejamos de mantener la direcci�n de c�mara anterior
                if (Vector3.Angle(holdedInputDirection, inputDirection) > 15) { holdCameraDirection = false;  }

                // Si el jugador ha cambiado bastante la intensidad de su input,
                // dejamos de mantener la direcci�n de c�mara anterior
                if(Mathf.Abs(holdedInputDirection.magnitude - inputDirection.magnitude) > 0.8f) { holdCameraDirection = false; }
            }
        }
        else
        {
            // Este es el modo de control en 1a persona

            movement.controlType = 2;

            // Aqu� la direcci�n en la que mira la c�mara marca la direcci�n hacia la que
            // mirar� el personaje.

            // Lo primero que pedimos es hacia d�nde est� mirando la direcci�n Z de la c�mara interpretada
            // en coordenadas del sistema global
            Vector3 cameraForward = new Vector3(0, 0, 1);
            cameraDirection = mainCamera.TransformDirection(cameraForward);

            // Como la c�mara puede estar haciendo un picado o contrapicado, y el jugador
            // solo puede mover al personaje en el plano ignoramos la coordenada Y
            Vector3 movementDirection = new Vector3(cameraDirection.x, 0, cameraDirection.z);

            // Como al hacer esto podemos haber hecho que el vector sea m�s corto, le
            // volvemos a dar tama�o 1 con la operaci�n de normalizaci�n
            movementDirection = movementDirection.normalized;

            // Ahora le pasamos al controlador de movimiento del personaje
            // la direcci�n que hemos calculado y cu�nto queremos que se mueva
            // hacia adelante y hacia los lados
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
        // Este c�digo se ejecuta cada vez que hay un cambio de c�mara. En la variable activated
        // recibimos la c�mara que se ha activado

        Debug.Log("Activated camera " + activated.Name);

        // Realmente lo que recibimos en el activated no es la c�mara exactamente pero lo podemos
        // transformar en la c�mara mediante esta operaci�n que se llama "casting". Esta operaci�n
        // es un poco complicada pero la pod�is imaginar de la siguiente forma. El sistema nos da
        // un objeto que podr�a ser una c�mara o no pero nosotros s� sabemos que es
        // una c�mara por lo que convertimos a ese tipo.
        // Es un poco como si un polic�a encontrara un ni�o por la calle y lo devolviera a
        // su madre. La madre recibe un ni�o pero ella sabe que es su hijo.

        CinemachineVirtualCameraBase activatedCamera = (CinemachineVirtualCameraBase)activated;

        if(activatedCamera.Priority.Value == 30)
        {
            // Si la c�mara que se ha activado es de prioridad 30 quiere decir que es una c�mara de evento
            // y mientras est� activa no queremos que el jugador se mueva
            freezeControls = true;
        }
        else if (activatedCamera.Priority.Value == 20)
        {
            // Si la c�mara que se ha activado es de prioridad 20 quiere decir que es una c�mara de nivel.
            // Como no queremos que el personaje empiece a moverse en una direcci�n distinta de golpe por
            // el cambio de c�mara, mantendremos la direcci�n de la c�mara anterior.
            // Nos guardamos tambi�n hacia d�nde est� moviendo el mando ahora para que cuando lo mueva en
            // una direcci�n distinta, dejemos mantener la direcci�n.
            holdCameraDirection = true;
            holdedCameraDirection = cameraDirection;
            holdedInputDirection = inputDirection;
            freezeControls = false;
            controlType = 1;
        }
        else if (activatedCamera.Priority.Value == 10 || activatedCamera.Priority.Value == 25)
        {
            // Se ha activado una c�mara de jugador

            freezeControls = false;

            if (cameraSelector.selected == 0)
            {
                // La c�mara que se ha activado es la aerial, que se controla en tercera persona.
                // Como no queremos que el personaje empiece a moverse en una direcci�n distinta de golpe por
                // el cambio de c�mara, mantendremos la direcci�n de la c�mara anterior.
                // Nos guardamos tambi�n hacia d�nde est� moviendo el mando ahora para que cuando lo mueva en
                // una direcci�n distinta, dejemos mantener la direcci�n.

                holdCameraDirection = true;
                holdedCameraDirection = cameraDirection;
                holdedInputDirection = inputDirection;
                controlType = 1;
            }
            else if (cameraSelector.selected == 1)
            {
                // La c�mara que se ha activado es la follow.

                holdCameraDirection = false;
                controlType = 0;
            }
            else if (cameraSelector.selected == 2)
            {
                // La c�mara que se ha activado es la orbital, que se controla en tercera persona.
                // Como no queremos que el personaje empiece a moverse en una direcci�n distinta de golpe por
                // el cambio de c�mara, mantendremos la direcci�n de la c�mara anterior.
                // Nos guardamos tambi�n hacia d�nde est� moviendo el mando ahora para que cuando lo mueva en
                // una direcci�n distinta, dejemos mantener la direcci�n.

                holdCameraDirection = true;
                holdedCameraDirection = cameraDirection;
                holdedInputDirection = inputDirection;
                controlType = 1;
            }
            else // cameraSelector.selected == 3
            {
                // La c�mara que se ha activado es la POV.

                holdCameraDirection = false;
                controlType = 2;
            }
        }
    }
}
