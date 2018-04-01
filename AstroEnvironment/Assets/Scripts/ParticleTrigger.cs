using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParticleTrigger : MonoBehaviour {

	public ParticleSystem ps;

	public List<ParticleSystem.Particle> enterParticles = new List<ParticleSystem.Particle>();

	public List<ParticleSystem.Particle> exitParticles = new List<ParticleSystem.Particle>();

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()
	{
		ps = GetComponent<ParticleSystem>();
	}

	/// <summary>
	/// OnParticleTrigger is called when any particles in a particle system
	/// meet the conditions in the trigger module.
	/// </summary>
	void OnParticleTrigger()
	{	
		int enterParticleNumber = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enterParticles);
		int exitParticleNumber = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exitParticles);

		//Itterate tru particles that enter the trigger and?
		for(int i=0; i<enterParticleNumber; i++){
			ParticleSystem.Particle p = enterParticles[i];
			p.startColor = new Color32(255,0,0,255);
			//enterParticles[i] = p;
		}


		ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enterParticles);
	}

	/// <summary>
	/// OnCollisionEnter is called when this collider/rigidbody has begun
	/// touching another rigidbody/collider.
	/// </summary>
	/// <param name="other">The Collision data associated with this collision.</param>
	void OnCollisionEnter(Collision other)
	{
		Debug.Log(other.ToString());
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		
	}
}
