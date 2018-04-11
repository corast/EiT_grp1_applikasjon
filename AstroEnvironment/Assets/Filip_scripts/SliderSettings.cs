using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSettings : MonoBehaviour {

	public Slider mainSlider;

	public void OnSliderChange(){
		
		float value = mainSlider.value;
		//Gjør et elelr annet med infoen fra slideren.
		Debug.Log(value);

	}
}
