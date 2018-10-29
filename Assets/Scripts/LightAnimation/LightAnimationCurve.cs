using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AbstractLightAnimation : MonoBehaviour
{
    public abstract float getCurrentValue();

    public abstract void StartAnimation();
    public abstract void StopAnimation();
    public abstract void PauseAnimation();
}


public class LightAnimationCurve : AbstractLightAnimation
{
    public LightAnimationManager animationManager;
    public UnityEvent onAnimationEnd;

    public AnimationCurve intensityCurve;
    public float animationLength = 1;
    public bool loopAnimation;

    private float currentValue = 1;
    private bool animate = false;
    private float localTime = 0 ;


	//void Start ()
    //{
    //    animationManager.RegisterComponent(this);
	//}

    public override float getCurrentValue()
    {
        if (animate)
        {
            if (loopAnimation) { localTime = (localTime + Time.deltaTime) % animationLength; }
            if (!loopAnimation) { localTime += Time.deltaTime; }
            currentValue = intensityCurve.Evaluate(localTime / animationLength);
            //Animation END
            if (localTime >= animationLength)
            {
                StopAnimate();
                onAnimationEnd.Invoke();
            }
        }
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
        localTime = 0;
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
}
