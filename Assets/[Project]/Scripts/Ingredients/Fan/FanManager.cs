using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FanManager : OnOffBehavior
{
    [SerializeField] private FanRotation _fanRotation;
    [SerializeField] private FanWindZone _fanWindZone;
    [SerializeField][Range(0, 10)] private float _windDuration;
    [SerializeField][Range(0, 10)] private float _delayBetweenBlow;
    [SerializeField] private UnityEvent _OnElementOn;
    [SerializeField] private UnityEvent _OnElementOff;
    private bool _isApplyingForce;

    private void Start()
    {
        StartCoroutine(FanInterval());
    }

    IEnumerator FanInterval()
    {
        yield return new WaitForSeconds(_windDuration);
        if (_isApplyingForce)
        {
            _isApplyingForce = false;
            _fanWindZone.SetWindForce(0);
        } else
        {
            _isApplyingForce = true;
            _fanWindZone.SetWindForce(_fanWindZone.MaxWindForce);
        }
        yield return new WaitForSeconds(_delayBetweenBlow);
        StartCoroutine(FanInterval());
    }

    public override void OnElementOn()
    {
        _OnElementOn.Invoke();
    }

    public override void OnElementOff()
    {
        _OnElementOff.Invoke();
    }
}
