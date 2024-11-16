using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public NavmeshFollow follow;
    public Transform intruder;
    public Transform waypoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(intruder.position, transform.position) < 15)
        {
            follow.target = intruder;            
        }
        else
        {
            follow.target = waypoint;
        }
    }
}
