using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField] int state;
    [SerializeField] float maxDuration;
    [SerializeField] AnimationCurve myCurve;
    [SerializeField] AudioClip storm, thunder, electricity;
    [SerializeField] ParticleSystem lightningStorm, thunderbolt, powerAura, rain;
    [SerializeField] Light myLight;

    float time, y, count, lightningTime; 
    AudioSource mySource, rainSource;
    Animator myAnimator;

    bool active;

    ParticleSystem.EmissionModule stormEM;

    public void Awake()
    {
        mySource = GetComponent<AudioSource>();
        rainSource = rain.gameObject.GetComponent<AudioSource>();
        rainSource.Stop();
        myAnimator = GetComponent<Animator>();
        time = 5;
        count = 0;
        active = false;
        stormEM = lightningStorm.emission;
        stormEM.rateOverTime = new ParticleSystem.MinMaxCurve(10, myCurve);

        if (maxDuration == 0)
            maxDuration = 5f;
    }

    public void Update()
    {
        time += Time.deltaTime;
        if (time < maxDuration && state != 0)
        {
            
            mySource.volume = y;
        }

        y = myCurve.Evaluate(time / maxDuration);
        myLight.intensity = y*5;

        StateMachine();
    }

    public void StateMachine()
    {
        switch (state)
        {
            case 0:
                RunEffect();
                break;
            case 1:
                StartEffect();
                break;
            case 2:
                MainEffect();
                break;
            case 3:
                EndEffect();
                break;
        }
    }

    public void RunEffect()
    {
        if (active)
        {
            count = 0;
            time = 0;
            state = 1;
            lightningStorm.Play();
        }
    }

    public void StartEffect()
    {
        powerAura.Stop();
        if (rain.isStopped)
        {
            rain.Play();
            rainSource.Play();
        }
        mySource.clip = storm;

        if (!mySource.isPlaying)
            mySource.Play();

        myAnimator.SetBool("IsCharging", true);

        if (time > 2.5f)
        {
            if (count == 0)
            {
                mySource.PlayOneShot(thunder, 1f);
                count = 1;
            }
            thunderbolt.Play();
            state = 2;
        }
    }

    public void MainEffect()
    {
        if (time > 3.7)
        {
            state = 3;
        }
    }

    public void EndEffect()
    {
        mySource.clip = electricity;
        if (rain.isPlaying)
        {
            rain.Stop();
            rainSource.Stop();
        }
        if (!mySource.isPlaying)
            mySource.Play();

        mySource.volume = 0.2f;

        powerAura.Play();

        if (time >= 5f)
        {
            myAnimator.SetBool("IsCharging", false);
        }

        if (time >= lightningTime)
        {
            state = 0;
            powerAura.Stop();
            mySource.Stop();
        }
            
    }

    public void Active( float i)
    {
        lightningTime = i;
        active = true;
        Invoke("UnActive", 1f);
    }

    public void ActiveTwo()
    {
        lightningTime = 10;
        active = true;
        Invoke("UnActive", 1f);
    }

    void UnActive()
    {
        active = false;
    }

}
