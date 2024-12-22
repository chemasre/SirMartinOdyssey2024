using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform blocker;
    public Animator animator;
    public bool opened;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(opened)
        {
            blocker.gameObject.SetActive(false);
        }
        else
        {
            blocker.gameObject.SetActive(true);
        }

        animator.SetBool("Opened", opened);
    }
}
