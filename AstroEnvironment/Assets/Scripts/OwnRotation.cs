using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnRotation : MonoBehaviour
{

    public GameObject ObjectCoordinates;
    public float speed = 10f;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }
    void Rotate()
    {
        transform.RotateAround(ObjectCoordinates.transform.position, Vector3.up, speed * Time.deltaTime);
    }
}

