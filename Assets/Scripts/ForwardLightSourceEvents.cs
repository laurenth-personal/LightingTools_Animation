using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class ForwardLightSourceEvents : MonoBehaviour {

    LightSource[] children;

    private void OnEnable()
    {
        children = GetComponentsInChildren<LightSource>();
    }

    public void SwitchON()
    {
        if(children.Length >0)
        {
            foreach(var child in children)
            {
                child.SwitchON();
            }
        }
    }

    public void SwitchOFF()
    {
        if (children.Length > 0)
        {
            foreach (var child in children)
            {
                child.SwitchOFF();
            }
        }
    }

    public void Break()
    {
        if (children.Length > 0)
        {
            foreach (var child in children)
            {
                child.Break();
            }
        }
    }

    public void SpecialEvent()
    {
        if (children.Length > 0)
        {
            foreach (var child in children)
            {
                child.SpecialEvent();
            }
        }
    }
}
