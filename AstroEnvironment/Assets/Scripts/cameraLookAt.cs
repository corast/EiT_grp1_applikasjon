using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraLookAt : MonoBehaviour {

	public Camera cameraToLookAt;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 v3 = cameraToLookAt.transform.position - transform.position;
		v3.x = v3.z = 0.0f;
		transform.LookAt( cameraToLookAt.transform.position);
		transform.Rotate(0,180,0);
	}
}
