using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonSineWave : MonoBehaviour {

	public GameObject sun;
	public float trailTime;
	public float oscillations;
	public float amp;

	private GameObject particle;
	private Vector3 centre_sun;
	private float t;

	//Creates particle sphere and puts on the trailRenderer. Sets scale to 0, and 
	//Needs changing to 
	void Start () {
		centre_sun = sun.transform.position;
		particle = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		particle.name = "Light";

		particle.transform.position = Vector3.Lerp (transform.position, centre_sun, 0.15f);
		TrailRenderer trailRenderer = particle.AddComponent (typeof(TrailRenderer)) as TrailRenderer;
		trailRenderer.time = trailTime;
		t = 0;
		particle.transform.localScale = new Vector3 (0, 0, 0);
	}

	void Update () {
		if (t < 0.15) {
			particle.transform.position = Vector3.Lerp (transform.position, centre_sun, 0.15f - t)
				+ new Vector3 (0, amp * Mathf.Sin(2 * Mathf.PI * t * oscillations / 0.15f), 0);

			t += 0.0001f;
		}
	}
}
