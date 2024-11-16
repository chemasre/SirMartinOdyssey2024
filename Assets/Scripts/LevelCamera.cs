using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCamera : MonoBehaviour
{
    public Transform camera;
    public Sensor sensor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(sensor.presence)
        {
            camera.gameObject.SetActive(true);
        }
        else
        {
            camera.gameObject.SetActive(false);
        }
    }
}
