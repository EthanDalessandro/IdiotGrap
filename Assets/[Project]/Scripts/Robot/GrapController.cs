using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrapController : MonoBehaviour
{
    public GameObject chainObjectPrefab;
    public Transform parentToSpawnIn;

    public List<HingeJoint> chainComponents = new List<HingeJoint>();
    public int chainSpawnedCount;

    public Transform baseObjectToAttachJoints;

    public float grapFallingSpeed;
    private float grapYAxisDirection;
    private bool isArmStabilizing;


    private void Update()
    {
        //Vive KAARIS
        MoveGrap();
        ChainConstruction();
        StabilizeArmUpdating();
    }

    private void StabilizeArmUpdating()
    {
        if (chainComponents.Count > 1)
        {
            foreach (HingeJoint joint in chainComponents)
            {
                if (isArmStabilizing)
                {
                    joint.massScale += Time.deltaTime;
                }
                else
                {
                    joint.massScale--;
                }
                joint.massScale = Mathf.Clamp(joint.massScale, 1, 8);
            }
        }
    }

    public void GrapYAxisValue(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            grapYAxisDirection = context.ReadValue<float>();
        }
        else
        {
            grapYAxisDirection = 0;
        }
    }

    public void StabilizeArm(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isArmStabilizing = true;
        }
        else
        {
            isArmStabilizing = false;
        }

    }

    //Pour potentiellement améliorer ce systeme faut voir le changement des conditions de spawn et reset le connected Anchor à -0.6f...
    private void ChainConstruction()
    {
        if (chainComponents[chainSpawnedCount].connectedAnchor.y <= -1.7f)
        {
            GameObject chainSpawned = Instantiate(chainObjectPrefab, chainComponents[chainSpawnedCount].transform.position, Quaternion.identity, parentToSpawnIn);
            chainComponents.Add(chainSpawned.GetComponent<HingeJoint>());
            //J'ai enlevé cette partie parce qu'on se rend compte que set la position ne sert à rien puisque elle se set automatiquement en fonction du connected anchor
            //chainComponents[chainSpawnedCount + 1].transform.position = new Vector3(chainComponents[chainSpawnedCount].transform.position.x,
            //                                                                    chainComponents[chainSpawnedCount].transform.position.y + 0.8f,
            //                                                                    chainComponents[chainSpawnedCount].transform.position.z);
            chainComponents[chainSpawnedCount].connectedBody = chainComponents[chainSpawnedCount + 1].GetComponent<Rigidbody>();
            chainComponents[chainSpawnedCount + 1].connectedBody = baseObjectToAttachJoints.GetComponent<Rigidbody>();
            chainSpawnedCount++;
        }

        else if (chainComponents.Count > 1)
        {
            if (chainComponents[chainSpawnedCount].connectedAnchor.y >= -0.799f)
            {
                float yPosBeforeDelete = chainComponents[chainSpawnedCount].connectedAnchor.y;
                Destroy(chainComponents[chainSpawnedCount].transform.gameObject);
                chainComponents.RemoveAt(chainSpawnedCount);
                chainSpawnedCount--;
                chainComponents[chainSpawnedCount].transform.localRotation = Quaternion.Euler(0, 0, 0);
                chainComponents[chainSpawnedCount].connectedBody = baseObjectToAttachJoints.GetComponent<Rigidbody>();
                chainComponents[chainSpawnedCount].connectedAnchor = new Vector3(0, chainComponents[chainSpawnedCount].connectedAnchor.y - yPosBeforeDelete, 0);
            }
        }
    }

    private void MoveGrap()
    {
        if (chainComponents[chainSpawnedCount] != null && chainComponents[chainSpawnedCount].connectedAnchor.y <= -0.799f)
        {
            chainComponents[chainSpawnedCount].connectedAnchor = new Vector3(0,
                                                               chainComponents[chainSpawnedCount].connectedAnchor.y + grapYAxisDirection * grapFallingSpeed * Time.deltaTime,
                                                               0);
        }
        else
        {
            chainComponents[chainSpawnedCount].connectedAnchor = new Vector3(0,
                                                               -0.8f,
                                                               0);
        }
    }
}