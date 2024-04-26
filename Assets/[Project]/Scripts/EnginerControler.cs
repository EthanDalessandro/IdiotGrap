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

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 forwardTarget = new Vector3(_inputVector.x, 0, _inputVector.y);
        transform.forward = Vector3.Slerp(transform.forward, forwardTarget, Time.deltaTime * _rotateSpeed);

        Vector3 forceDirection = new Vector3(_inputVector.x, 0, _inputVector.y);
        _rigidbody.AddForce(forceDirection * _moveSpeed * _rigidbody.mass, ForceMode.Force);
    }

    private void OnMove(InputValue value)
    {
        _inputVector = value.Get<Vector2>();
    }
}
