using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Ellipse : MonoBehaviour
{

   public LineRenderer lineRenderer;

    //Segments in our ellipse
    [Range(3, 36)]
    public int segments;
    public float xAxis = 5f;
    public float yAxis = 3f;
    Vector3 SunPosition = new Vector3(-40, 0, 30);

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        lineRenderer.SetColors()
        lineRenderer = GetComponent<LineRenderer>();
        CalculateEllipseSun();
    }

    void CalculateEllipseSun()
    {
        Vector3[] points = new Vector3[segments + 1];
        for (int i = 0; i < segments; i++)
        {
            float angle = ((float)i / (float)segments) * 360 * Mathf.Deg2Rad;
            float x = Mathf.Cos(angle) * xAxis;
            float y = Mathf.Sin(angle) * yAxis;
            points[i] = new Vector3(x,y, 0f) + SunPosition;
        }
        points[segments] = points[0];

        lineRenderer.positionCount = segments + 1;
        lineRenderer.SetPositions(points);
    }

   
    /// <summary>
    /// Called when the script is loaded or a value is changed in the
    /// inspector (Called in the editor only).
    /// </summary>
    void OnValidate()
    {
        CalculateEllipseSun();
     
    }
}