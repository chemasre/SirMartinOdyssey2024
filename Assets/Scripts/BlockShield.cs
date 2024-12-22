using UnityEngine;

public class BlockShield : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Block()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Movement"))
        {
            animator.SetTrigger("Block");
        }
    }
}
