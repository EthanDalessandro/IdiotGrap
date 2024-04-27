using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private OnOffBehavior _connectedOnOff;

    void OnTriggerEnter(Collider other)
    {
        if (!_connectedOnOff)
            return;

        if (other.GetComponent<EnginerControler>() || other.tag == "Crate")
            _connectedOnOff.SetOnOff(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (!_connectedOnOff)
            return;

        if (other.GetComponent<EnginerControler>() || other.tag == "Crate")
            _connectedOnOff.SetOnOff(false);
    }
}
