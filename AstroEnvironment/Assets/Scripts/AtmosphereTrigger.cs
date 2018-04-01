using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class AtmosphereTrigger : MonoBehaviour {

	public ParticleSystem ps;

    public List<ParticleCollisionEvent> collisionEvents;

	public List<ParticleSystem.Particle> exitParticles = new List<ParticleSystem.Particle>();
	
    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
    }

	void OnParticleTrigger()
	{	
		int exitParticleNumber = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exitParticles);


		for(int i = 0; i < exitParticleNumber; i++){
			//We need to change direction of particle.
			ParticleSystem.Particle p = exitParticles[i];
			Vector3 direction = p.position - transform.position;
			direction = direction.normalized;
			p.angularVelocity3D = direction;
			exitParticles[i] = p;
		}

		ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exitParticles);
	}
	/* 
    void OnParticleCollision(GameObject other)
    {
		int enterParticleNumber = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enterParticles);
		
        		//Itterate tru particles that enter the trigger and?
		for(int i=0; i<enterParticleNumber; i++){
			ParticleSystem.Particle p = enterParticles[i];
			p.startSize = 2f;
			enterParticles[i] = p;
		}

		ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enterParticles);

    } */

	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		Debug.Log(other.ToString());
	}
}