using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraLookAt : MonoBehaviour {

	public GameObject cameraToLookAt;
	public GameObject sun;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = 10f*(cameraToLookAt.transform.position - sun.transform.position).normalized
			+ cameraToLookAt.transform.position;
		transform.LookAt(transform.position + transform.position - cameraToLookAt.transform.position);
		//transform.Rotate(180,180,0);
	}
}
