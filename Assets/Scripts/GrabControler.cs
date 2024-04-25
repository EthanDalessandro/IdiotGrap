using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabControler : MonoBehaviour
{
    [SerializeField] private Rigidbody _grabRigidbody;
    [SerializeField] private float _grabSpeed;
    [SerializeField] private float _controlerSpeed;
    private Rigidbody _controlerRigdbody;

    void Start()
    {
        _controlerRigdbody = GetComponent<Rigidbody>();
    }
    // public Vector3 newGrabVel;
    // void FixedUpdate()
    // {
    //     newGrabVel = transform.position - _grabRigidbody.transform.position;
    //     newGrabVel.y = 0;

    //     if(newGrabVel.magnitude > .1f)
    //         _grabRigidbody.velocity = newGrabVel.normalized * _grabSpeed;
    // }

    private void OnMove(InputValue value)
    {
        Vector2 inputVector = value.Get<Vector2>();
        _controlerRigdbody.velocity = new Vector3(inputVector.x, 0, inputVector.y) * _controlerSpeed;
    }
}
