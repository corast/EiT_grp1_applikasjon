using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;

public class main : MonoBehaviour {

	public GameObject earth;
	public GameObject sun;
	public GameObject SolarRay;
	public bool stoppedRotation = false;
	public bool stoppedOrbit = false;
	public bool zoomFinished = false; //Use this to initialize audio and close-up scene

	private float time = 0;
	private bool successPooling;
	private EZObjectPool objectPool;
	private Quaternion rot = new Quaternion (0, 0, 0, 0);
	private GameObject ray;
	//private bool eventSignal = false; //Use this to stop rotation and orbits. (Is this necessary?)

		
	// Use this for initialization
	void Awake () {
		objectPool = EZObjectPool.CreateObjectPool (SolarRay, "SolarRays", 10, false, true, false);
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
		earth.GetComponent<OwnRotation> ().lowerSpeed = true;
	}

	void StopOrbit () {
		earth.GetComponent<OrbitAround> ().lowerSpeed = true;
	}

	//Run the audio and the photonsinewaves
	void RunGreenhouseGasScenario () {
		time += Time.deltaTime;
		if (time > 0.5f) {
			time = 0f;
			Vector3 targetPoint = earth.transform.position
				+ new Vector3(0, Random.value * earth.transform.lossyScale.y * 0.5f, 0);
			Vector3 initPoint = Vector3.Lerp (sun.transform.position, targetPoint, 0.96f);
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
		
	// Update is called once per frame
	void Update () {

		if (zoomFinished) {
			StopRotation ();
			StopOrbit ();
			zoomFinished = false;
		}

		//Emit some sexy solar rays
		if (stoppedOrbit && stoppedRotation) {
			RunGreenhouseGasScenario ();
		}

	}
	//LateUpdate for last changes in scene per frame.
	void LateUpdate () {
	}
}
