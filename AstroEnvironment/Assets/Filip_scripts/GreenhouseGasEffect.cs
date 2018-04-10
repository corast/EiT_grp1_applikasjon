using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenhouseGasEffect : MonoBehaviour {

	public bool triggered = false;

	//Use list for random number of photons
	private Photons[] particles;

	void Start () {
		//UI.Slider
	//	Material matGasSphere = earth.transform.Find ("Atmosphere").gameObject.GetComponent<Renderer> ().material;
	//	Color fromColor = new Color (0.5f, 0.5f, 0.5f, 0.1f);
	//	Color toColor = new Color (0.5f, 0.5f, 0.5f, 0.6f);
	//	matGasSphere.color = Color.Lerp (fromColor, toColor, tag);
	//	tag += 0.01f;
	}

	void makeListOfPhotons (int photons) {
	}

	private class Photons {

		//Or public?
		private GameObject particle;

		public Photons (){

		}

	}

}