using UnityEngine;

public class EventCamera : MonoBehaviour
{
    public float time = 3;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        time = time - Time.deltaTime;

        if(time < 0)
        {
            gameObject.SetActive(false);
        }
    }
}
