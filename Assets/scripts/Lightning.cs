using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField] int state;
    [SerializeField] float maxDuration;
    [SerializeField] AnimationCurve myCurve;
    [SerializeField] AudioClip storm, thunder, electricity;
    [SerializeField] ParticleSystem lightningStorm, thunderbolt, powerAura;
    [SerializeField] Light myLight;

    float time, y, count; 
    AudioSource mySource;
    Animator myAnimator;

    ParticleSystem.EmissionModule stormEM;

    public void Awake()
    {
        mySource = GetComponent<AudioSource>();
        myAnimator = GetComponent<Animator>();
        time = 0;
        count = 0;

        stormEM = lightningStorm.emission;
        stormEM.rateOverTime = new ParticleSystem.MinMaxCurve(10, myCurve);

        if (maxDuration == 0)
            maxDuration = 5f;
    }

    public void Update()
    {
        if (time < maxDuration && state != 0)
        {
            time += Time.deltaTime;
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
        myAnimator.SetBool("IsCharging", false);
        if (Input.GetButtonDown("Fire1"))
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

        if (!mySource.isPlaying)
            mySource.Play();

        mySource.volume = 0.2f;

        powerAura.Play();

        if (time >= 5)
            state = 0;
    }
}
