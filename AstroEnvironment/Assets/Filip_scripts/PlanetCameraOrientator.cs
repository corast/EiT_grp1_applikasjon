using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetCameraOrientator : MonoBehaviour {

	//To do:
	//Make reset function for reuse

	public GameObject sun;
	public GameObject player;
	public GameObject camera;
	public GameObject self;
	public GameObject slider;

	public bool triggered = false;
	public float zoomTime = 12f;
	public float rotTime = 8f;
	public Vector3 camToFocus;

	private bool rotate = false;
	private bool zoom = false;
	private bool needReset = false;
	private bool freeCamera = true;

	private GameObject pointTo;
	private GameObject pointCameraFocus;

	private float speedOrbit;
	private float radiusPlanetOrbit;

	private float totalTime = 0;
	private float rt = 0;
	private float zt = 0;
	private float at = 0;
	private Quaternion targetRotation;
	private Quaternion originalRotation;
	private Quaternion secondRot;
	private Quaternion secondWant;
	private Quaternion secondRotHelp;
	private Vector3 originalPosition;
	private Vector3 centreSun;
	private Vector3 ab;
	private Vector3 bc;
	private Vector3 fro;
	private Vector3 to;

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
		pointTo.transform.position = idealPosition + new Vector3 (-sizePlanet * 0.6f, sizePlanet*0.25f, sizePlanet * 0.175f);

		pointCameraFocus = new GameObject ("pointCameraFocus");
		pointCameraFocus.transform.position = idealPosition + new Vector3 (-sizePlanet*0.4f, sizePlanet*0.25f, sizePlanet * 0.0625f);

		//Get angle of planet and sun compared to ideal placement
		Vector3 lineSunPlanet = transform.position - centreSun;
		lineSunPlanet.Normalize ();
		float angleToIdeal = GetAngle (Vector3.right, lineSunPlanet, false);

		//Rotate the hardcoded points so they correspond to earths actual position
		pointTo.transform.RotateAround (centreSun, Vector3.up, angleToIdeal);
		pointCameraFocus.transform.RotateAround (centreSun, Vector3.up, angleToIdeal + 1f);
	}

	void Start () {
	}
				
	void LateUpdate () {				
		speedOrbit = self.GetComponent <OrbitAround> ().speed;

		//Where to
		if (triggered && !needReset) {
			triggered = false;
			needReset = true;

			MakeCameraPoints ();
			camToFocus = pointCameraFocus.transform.position - player.transform.position;
			pointTo.transform.RotateAround (centreSun,  Vector3.up, speedOrbit * (zoomTime+rotTime));
			pointCameraFocus.transform.RotateAround (centreSun,  Vector3.up, speedOrbit * rotTime);

			rotate = true;
			zoom = true;
			targetRotation = Quaternion.LookRotation
				((pointCameraFocus.transform.position - player.transform.position).normalized);
			originalPosition = player.transform.position;
			originalRotation = camera.transform.rotation;
				
		}
		if (rotate || zoom) {
			totalTime += Time.deltaTime;
		}
		//Rotate and zoom
		if (rotate){
			rt = totalTime / rotTime;
			camera.transform.rotation = Quaternion.Slerp (originalRotation, targetRotation, rt);
			if (rt >= 1f) {
				rotate = false;
			}

		}
		if (zoom) {
			zt = (totalTime-rotTime) / zoomTime;
			if (zt >= 0f) {
				ab = Vector3.Lerp (originalPosition, centreSun, zt);
				bc = Vector3.Lerp (centreSun, pointTo.transform.position, zt);
				player.transform.position = Vector3.Lerp (ab, bc, zt);
				if (zt >= 1f) {
					zoom = false;
					self.GetComponent <main> ().zoomFinished = true; 
				}
			}
		}
			
		//Finished, keep in position and look at
		if (rt >= 1f) {
			pointCameraFocus.transform.RotateAround (centreSun, Vector3.up, speedOrbit * Time.deltaTime);
			if (speedOrbit > 0) {
				camera.transform.LookAt (self.transform.position);

				secondRot = Quaternion.LookRotation ((self.transform.position -
					player.transform.position).normalized);
				secondWant = Quaternion.LookRotation ((pointCameraFocus.transform.position -
					player.transform.position).normalized);

				fro = camera.transform.rotation*Vector3.forward;
				to = self.transform.position-player.transform.position;
				secondRotHelp = Quaternion.FromToRotation (fro, to);

			} else if (freeCamera) {
				if (at > 1f) {
					player.transform.rotation = new Quaternion (0, 0, 0, 0);
					fro = camera.transform.rotation*Vector3.forward;
					to = pointCameraFocus.transform.position-player.transform.position;
					secondRotHelp = Quaternion.FromToRotation (fro, to);
		
					player.transform.rotation *= secondRotHelp;
					freeCamera = false;
				} else if (at > 0) {
					camera.transform.rotation = Quaternion.Slerp (secondRot, secondWant, at);
					at += 0.01f;
				} else {
					player.transform.rotation *= secondRotHelp;
					at += 0.01f;
				}
			}
		if (zt >= 1f && speedOrbit > 0) {
			pointTo.transform.RotateAround (centreSun,  Vector3.up, speedOrbit * Time.deltaTime);
			player.transform.position = pointTo.transform.position;
		}
	}
}
}