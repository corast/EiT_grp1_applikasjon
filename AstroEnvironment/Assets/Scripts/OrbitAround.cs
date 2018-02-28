using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitAround : MonoBehaviour
{

    public GameObject Midpoint;
    public float speed = 10f;

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
        transform.RotateAround(Midpoint.transform.position, Vector3.up, speed * Time.deltaTime);
    }
}

