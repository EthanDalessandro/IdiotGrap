using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnginerControler : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }



    private void OnMove(InputValue value)
    {
        Vector2 inputVector = value.Get<Vector2>();
        _rigidbody.velocity = new Vector3(inputVector.x, 0, inputVector.y) * _speed;
    }
}
