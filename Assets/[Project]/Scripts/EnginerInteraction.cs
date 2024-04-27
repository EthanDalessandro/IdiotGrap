using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnginerInteraction : MonoBehaviour
{
    [SerializeField] private Transform _grabTarget;
    [SerializeField] private float _range;
    [SerializeField] private List<GameObject> _interactibleInRange = new List<GameObject>();

    private Collider _triggerCollider;
    private Rigidbody _rigidbodyGrab;

    void Start()
    {
        _triggerCollider = GetComponent<Collider>();
    }

    private void OnValidate()
    {
        if (!_triggerCollider)
            _triggerCollider = GetComponent<Collider>();

        ((SphereCollider)_triggerCollider).radius = _range;
    }

    private void GrabObject(GameObject toGrab)
    {
        _rigidbodyGrab = toGrab.GetComponent<Rigidbody>();
        _rigidbodyGrab.isKinematic = true;
        _rigidbodyGrab.transform.parent = _grabTarget;
        _rigidbodyGrab.MovePosition(_grabTarget.position);
    }

    private void DropObject()
    {
        _rigidbodyGrab.isKinematic = false;
        _rigidbodyGrab.transform.parent = null;
        _rigidbodyGrab = null;
    }

    private GameObject GetNearestObject()
    {
        float minDistance = Mathf.Infinity;
        GameObject toReturn = null;

        foreach (var item in _interactibleInRange)
        {
            float currentDistance = Vector3.Distance(transform.position
                                                   , item.GetComponent<Collider>().ClosestPoint(transform.position));
            if (currentDistance < minDistance)
            {
                minDistance = currentDistance;
                toReturn = item;
            }
        }

        return toReturn;
    }

    private void OnInteract(InputValue value)
    {
        print("OOOO");
        if (_interactibleInRange.Count == 0)
            return;

        float buttonValue = value.Get<float>();
        print("Interact value : " + buttonValue);
        if (buttonValue > .5f)
            GrabObject(GetNearestObject());
        else
            DropObject();
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();
        if (otherRigidbody)
            _interactibleInRange.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (_interactibleInRange.Contains(other.gameObject))
            _interactibleInRange.Remove(other.gameObject);
    }
}
