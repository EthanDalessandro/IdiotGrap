using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private OnOffBehavior _connectedOnOff;
    private List<GameObject> _rigidbodysOnPlate = new List<GameObject>();

    private void TurnOnOff()
    {
        _connectedOnOff.SetOnOff(_rigidbodysOnPlate.Count != 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!_connectedOnOff)
            return;

        if (other.GetComponent<Rigidbody>())
            _rigidbodysOnPlate.Add(other.gameObject);

        TurnOnOff();
    }

    void OnTriggerExit(Collider other)
    {
        if (!_connectedOnOff)
            return;

        if (_rigidbodysOnPlate.Contains(other.gameObject))
            _rigidbodysOnPlate.Remove(other.gameObject);

        TurnOnOff();
    }
}
