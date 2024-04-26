using UnityEngine;

public class Fan_Rotation : MonoBehaviour
{
    [SerializeField][Range(1, 5)] private float _tourPerSec;
    private bool _canRotate;
    private float rotationSpeed;
    private float _currentRotation;

    private void Start()
    {
        _currentRotation = _tourPerSec;
    }

    private void Update()
    {
        if (_canRotate == true)
        {
            rotationSpeed = _currentRotation * 360;
            transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
        }
    }

    public void SetRotate(bool isEnabled)
    {
        _canRotate = isEnabled;
    }
}