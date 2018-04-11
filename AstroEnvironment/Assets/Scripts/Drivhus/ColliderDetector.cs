using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class ColliderDetector : MonoBehaviour {
	/// <summary>
	/// OnParticleCollision is called when a particle hits a collider.
	/// </summary>
	/// <param name="other">The GameObject hit by the particle.</param>
	void OnParticleCollision(GameObject other)
	{	
		
		Rigidbody body = other.GetComponent<Rigidbody>();
		if(body){
			Debug.Log("Nei");
				Vector3 direction = other.transform.position - transform.position;
				direction = direction.normalized;
				body.AddForce(direction * 5);
			}
	}
}
