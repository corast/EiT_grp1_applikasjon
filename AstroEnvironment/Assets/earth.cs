using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class earth : MonoBehaviour {
	public GameObject Earth;
	public Slider mainSlider;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onClick(){
		/* Change camera position to on earth, 
		and stop the rotation around the sun */ 
		Debug.Log("Clicking");
		
		//Stop rotating around the sun.
		OrbitAround.toggleOrbit();
	}

	public void changeSize(float size){
		double value = mainSlider.value;
		//Earth.transform.localScale();
		Debug.Log(value);
	}
}
