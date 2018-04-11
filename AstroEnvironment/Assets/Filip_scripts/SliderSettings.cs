using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSettings : MonoBehaviour {

	public Slider slider1;
	public Slider slider2;
	public Slider slider3;

	public void OnSliderChangeOne(){
		
		float value = slider1.value;
		//Gjør et elelr annet med infoen fra slideren.
		Debug.Log(value);

	}
	
	public void OnSliderChangeTwo(){
		
		float value = slider2.value;
		//Gjør et elelr annet med infoen fra slideren.
		Debug.Log(value);

	}
	
	public void OnSliderChangeThree(){
		
		float value = slider3.value;
		//Gjør et elelr annet med infoen fra slideren.
		Debug.Log(value);

	}
}
