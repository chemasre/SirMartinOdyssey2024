using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public bool entered;
    public bool presence;

    public float presenceTimer;

    bool previousPresence;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        presenceTimer = presenceTimer - Time.deltaTime;

        if(presenceTimer <= 0)
        {
            presence = false;
        }

        if(presence == true && previousPresence == false)
        {
            entered = true;
        }
        else
        {
            entered = false;
        }

        previousPresence = presence;
    }

    void OnTriggerEnter(Collider other)
    {
        presence = true;
        presenceTimer = 0.2f;
    }
    void OnTriggerStay(Collider other)
    {
        presence = true;
        presenceTimer = 0.2f;

    }

    void OnTriggerExit(Collider other)
    {

    }
}
