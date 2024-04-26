using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnginerControler : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    private Vector2 _inputVector;

    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        transform.forward = new Vector3(_inputVector.x, 0, _inputVector.y);

        Vector3 newVelocity = transform.forward * _inputVector.magnitude * _speed;
        newVelocity.y = _rigidbody.velocity.y;
        _rigidbody.velocity = newVelocity;
    }

    private void OnMove(InputValue value)
    {
        _inputVector = value.Get<Vector2>();
    }
}
