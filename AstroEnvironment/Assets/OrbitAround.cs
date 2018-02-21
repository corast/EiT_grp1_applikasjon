using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {

    public GameObject Sun;
    public float speed=0.001;

    // Use this for initialization
    void Start()
    {
    }
	
	// Update is called once per frame
	void Update () {
        OrbitAround();
    }
    void OrbitAround()
    {
        transform.RotateAround(Sun.transform.position, Vector3.up, speed * Time.deltaTime);
    }
}
}
