using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExempleOnOff : OnOffBehavior
{
    private MeshRenderer _renderer;

    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _renderer.material.color = Color.red;
    }

    public override void OnElementOn()
    {
        // base.OnElementOn();
        _renderer.material.color = Color.green;
    }

    public override void OnElementOff()
    {
        // base.OnElementOff();
        _renderer.material.color = Color.red;
    }
}
