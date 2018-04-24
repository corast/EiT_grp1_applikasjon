using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZObjectPools;

public class main : MonoBehaviour {

	public GameObject earth;
	public GameObject sun;
	public GameObject SolarRay;
	public GameObject canv;
	public GameObject player;

	public Slider rateOfFireSlider;

	public bool stoppedRotation = false;
	public bool stoppedOrbit = false;
	public bool zoomFinished = false; //Use this to initialize audio and close-up scene
	public float timeBetweenRays = 1;

	private bool canvasUp = false;
	private float time = 0;
	private bool successPooling;
	private EZObjectPool objectPool;
	private Quaternion rot = new Quaternion (0, 0, 0, 0);
	private GameObject ray;

		
	// Use this for initialization
	void Awake () {
		objectPool = EZObjectPool.CreateObjectPool (SolarRay, "SolarRays", 50, false, true, false);
	}

	void Start () {
	}

	//Trigger everything
	public void StartEvent () {
		ZoomToEarth ();
	}

	//Sets camera position and moves the camera towards its ideal position for ghg-scene
	void ZoomToEarth () {
		earth.GetComponent <PlanetCameraOrientator> ().triggered = true;
	}

	//Reverts everything back to original settings. (Moves camera back?) 
	void ResetToOriginal (){
		RestartOrbit ();
		RestartRotation ();
	}

	//Self explanatory. (More orbits to stop?)
	void StopRotation () {
		earth.GetComponent<OwnRotationFilip> ().lowerSpeed = true;
	}

	void StopOrbit () {
		earth.GetComponent<OrbitAroundFilip> ().lowerSpeed = true;
	}

	//Run the audio and the photonsinewaves
	void RunGreenhouseGasScenario () {
		time += Time.deltaTime;
		if (time > timeBetweenRays) {
			time = 0f;
			Vector3 targetPoint = earth.transform.position
				+ new Vector3(0, Random.value * earth.transform.lossyScale.y * 0.4f, 0);
			Vector3 initPoint = Vector3.Lerp (sun.transform.position, targetPoint, 0.96f) + Vector3.up*0.5f;
			successPooling = objectPool.TryGetNextObject (initPoint, rot, out ray);
			ray.GetComponent<RayBehaviour> ().initDistToEarth = (targetPoint - initPoint).magnitude
				- earth.transform.lossyScale.x;
			ray.GetComponent<RayBehaviour> ().currentDir = (targetPoint - initPoint).normalized;

			ray.SetActive (true);
			if (successPooling) {
				print ("Ray on it's way!");
			}
		}
	}
		//earth.GetComponent<GreenhouseGasEffect> ().triggered = true;

	void RestartRotation () {
	}

	void RestartOrbit () {
	}

	void setUpCanvas () {
		canv.SetActive (true);
		canv.transform.position = Vector3.Lerp (sun.transform.position, earth.transform.position, 0.94f)
			+ 4f*Vector3.up;
		canv.transform.RotateAround (sun.transform.position, Vector3.up, 3f);
		canv.transform.LookAt (canv.transform.position + (canv.transform.position - player.transform.position));
		//canv.transform.rotation = Quaternion.Inverse (canv.transform.rotation);
		canv.transform.rotation = Quaternion.Euler(canv.transform.rotation.eulerAngles.x,
			canv.transform.rotation.eulerAngles.y, 0f);
	}

	// Update is called once per frame
	void Update () {
		timeBetweenRays = 1f / rateOfFireSlider.value;

		if (zoomFinished) {
			StopRotation ();
			StopOrbit ();
			zoomFinished = false;
		}

		//Emit some sexy solar rays
		if (stoppedOrbit && stoppedRotation) {
			RunGreenhouseGasScenario ();
			if (!canvasUp){
				canvasUp = true;
				setUpCanvas ();
		}
	}
}
}