using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClickFunction : MonoBehaviour {

    public GameObject sun;
    private Vector3 startPoint;
    private Vector3 endPoint;

    void Start()
    {

        startPoint = transform.position;
        endPoint = new Vector3(15.0f, 3.0f, 0.0f);

    }

    public void MoveSun (){
        Debug.Log("Trykk");
        transform.position = Vector3.down;
    }
}
