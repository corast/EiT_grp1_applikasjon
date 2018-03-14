using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenhouseGasEffect : MonoBehaviour {

	public float opacity;
	public float size;
	public Color colorGasSphere;

	private GameObject gasSphere;
	private float planetSize;
	private Color fromColor;
	private Color toColor;
	private float t;

	// Use this for initialization
	void Start () {
		//Create and place a sphere
		gasSphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		gasSphere.transform.localScale = transform.localScale*1.1f;

		var matGasSphere = gasSphere.GetComponent<Renderer>().material;
		//I have no clue what this does, but it's essential for it to work
		matGasSphere.SetFloat ("_Mode", 3);
		matGasSphere.SetInt ("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
		matGasSphere.SetInt ("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
		matGasSphere.SetInt ("_ZWrite", 0);
		matGasSphere.DisableKeyword ("_ALPHATEST_ON");
		matGasSphere.DisableKeyword ("_ALPHABLEND_ON");
		matGasSphere.EnableKeyword ("_ALPHAPREMULTIPLY_ON");
		matGasSphere.renderQueue = 3000;

		fromColor = new Color (0.5f, 0.5f, 0.5f, 0.05f);
		toColor = new Color (0.5f, 0.5f, 0.5f, 0.75f);
		t = -10f;
		gasSphere.GetComponent<Renderer> ().material.color = fromColor;
	}
	
	// Update is called once per frame
	void Update () {
		//Ensure the sphere is positioned around the planet
		gasSphere.transform.position = transform.position;
		if (0 <= t && t < 1f) {
			var lerpedColor = Color.Lerp (fromColor, toColor, t);
			gasSphere.GetComponent<Renderer> ().material.color = lerpedColor;
			t += (1f / 300f);
		} else if (t < 0) {
			t += 1f / 100f;
		}
		print (t);


		//if raycast, change size and transparency?
		//gasSphere.renderer.material.color.a = 0.5;
	}
}
