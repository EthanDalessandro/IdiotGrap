using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    [SerializeField] private float _followSpeed = 5f;
    [SerializeField] private Vector3 _offSet;
    [SerializeField] private Transform _targetTransform;

    void OnValidate()
    {
        transform.position = _targetTransform.position + _offSet;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _targetTransform.position + _offSet, Time.deltaTime * _followSpeed);
    }
}
