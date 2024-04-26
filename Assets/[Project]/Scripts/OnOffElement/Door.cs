using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Door : OnOffBehavior
{
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _openGap;
    [SerializeField] private Transform _leftDoor;
    [SerializeField] private Transform _rightDoor;
    private Vector3 _leftDoorStart;
    private Vector3 _rightDoorStart;
    private Vector3 _leftDoorTarget;
    private Vector3 _rightDoorTarget;

    void Start()
    {
        _leftDoorStart = _leftDoor.localPosition;
        _rightDoorStart = _rightDoor.localPosition;

        ResetTarget();
    }

    void Update()
    {
        _leftDoor.localPosition = Vector3.Lerp(_leftDoor.localPosition, _leftDoorTarget, Time.deltaTime * _speed);
        _rightDoor.localPosition = Vector3.Lerp(_rightDoor.localPosition, _rightDoorTarget, Time.deltaTime * _speed);
    }

    private void ResetTarget()
    {
        _leftDoorTarget = _leftDoorStart;
        _rightDoorTarget = _rightDoorStart;
    }

    private void SetDoorTarget()
    {
        _leftDoorTarget = _leftDoorStart + new Vector3(-_openGap / 2, 0, 0);
        _rightDoorTarget = _rightDoorStart + new Vector3(_openGap / 2, 0, 0);
    }

    public override void OnElementOn()
    {
        // base.OnElementOn();
        SetDoorTarget();
    }

    public override void OnElementOff()
    {
        // base.OnElementOff();
        _leftDoorTarget = _leftDoorStart;
        _rightDoorTarget = _rightDoorStart;
    }
}
