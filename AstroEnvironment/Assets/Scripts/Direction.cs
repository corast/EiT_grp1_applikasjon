using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour {
	/*
		Makes object look at another gameobject.
	*/ 
	public GameObject gameObject;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 target = gameObject.transform.position - transform.position;

		Vector3 newDir = Vector3.RotateTowards(transform.forward, target, Time.deltaTime, 0.0f);

		//v3.x = v3.z = 0.0f;
		//transform.LookAt( gameObject.transform.position - v3 );
		//transform.Rotate(0,180,0);
		transform.rotation = Quaternion.LookRotation(newDir);
	}
}
