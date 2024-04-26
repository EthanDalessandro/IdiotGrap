using UnityEngine;

public class FanWindZone : MonoBehaviour, IEffectable
{
    [SerializeField] private FanRotation _fanRotation;
    [SerializeField][Range(100, 300)] private float _maxWindForce;
    private ParticleSystem _particuleSystem;
    private float _currentWindForce;
    private bool _updateForce;
    private float _newForce;
    private float _newEmission;

    public float MaxWindForce { get { return _maxWindForce; } }

    private void Start()
    {
        _particuleSystem = GetComponentInChildren<ParticleSystem>();

        _currentWindForce = _maxWindForce;
        _fanRotation.CurrentRotationSpeed = _currentWindForce;
    }

    private void Update()
    {
        if (_updateForce == true)
        {
            _currentWindForce = Mathf.Lerp(_currentWindForce, _newForce, 2 * Time.deltaTime);
            _fanRotation.CurrentRotationSpeed = _currentWindForce;
            var emission = _particuleSystem.emission;
            emission.rateOverTime = Mathf.Lerp(emission.rateOverTime.constant, _newEmission, Time.deltaTime);

            if (_currentWindForce == _newForce)
            {
                _updateForce = false;
                _currentWindForce = _newForce;
                _fanRotation.CurrentRotationSpeed = _currentWindForce;
            }
        }
    }

    public void OnAreaEnter(Rigidbody bodyToAffect)
    {
        if (_currentWindForce > 0)
        {
            bodyToAffect.AddForce(transform.forward * _currentWindForce);
        }
    }

    public void SetWindForce(float newForce)
    {
        if (newForce > _maxWindForce) { newForce = _maxWindForce; }
        _newForce = newForce;
        _updateForce = true;
        if (newForce == 0)
            _newEmission = 0;
        else
            _newEmission = 2;
    }
}