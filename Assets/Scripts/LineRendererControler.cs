using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererControler : MonoBehaviour
{
    [SerializeField] private Transform _controlerTransform;
    [SerializeField] private Transform _grabTransform;
    [SerializeField] private LineRenderer _line;


    void Update()
    {
        _line.SetPosition(0, _controlerTransform.position);
        _line.SetPosition(1, _grabTransform.position);
    }
}
