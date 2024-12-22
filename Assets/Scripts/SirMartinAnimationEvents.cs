using UnityEngine;

public class SirMartinAnimationEvents : MonoBehaviour
{
    public Animator animator;
    public AudioSource attackSound;
    public AudioSource stepSound1;
    public AudioSource stepSound2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAttackSound()
    {
        attackSound.Play();
    }

    public void PlayStepSound1()
    {
        if(stepSound1.isPlaying == false && animator.GetFloat("Speed") > 0.3f)
        {
            stepSound1.Play();
        }

    }

    public void PlayStepSound2()
    {
        if(stepSound2.isPlaying == false && animator.GetFloat("Speed") > 0.3f)
        {
            stepSound2.Play();
        }

    }
}
