using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayBehaviour : MonoBehaviour {

	//To do:
	//Temporarily turn off trail renderer - check
	//Better way to oscillate - check
	//Lerp should be replaced, no t! - check
	//Excitation 
	//Color change of trail renderer

	public GameObject earth;
	public GameObject self;
	public GameObject sun;
	public GameObject atmosphere;

	public float speed = 0.01f;
	public float oscillationSpeed = 0.1f;
	public float amplitude = 0.5f;
	public float initDistToEarth;

	public Vector3 currentDir;

	private Color RayColor = new Color (255, 255, 255, 1);

	private bool isHeatray = false;
	private bool insideAtmosphere = false;
	private bool launched = false;

	private Vector3 oscComp;
	private Vector3 oscDir = Vector3.up;
	private Vector3 camToFocus;
	private Vector3 noOscillationPos;

	private float oscillationT = 0f;
	private float distToEarth;

	//Custom methods

	//Methods for making sure the ray oscillates
	Vector3 OscillationDirection (Vector3 newDirection) {
		return Vector3.Cross (newDirection, camToFocus).normalized;
	}

	//to do, returns the oscillation to be added to the position. Keeps control of oscT internally
	Vector3 OscillationComponent (Vector3 oscDirection, float amp, float oscSpeed){
		oscillationT += speed;
		return oscDirection * amp * Mathf.Sin (2 * Mathf.PI * oscillationT * oscillationSpeed);
	}

	//Kan lage forenklet retningsvariabel, kanskje spare litt ressurser
	//Could use other, to make sure the correct path is used
	void OnTriggerEnter (Collider other) {
		Vector3 normalToEarth = self.transform.position - earth.transform.position;
		currentDir = Vector3.Reflect (currentDir, normalToEarth).normalized;
		oscDir = OscillationDirection (currentDir);

		noOscillationPos = self.transform.position;

		if (!isHeatray) {
			isHeatray = true;
			self.GetComponent<TrailRenderer> ().startColor = new Color (255f, 0f, 0f, 1f);
		}
	}

	void ExcitationRandomPath () {
		currentDir = new Vector3 (Random.value, Random.value, Random.value);
		noOscillationPos = self.transform.position;
		oscDir = OscillationDirection (currentDir);
	}

	void StartUp () {
		launched = true;
		//Enable trail and its color
		noOscillationPos = self.transform.position;
		self.GetComponent<TrailRenderer> ().enabled = true; 
		self.GetComponent<TrailRenderer> ().startColor = new Color (255f, 255f, 255f, 1f);
	}

	void kill (){
		oscDir = Vector3.up;
		insideAtmosphere = false;
		launched = false;
		self.GetComponent<TrailRenderer> ().Clear ();
		self.GetComponent<TrailRenderer> ().enabled = false; 
		self.SetActive (false);
	}

	//Start and update methods

	void Start () {
		self.SetActive (false);
	}

	void Update (){
		//Shouldn't need to be in Update, dependent on rotation
		camToFocus = earth.GetComponent<PlanetCameraOrientator> ().camToFocus;

		if (self.activeSelf && !launched) {
			StartUp ();
		}

		if (launched) {
			//Propelling in its given direction
			noOscillationPos = noOscillationPos + currentDir * speed;
			self.transform.position = noOscillationPos + currentDir * speed
			+ OscillationComponent (oscDir, amplitude, oscillationSpeed);

			distToEarth = (earth.transform.position - self.transform.position).sqrMagnitude - earth.transform.lossyScale.x;

			//Checks if inside atmos
			if (distToEarth < atmosphere.transform.lossyScale.x) {
				insideAtmosphere = true;
			} else {
				insideAtmosphere = false;
			}

			if (distToEarth > 2 * initDistToEarth) {
				kill ();
			}
				
			//Changes path randomly is inside the atmosphere
			if (insideAtmosphere && (Random.value < 0) &&
				(distToEarth > 0.05f*earth.transform.lossyScale.x) && isHeatray) {
				ExcitationRandomPath ();
			}
		}
	}
}
