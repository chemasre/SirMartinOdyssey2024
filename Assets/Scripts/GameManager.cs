using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int level = 1;

    [Header("Switches")]

    public Switch switch1;
    public Switch switch2;
    public Switch switch3;
    public Switch switch4;
    public Switch switch5;
    public Switch switch6;
    public Switch switch7;
    public Switch switch8;
    public Switch switch9;

    [Header("Doors")]

    public Door door1;
    public Door door2;
    public Door door3;
    public Door door4;
    public Door door5;
    public Door door6;
    public Door door7;
    public Door door8;
    public Door door9;

    [Header("Lamps")]

    public Lamp lamp1;
    public Lamp lamp2;
    public Lamp lamp3;
    public Lamp lamp4;

    [Header("Sensors")]

    public Sensor sensor1;
    public Sensor sensor2;

    [Header("Events")]

    public Transform event1;
    public Transform event2;

    public Transform finalCamera;

    // Start is called before the first frame update
    void Start()
    {
        if(level == 1)
        {
            switch1.on = false;
            switch2.on = false;
            switch3.on = false;
            switch4.on = false;
            switch5.on = false;
            switch6.on = false;
            switch7.on = false;
            switch8.on = false;
            switch9.on = false;

            door1.opened = false;
            door2.opened = false;
            door3.opened = false;
            door4.opened = false;
            door5.opened = false;
            door6.opened = false;
            door7.opened = false;
            door8.opened = false;
            door9.opened = false;

            lamp1.on = false;
            lamp2.on = false;
            lamp3.on = false;
            lamp4.on = false;

            finalCamera.gameObject.SetActive(false);
        }
        else if(level == 2)
        {
            event1.gameObject.SetActive(false);
            event2.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(level == 1)
        {
            if (switch1.on)
            {
                door1.opened = true;
                door2.opened = true;
            }

            if (switch2.on && switch3.on)
            {
                lamp1.on = true;
            }

            if (switch4.on)
            {
                lamp3.on = true;
                door4.opened = true;
            }

            if (switch5.on)
            {
                lamp2.on = true;
                door8.opened = true;
            }

            if (switch6.on == true && switch7.on == false &&
               switch8.on == true && switch9.on == false)
            {
                lamp4.on = true;
            }

            if (lamp1.on && lamp2.on && lamp3.on && lamp4.on)
            {
                door3.opened = true;
                door9.opened = true;
            }

            if (sensor1.entered)
            {
                Debug.Log("Level finished");
                finalCamera.gameObject.SetActive(true);
            }

        }
        else if(level == 2)
        {
            if(sensor1.entered)
            {
                event1.gameObject.SetActive(true);
            }
            else if (sensor2.entered)
            {
                event2.gameObject.SetActive(true);
            }
        }
        

    }
}
