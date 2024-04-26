using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public GameObject  target1;
    public GameObject target2;
    private void Start() 
    {
        lineRenderer.positionCount = 2;
    }
    private void Update() 
    {
        lineRenderer.SetPosition(0, target1.transform.position);
        lineRenderer.SetPosition(1, target2.transform.position);    
        
    }
}
