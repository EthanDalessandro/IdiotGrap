using UnityEngine;

public class FanManager : MonoBehaviour
{
    public enum FanMode
    {
        CONTINIOUS,
        INTERVAL
    }

    [SerializeField] private FanMode _fanMode;
}
