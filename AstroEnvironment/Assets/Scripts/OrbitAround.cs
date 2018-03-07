using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitAround : MonoBehaviour
{

    public GameObject Midpoint;
    public float speed = 10f;

    static bool orbit = true;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Orbit();
    }
    void Orbit()
    {
        if(orbit){
            transform.RotateAround(Midpoint.transform.position, Vector3.up, speed * Time.deltaTime);
        }
        
    }

    public static void toggleOrbit(){
        if(orbit){
            orbit = false;
        }else{
            orbit = true;
        }
    }
}

