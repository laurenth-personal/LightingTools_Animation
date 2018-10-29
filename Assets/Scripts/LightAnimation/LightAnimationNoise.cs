using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightAnimationNoise : AbstractLightAnimation
{
    public LightAnimationManager animationManager;
    public UnityEvent onAnimationEnd;

    public float frequency = 5;
	public float minimumValue = 0;
	public float maximumValue = 1;
	[Range(0.0f,1.0f)]
	public float jumpFrequency = 0;
    public float animationLength = 1.0f;
	
	private float currentValue = 1;
    private bool animate = false;
    private float localTime = 0 ;
	private int seed;


    void Start()
    {
        //animationManager.RegisterComponent(this);

	    seed = (int)Random.Range(0,10000);
	}

    public override float getCurrentValue()
    {
        if (animate)
        {
            localTime += Time.deltaTime;

            EvaluateNoiseAnimation();
            
            //Animation END
            if (animationLength != 0 && localTime >= animationLength)
            {
                StopAnimate();
                onAnimationEnd.Invoke();
            }
        }
        return currentValue;
    }

    private void EvaluateNoiseAnimation()
    {
        if (jumpFrequency > 0)
        {
            float jumpRand;
            jumpRand = Random.value;
            jumpRand = Mathf.Round(jumpRand * 10) / 10;
            if (jumpRand < jumpFrequency)
            {
                localTime = localTime + 1;
            }
        }
        currentValue = samplePerlinNoise(localTime, frequency, seed);
        currentValue = currentValue * (maximumValue - minimumValue) + minimumValue;
    }

    private float samplePerlinNoise(float localtime, float frequency, int seed)
	{
		float noiseFade;
		noiseFade = Mathf.PerlinNoise(localtime*frequency,(float)seed);
		return noiseFade;
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
        currentValue = 1.0f;
        localTime = 0;
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
