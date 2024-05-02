using UnityEngine;
using UnityEngine.Events;

public class FanManager : OnOffBehavior
{
    [SerializeField] private FanRotation _fanRotation;
    [SerializeField] private FanWindZone _fanWindZone;
    [SerializeField][Range(0, 10)] private float _windDuration;

    private void Start()
    {
        SetOnOffDelayed(true, _windDuration, true);
    }

    public override void OnElementOn()
    {
        _fanWindZone.SetWindForce(_fanWindZone.MaxWindForce);
    }

    public override void OnElementOff()
    {
        _fanWindZone.SetWindForce(0);
    }
}
