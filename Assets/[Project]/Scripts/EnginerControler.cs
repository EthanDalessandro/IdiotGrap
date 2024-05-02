using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnginerControler : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotateSpeed = 5f;
    private Vector2 _inputVector;
    private Rigidbody _rigidbody;
    private EnginerInteraction _enginerInteraction;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _enginerInteraction = GetComponentInChildren<EnginerInteraction>();
    }

    void FixedUpdate()
    { 
        if(_inputVector == Vector2.zero)
            return;
        
        Vector3 forwardTarget = new Vector3(_inputVector.x, 0, _inputVector.y);
        transform.forward = Vector3.Slerp(transform.forward, forwardTarget, Time.deltaTime * _rotateSpeed / _enginerInteraction.GetItemGrabMass());

        Vector3 forceDirection = new Vector3(_inputVector.x, 0, _inputVector.y);
        float speedMult = _moveSpeed / _enginerInteraction.GetItemGrabMass();
        speedMult = Mathf.Clamp(speedMult, 8, 1000);
        // print(speedMult);
        _rigidbody.AddForce(forceDirection * speedMult * _rigidbody.mass, ForceMode.Force);
    }

    private void OnMove(InputValue value)
    {
        _inputVector = value.Get<Vector2>();
    }
}
