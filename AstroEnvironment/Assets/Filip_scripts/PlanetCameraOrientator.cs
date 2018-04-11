using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCameraOrientator : MonoBehaviour {

	//To do:
	//Fix initial camera rotation
	//Make reset function for reuse

	public GameObject sun;
	public GameObject player;
	public GameObject self;

	public bool triggered = false;
	public float zoomTime = 12f;
	public float rotTime = 8f;
	public Vector3 camToFocus;

	private bool rotate = false;
	private bool zoom = false;

	private GameObject pointTo;
	private GameObject pointCameraFocus;

	private float speedOrbit;
	private float radiusPlanetOrbit;

	private float totalTime = 0;
	private float rt = 0;
	private float zt = 0;
	private float lerpValueRotation = 0;
	private float lerpValueMovement = 0;
	private Quaternion targetRotation;
	private Quaternion originalRotation;
	private Vector3 originalPosition;
	private Vector3 centreSun;
	private Vector3 ab;
	private Vector3 bc;

	private float GetAngle (Vector3 line1, Vector3 line2, bool rad = true) {
		line1.Normalize ();
		line2.Normalize ();
		float angle = Mathf.Acos (line1.x * line2.x + line1.z * line2.z);
		//Checking sign of angle
		if (line1.z < line2.z) {
			angle = -angle;
		}
		if (rad)
			return angle;
		else
			return angle * 180f / Mathf.PI;
	}
		
	public void MakeCameraPoints () {
		//Useful variables
		var sizePlanet = transform.lossyScale.x;
		centreSun = sun.transform.position;

		//Position if earth was in "ideal position" for hardcoded values
		var idealPosition = centreSun + new Vector3 ((centreSun - self.transform.position).magnitude, 0 ,0); 

		//Points in which to the camera moves to and have its focus on
		pointTo = new GameObject ("pointTo");
		pointTo.transform.position = idealPosition + new Vector3 (-sizePlanet * 0.625f, sizePlanet*0.25f, sizePlanet * 0.25f);

		pointCameraFocus = new GameObject ("pointCameraFocus");
		pointCameraFocus.transform.position = idealPosition + new Vector3 (-sizePlanet*0.4f, sizePlanet*0.25f, sizePlanet * 0.0625f);

		//Get angle of planet and sun compared to ideal placement
		Vector3 lineSunPlanet = transform.position - centreSun;
		lineSunPlanet.Normalize ();
		float angleToIdeal = GetAngle (Vector3.right, lineSunPlanet, false);

		//Rotate the hardcoded points so they correspond to earths actual position
		pointTo.transform.RotateAround (centreSun, Vector3.up, angleToIdeal);
		pointCameraFocus.transform.RotateAround (centreSun, Vector3.up, angleToIdeal);
	}

	void Start () {
	}
		
	void LateUpdate () {
		speedOrbit = self.GetComponent <OrbitAround> ().speed;

		//Where to
		if (triggered) {
			triggered = false;

			MakeCameraPoints ();
			camToFocus = pointCameraFocus.transform.position - player.transform.position;
			pointTo.transform.RotateAround (centreSun,  Vector3.up, speedOrbit * (zoomTime+rotTime));
			pointCameraFocus.transform.RotateAround (centreSun,  Vector3.up, speedOrbit * rotTime);

			rotate = true;
			zoom = true;
			targetRotation = Quaternion.LookRotation
				((pointCameraFocus.transform.position - player.transform.position).normalized);
			originalPosition = player.transform.position;
			originalRotation = player.transform.rotation;
			print (targetRotation);
			print (targetRotation.eulerAngles);
				
		}
		if (rotate || zoom) {
			totalTime += Time.deltaTime;
		}
		//Rotate and zoom
		if (rotate){
			rt = totalTime / rotTime;
			player.transform.rotation = Quaternion.Slerp (originalRotation, targetRotation, lerpValueRotation);
			lerpValueRotation = 0.5f * (1 + Mathf.Sin ((rt + 0.5f * Mathf.PI) * Mathf.PI));
			if (rt >= 1f) {
				rotate = false;
			}

		}
		if (zoom) {
			zt = (totalTime-rotTime) / zoomTime;
			if (zt >= 0f) {
				ab = Vector3.Lerp (originalPosition, centreSun, lerpValueMovement);
				bc = Vector3.Lerp (centreSun, pointTo.transform.position, lerpValueMovement);
				player.transform.position = Vector3.Lerp (ab, bc, lerpValueMovement);
				lerpValueMovement = 0.5f * (1 + Mathf.Sin ((zt + 0.5f * Mathf.PI) * Mathf.PI));
				if (zt >= 1f) {
					zoom = false;
					self.GetComponent <main> ().zoomFinished = true; 
				}
			}
		}
			
		//Finished, keep in position and look at
		if (rt >= 1f) {
			pointCameraFocus.transform.RotateAround (centreSun, Vector3.up, speedOrbit * Time.deltaTime);
			player.transform.LookAt (pointCameraFocus.transform.position);
			if (zt >= 1f) {
				pointTo.transform.RotateAround (centreSun,  Vector3.up, speedOrbit * Time.deltaTime);
				player.transform.position = pointTo.transform.position;
			}
		}
	}
}
