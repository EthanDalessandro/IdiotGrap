using UnityEngine;

public class FanRotation : MonoBehaviour
{
    private float _rotationSpeed;
    private float _currentRotation;

    public float CurrentRotationSpeed 
    { 
        get { return _currentRotation; }
        set 
        {
            _currentRotation = value.Remap(0, 300, 0, 5);
        } 
    }

    private void Update()
    {
        if (_currentRotation > 0)
        {
            _rotationSpeed = _currentRotation * 360;
            transform.Rotate(new Vector3(0, 0, _rotationSpeed * Time.deltaTime));
        }
    }
}