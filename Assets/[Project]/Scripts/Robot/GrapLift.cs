using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrapLift : MonoBehaviour
{
    public float grapForceThreshold;
    public List<GameObject> liftableObjects = new List<GameObject>();
    private Vector3 nearestObjectPosition;
    public GameObject nearestObject;

    public Material closedObjectMaterial;
    public Material notClosedObjectMaterial;

    //KAARIS est trop UwU Vive le kaaris

    public void AddObjectInLiftableList(GameObject objectToAdd)
    {
        liftableObjects.Add(objectToAdd);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.GetComponent<Rigidbody>() != null && !other.CompareTag("ChainComponent"))
            {
                if (liftableObjects.Count > 0)
                {
                    GetNearestObject();
                    foreach (GameObject obj in liftableObjects)
                    {
                        obj.GetComponent<MeshRenderer>().material = notClosedObjectMaterial;
                    }
                    nearestObject.GetComponent<MeshRenderer>().material = closedObjectMaterial;
                }
            }
        }
    }

    private void GetNearestObject()
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
                GetNearestObject();

                if (nearestObject != null && nearestObject.GetComponent<Rigidbody>().mass <= grapForceThreshold)
                {
                    nearestObject.gameObject.AddComponent<HingeJoint>();
                    nearestObject.GetComponent<HingeJoint>().enableCollision = true;
                    nearestObject.GetComponent<HingeJoint>().axis = new Vector3(0, 1, 0);
                    nearestObject.GetComponent<HingeJoint>().connectedBody = transform.GetComponent<Rigidbody>();
                }
            }
            else if (context.canceled)
            {
                if (nearestObject != null && nearestObject.GetComponent<HingeJoint>())
                {
                    Destroy(nearestObject.GetComponent<HingeJoint>());
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
//KAARIS SEVRAN !!