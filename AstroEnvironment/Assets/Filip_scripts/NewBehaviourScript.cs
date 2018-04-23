using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

	public GameObject self;

	// Use this for initialization
	void Start () {
		self.transform.localRotation = Quaternion.Euler (90f, 120f, 100f);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
