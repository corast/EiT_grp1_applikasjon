using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnRotation : MonoBehaviour
{
	public bool lowerSpeed = false;
	public float lowerSpeedRate = 0.001f;
    public GameObject ObjectCoordinates;
    public float speed = 10f;

    // Update is called once per frame
    void Update()
    {
		Rotate(lowerSpeed);
    }

	void Rotate(bool lowerSpeed)
    {
        transform.RotateAround(ObjectCoordinates.transform.position, Vector3.back, speed * Time.deltaTime);
		if (lowerSpeed && speed > 0) {
			speed -= lowerSpeedRate;
		} else if (speed <= 0) {
			speed = 0;
			ObjectCoordinates.GetComponent<main> ().stoppedRotation = true;
		}
    }
}

