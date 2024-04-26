using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrapLift : MonoBehaviour
{
    public List<GameObject> liftableObjects;
    private Vector3 nearestObjectPosition;
    public GameObject nearestObject;
    public Transform grapParent;

    public void AddObjectInLiftableList(GameObject objectToAdd)
    {
        liftableObjects.Add(objectToAdd);
    }

    public void RemoveObjectInLiftableList(GameObject objectToRemove)
    {
        int index = 0;
        index = liftableObjects.IndexOf(objectToRemove);
        liftableObjects.RemoveAt(index);
    }

    public void GrapObject(InputAction.CallbackContext context)
    {
        if (liftableObjects.Count > 0)
        {
            if (context.performed)
            {
                nearestObjectPosition = liftableObjects[0].GetComponent<Collider>().ClosestPoint(transform.position);
                nearestObject = liftableObjects[0].gameObject;

                foreach (GameObject obj in liftableObjects)
                {
                    Vector3 currentNearestPoint = obj.transform.GetComponent<Collider>().ClosestPoint(transform.position);

                    if (Vector3.Distance(currentNearestPoint, transform.position) < Vector3.Distance(nearestObjectPosition, transform.position))
                    {
                        nearestObject = obj.transform.gameObject;
                        nearestObjectPosition = currentNearestPoint;
                    }
                }

                if (nearestObject != null)
                {
                    nearestObject.GetComponent<HingeJoint>().connectedBody = transform.GetComponent<Rigidbody>();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.GetComponent<Rigidbody>() != null && !other.CompareTag("ChainComponent"))
            {
                AddObjectInLiftableList(other.transform.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.GetComponent<Rigidbody>() != null && !other.CompareTag("ChainComponent"))
            {
                RemoveObjectInLiftableList(other.transform.gameObject);
            }
        }
    }
}
