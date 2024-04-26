using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffBehavior : MonoBehaviour
{
    public bool isOn = false;

    public void SetOnOff(bool value)
    {
        isOn = value;

        if(isOn)
            OnElementOn();
        else
            OnElementOff();
    }

    public virtual void OnElementOn()
    {
        print("Base fonction not set");
    }

    public virtual void OnElementOff()
    {
        print("Base fonction not set");
    }
}
