using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class OnOffBehavior : MonoBehaviour
{
    protected bool isOn = false;

    public bool IsOn 
    { 
        get { return isOn; }  
        set 
        { 
            isOn = value;
            if (isOn)
                OnElementOn();
            else
                OnElementOff();
        }
    } 

    public void SetOnOff(bool value)
    {
        IsOn = value;
    }

    public void SetOnOffDelayed(bool value, float delay, bool loop = false)
    {
        StartCoroutine(SetOnOffWithDelay(value, delay, loop));
    }

    public virtual void OnElementOn()
    {
        print("Base fonction not set");
    }

    public virtual void OnElementOff()
    {
        print("Base fonction not set");
    }

    IEnumerator SetOnOffWithDelay(bool value, float delay, bool loop)
    {
        IsOn = value;
        yield return new WaitForSeconds(delay);
        IsOn = !value;
        if (loop) { SetOnOffDelayed(!value, delay, loop); }
    }
}
