using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {
	
	public float Timer = 0f;
	ObjectPooler objectPooler;
	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		objectPooler = ObjectPooler.Instance;
	}
	/// <summary>
	/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	/// </summary>
	void FixedUpdate()
	{
		Timer -= Time.deltaTime;
		//TODO: delay between each spawn.
		if(Timer <= 0f){
			objectPooler.SpawnFromPool("Cube", transform.position, Quaternion.identity);
			Timer = 2f;
		}
		
	}
}
