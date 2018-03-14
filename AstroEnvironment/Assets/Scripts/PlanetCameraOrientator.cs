using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCameraOrientator : MonoBehaviour {

	public GameObject sun;
	public GameObject planet;
	public float speedAnimation;
	public float speedOrbit;
	private float radiusPlanetOrbit;

	private GameObject pointVia;
	private GameObject pointTo;
	private GameObject pointCameraFocus;

	private Vector3 centreSun;
	private float zt;
	private float rt;
	private Vector3 ab;
	private Vector3 bc;


	float Distance (Vector3 distFrom, Vector3 distTo) {
		return Mathf.Sqrt( Mathf.Pow (distFrom.x - distTo.x, 2) + Mathf.Pow (distFrom.y - distTo.y, 2) + Mathf.Pow (distFrom.z - distTo.z, 2));
	}

	//
	float GetAngle (Vector3 line1, Vector3 line2, bool rad = true) {
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
			return angle * 180 / Mathf.PI;
	}

	void Start () {
		//Useful variables
		var radiusPlanetOrbit = Distance(sun.transform.position, planet.transform.position);
		var sizePlanet = planet.transform.lossyScale.x;
		centreSun = sun.transform.position;

		//Position if earth was in "perfect alignment" to hardcoded values
		var idealPosition = centreSun + new Vector3 (Distance (centreSun, planet.transform.position), 0 ,0);

		//Points in which to the camera moves  via, to and have its focus on
		pointTo = new GameObject ("pointTo");
		pointTo.transform.position = idealPosition + new Vector3 (-sizePlanet * 0.625f, sizePlanet*0.125f, sizePlanet * 0.25f);

		pointVia = new GameObject ("pointVia");
		pointVia.transform.position = new Vector3 (radiusPlanetOrbit*0.25f, 0, radiusPlanetOrbit*0.25f);

		pointCameraFocus = new GameObject ("pointCameraFocus");
		pointCameraFocus.transform.position = idealPosition + new Vector3 (-sizePlanet*0.5f, sizePlanet*0.125f, 0);

		//Get angle of planet and sun compared to ideal placement
		Vector3 lineSunPlanet = planet.transform.position - centreSun;
		lineSunPlanet.Normalize ();
		float angleToIdeal = GetAngle (Vector3.right, lineSunPlanet, false);

		//Rotate the hardcoded points so they correspond to earths actual position
		pointTo.transform.RotateAround (centreSun, Vector3.up, angleToIdeal);
		pointVia.transform.RotateAround (centreSun, Vector3.up, angleToIdeal);
		pointCameraFocus.transform.RotateAround (centreSun, Vector3.up, angleToIdeal);
		zt = 0;
		rt = 0;
	}
	
	void LateUpdate () {

		//Updating points so they rotate along with earth
		pointTo.transform.RotateAround (centreSun,  Vector3.up, speedOrbit * Time.deltaTime);
		pointVia.transform.RotateAround (centreSun,  Vector3.up, speedOrbit * Time.deltaTime);
		pointCameraFocus.transform.RotateAround (centreSun,  Vector3.up, speedOrbit * Time.deltaTime);

		//Nice rotation from start of zoom
		if (rt < 1) {
			var targetRotation = Quaternion.LookRotation(pointCameraFocus.transform.position - transform.position);

			// Smoothly rotate towards the target point.
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rt);
			rt += speedAnimation * (1.25f - Mathf.Pow (rt, 2));
		}

		//Movement of the camera to its destination point
		//Should be replaced with raycast, and t = 0 after, if it's to repeated
		if (zt < 1 && rt >= 0.9)  {
			ab = Vector3.Lerp (transform.position, pointVia.transform.position, zt);
			bc = Vector3.Lerp (pointVia.transform.position, pointTo.transform.position, zt);
			transform.position = Vector3.Lerp (ab, bc, zt);
			transform.LookAt (pointCameraFocus.transform.position);
			zt += speedAnimation * (1.25f - Mathf.Pow (zt, 2));
		}
		if (zt >= 1) {
			transform.position = pointTo.transform.position;
			transform.LookAt (pointCameraFocus.transform.position);
		}
	}
}
