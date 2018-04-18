using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveCamera : MonoBehaviour {

    public GameObject player;
    private bool moveCamera = false;

    public float speed = 1.0F;
    private float startTime;
    private float journeyLength;

    private Transform startMarker;
    private Transform endMarker;

	public void OnCameraClick(){
        if(playerScript.isMoving){//prevent us from trying to move to multiple camera points
            return;
        }
        
        startMarker = Camera.main.transform;
		endMarker = this.transform;
        
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    
        moveCamera = true;
        playerScript.isMoving = true;
	}
    
    void OnTriggerEnter (Collider other){
        moveCamera = false;
        playerScript.isMoving = false;
        this.gameObject.GetComponent<Renderer>().enabled = false;
        //make object unclickable
        this.gameObject.GetComponent<EventTrigger>().enabled = false;
    }

    void OnTriggerExit (Collider other){
        this.gameObject.GetComponent<Renderer>().enabled = true;
        //this.gameObject.SetActive(true);
        this.gameObject.GetComponent<EventTrigger>().enabled = true;
    }

    void Update(){
        if(moveCamera){
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            player.transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
        }
    }
}
