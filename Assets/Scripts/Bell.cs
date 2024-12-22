using UnityEngine;

public class Bell : MonoBehaviour
{
    public Sensor interactionSensor;
    public Animator animator;
    public AudioSource sound;

    public bool isRinging;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(interactionSensor.entered)
        {
            animator.SetTrigger("Move");
            sound.Play();
        }

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Moving"))
        {
            isRinging = true;
        }
        else
        {
            isRinging = false;
        }

    }
}
