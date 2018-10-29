using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightAnimationConstant : AbstractLightAnimation
{
    public LightAnimationManager animationManager;
    public UnityEvent onAnimationEnd;

    public float value = 1.0f;
    public bool animate;

    private float currentValue = 1;


	//void Start ()
    //{
    //    animationManager.RegisterComponent(this);
	//}

    public override float getCurrentValue()
    {
        if (animate == true && currentValue != value)
            currentValue = value;
        return currentValue;
    }

    void Animate()
    {
        animate = true;
    }

    void PauseAnimate()
    {
        animate = false;
    }

    void StopAnimate()
    {
        PauseAnimate();
        currentValue = 1;
    }

    //Public Events
    public override void StartAnimation()
    {
        Animate();
    }

    public override void StopAnimation()
    {
        StopAnimate();
    }

    public override void PauseAnimation()
    {
        PauseAnimate();
    }

    public void AnimationEnd()
    {
        onAnimationEnd.Invoke();
    }
}
