using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnginerInteraction : MonoBehaviour
{
    [SerializeField] private Transform _grabTarget;
    [SerializeField] private float _grabMassThresold = 100;
    [SerializeField] private float _grabForce = 3;
    [SerializeField] private float _range;
    [SerializeField] private List<GameObject> _objectInRange = new List<GameObject>();

    private Collider _triggerCollider;
    private Rigidbody _currentGrabRigidbody;


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

    void FixedUpdate()
    {
        if (_currentGrabRigidbody)
            _currentGrabRigidbody.MovePosition(Vector3.Lerp(_currentGrabRigidbody.position, _grabTarget.position
                                                            , Time.fixedDeltaTime * _grabForce));
    }

    private void GrabObject()
    {
        _currentGrabRigidbody = GetNearestObject()?.GetComponent<Rigidbody>();
        if (_currentGrabRigidbody)
            _currentGrabRigidbody.useGravity = false;
    }

    private void DropObject()
    {
        if (!_currentGrabRigidbody)
            return;

        _currentGrabRigidbody.useGravity = true;
        _currentGrabRigidbody = null;
    }

    private GameObject GetNearestObject()
    {
        if (_objectInRange.Count == 0)
            return null;

        float minDistance = Mathf.Infinity;
        GameObject toReturn = null;

        foreach (var item in _objectInRange)
        {
            float currentDistance = Vector3.Distance(transform.position
                                                   , item.GetComponent<Collider>().ClosestPoint(transform.position));
            //! Check pour la distance
            if (currentDistance < minDistance)
            {
                //! Check orientation
                Vector3 itemDirection = (item.transform.position - transform.position).normalized;
                // print("Dot : " + Vector3.Dot(transform.forward, itemDirection));
                if (Vector3.Dot(transform.forward, itemDirection) >= 0)
                {
                    if (item.GetComponent<Rigidbody>().mass <= _grabMassThresold)
                    {
                        minDistance = currentDistance;
                        toReturn = item;
                    }
                }
            }
        }

        return toReturn;
    }

    private void OnInteract(InputValue value)
    {
        // print("OOOO");
        if (_objectInRange.Count == 0)
            return;

        float buttonValue = value.Get<float>();
        // print("Interact value : " + buttonValue);

        if (buttonValue > .5f)
            GrabObject();
        else
            DropObject();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnginerControler>())
            return;

        Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();
        if (otherRigidbody)
            _objectInRange.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (_objectInRange.Contains(other.gameObject))
            _objectInRange.Remove(other.gameObject);
    }

    public float GetItemGrabMass()
    {
        return _currentGrabRigidbody ? _currentGrabRigidbody.GetComponent<Rigidbody>().mass : 1; 
    }
}
