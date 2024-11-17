using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSelector : MonoBehaviour
{
    public Transform camera1;
    public Transform camera2;
    public Transform camera3;
    public Transform camera4;

    public int selected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        camera1.gameObject.SetActive(false);
        camera2.gameObject.SetActive(false);
        camera3.gameObject.SetActive(false);
        camera4.gameObject.SetActive(false);

        if (selected == 0) { camera1.gameObject.SetActive(true); }
        else if(selected == 1) { camera2.gameObject.SetActive(true); }
        else if (selected == 2) { camera3.gameObject.SetActive(true); }
        else // selected == 3
        { camera4.gameObject.SetActive(true); }
    }
}
