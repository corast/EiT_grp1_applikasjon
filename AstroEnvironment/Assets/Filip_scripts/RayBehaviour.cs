using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayBehaviour : MonoBehaviour {

	//To do:
	//Temporarily turn off trail renderer 		- check
	//Better way to oscillate 					- check
	//Lerp should be replaced, no t! 			- check
	//Excitation 								- check, possible modification: Only in normal plane for better view +
	//													 + waiting time before it rechanges course (or other criteria)
	//Color change of trail renderer			- check
	//Slider to determine density of atmosphere, aka excitationProbPerFrame - Sondre

	public GameObject earth;
	public GameObject self;
	public GameObject sun;
	public GameObject atmosphere;

	public Slider excitationSlider;
	public Slider speedSlider;

	public float speed = 0.01f;
	public float oscillationSpeed = 0.1f;
	public float amplitude = 0.5f;
	public float initDistToEarth;

	public Vector3 currentDir;

	public Material whiteTrail;
	public Material redTrail;

	private bool isHeatray = false;
	private bool insideAtmosphere = false;
	private bool launched = false;

	private Vector3 oscComp;
	private Vector3 oscDir = Vector3.up;
	private Vector3 camToFocus;
	private Vector3 posNoOscillation;
	private Vector3 sunToEarth;

	private float oscillationT = 0f;
	private float distToEarth;
	private float radiusEarth;
	private float excitationProbPerFrame;

	//Custom methods

	//Method for finding which way the oscillation occurs, relative to the camera.
	//Will be buggy if camera-particle and direction is parallell, no normal vector. Extremely unlikely if static camera
	Vector3 OscillationDirection (Vector3 newDirection) {
		return Vector3.Cross (newDirection, camToFocus).normalized;
	}

	//Oscillation component, in normal direction to the direction of travel and camera-particle vector
	Vector3 OscillationComponent (Vector3 oscDirection, float amp, float oscSpeed){
		oscillationT += speed;
		return oscDirection * amp * Mathf.Sin (2 * Mathf.PI * oscillationT * oscillationSpeed);
	}

	//If it collides with a Rigidbody (earth)
	void OnTriggerEnter (Collider other) {

		//Finding reflection angle to the earths surface, aka new direction
		Vector3 normalToEarth = self.transform.position - earth.transform.position;
		currentDir = Vector3.Reflect (currentDir, normalToEarth).normalized;
		oscDir = OscillationDirection (currentDir);

		posNoOscillation = self.transform.position;
		self.GetComponent <TrailRenderer> ().startWidth = self.GetComponent <TrailRenderer> ().startWidth * 0.9f;
		self.GetComponent <TrailRenderer> ().endWidth = self.GetComponent <TrailRenderer> ().endWidth * 0.9f;
		self.GetComponent<TrailRenderer> ().Clear ();

		//If it hits the earth for the 1. time, it changes to a heatray (color red) and may excitate
		if (!isHeatray) {
			isHeatray = true;
			self.GetComponent<TrailRenderer> ().material = redTrail;
		}
	}

	//Gives a new random path for the particle. To be activated randomly in atmosphere
	void ExcitationRandomPath () {
		//currentDir = new Vector3 (Random.value, Random.value, Random.value);
		self.GetComponent<TrailRenderer> ().Clear ();

		sunToEarth = (sun.transform.position - earth.transform.position).normalized;
		currentDir = (Random.Range(-1,1)*Vector3.up + Random.Range(-1,1)*sunToEarth).normalized;
		posNoOscillation = self.transform.position;
		oscDir = OscillationDirection (currentDir);
	}

	//Startup upon activation
	void StartUp () {
		launched = true;
		//Init pos and distance to earths surface
		posNoOscillation = self.transform.position;
		initDistToEarth = (earth.transform.position - self.transform.position).magnitude - radiusEarth;
		//Enable trail and white (light) color
		self.GetComponent<TrailRenderer> ().enabled = true; 
		self.GetComponent<TrailRenderer> ().startColor = new Color (255f, 255f, 255f, 1f);
	}

	//Resets the particle and clears trail
	void kill (){
		oscDir = Vector3.up;
		insideAtmosphere = false;
		launched = false;
		isHeatray = false;
		self.GetComponent<TrailRenderer> ().Clear ();
		self.GetComponent<TrailRenderer> ().enabled = false;
		self.GetComponent<TrailRenderer> ().material = whiteTrail;
		self.SetActive (false);
	}

	//Start and update methods

	//Deactivate the ray, so all copies in pool are deactivated until called upons
	void Start () {
		radiusEarth = earth.transform.lossyScale.x * 0.5f;
		self.SetActive (false);
	}

	void Update (){
		//Shouldn't need to be in Update after rotation is stopped, dependent on rotation really
		//If non-static camera, keep as is
		camToFocus = earth.GetComponent<PlanetCameraOrientator> ().camToFocus;
		excitationProbPerFrame = (speed / 0.003f) * excitationSlider.value / 12000f;
		speed = speedSlider.value;
		self.GetComponent <TrailRenderer> ().time = 0.003f / speed;

		//Initialize base variables, only if not launched and activated.
		if (self.activeSelf && !launched) {
			StartUp ();
		}

		if (launched) {
			//Propelling in its given direction
			posNoOscillation = posNoOscillation + currentDir * speed;
			self.transform.position = posNoOscillation + currentDir * speed
			+ OscillationComponent (oscDir, amplitude, oscillationSpeed);

			//Distance to earth's surface
			distToEarth = (earth.transform.position - self.transform.position).magnitude - radiusEarth;

			//Checks if inside atmosphere
			if (distToEarth < (atmosphere.transform.lossyScale.x*0.5f - radiusEarth)) {
				insideAtmosphere = true;
			} else {
				insideAtmosphere = false;
			}

			//Kills the particle if it's going too far away from earth
			if (distToEarth > initDistToEarth) {
				kill ();
			}
				
			//Changes path randomly if inside the atmosphere; excitation
			if (insideAtmosphere && (Random.value < excitationProbPerFrame) &&
				(distToEarth > 0.05f*radiusEarth) && isHeatray) {
				ExcitationRandomPath ();
			}
		}
	}
}
