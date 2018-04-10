using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitAround : MonoBehaviour
{
	public bool lowerSpeed = false;
	public float lowerSpeedRate = 0.001f;
    public GameObject Midpoint;
	public GameObject self;
    public float speed = 10f;

    // Update is called once per frame
    void Update()
    {
		Orbit(lowerSpeed);
    }

	void Orbit(bool lowerSpeed)
    {
        transform.RotateAround(Midpoint.transform.position, Vector3.up, speed * Time.deltaTime);
		if (lowerSpeed && speed > 0) {
			speed -= lowerSpeedRate;
		} else if (speed <= 0) {
			speed = 0;
			self.GetComponent<main> ().stoppedOrbit = true;
		}

    }
}

