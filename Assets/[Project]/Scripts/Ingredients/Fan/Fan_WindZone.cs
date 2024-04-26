using UnityEngine;

public class Fan_WindZone : MonoBehaviour, IEffectable
{
    [SerializeField][Range(100, 300)] private float _windForce;
    private bool _isActive = true;

    public void OnAreaEnter(Rigidbody bodyToAffect)
    {
        if (_isActive == true)
        {
            bodyToAffect.AddForce(transform.forward * _windForce);
        }
    }

    public void SetEffect(bool isActive)
    {
        _isActive = isActive;
    }
}