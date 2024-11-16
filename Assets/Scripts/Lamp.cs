using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    public Transform light;

    public bool on;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(on)
        {
            light.gameObject.SetActive(true);
        }
        else
        {
            light.gameObject.SetActive(false);
        }
    }
}
