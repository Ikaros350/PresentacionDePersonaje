using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffects : MonoBehaviour
{
    [SerializeField] bool play = true;
    [SerializeField] int state = 0;
    [SerializeField] AnimationCurve explosionCurve;
    [SerializeField] AnimationCurve RainCurve;
    [SerializeField] float explosionDuration, rainDuration, ExplosionMaxParticles, RainMaxParticles;
    [SerializeField] Color colorSmoke;
    [SerializeField] Gradient colorSnow;
    [SerializeField] Light light;
    [SerializeField] Material psMaterial;

    [SerializeField] ParticleSystem ps1;
    [SerializeField] ParticleSystem ps2;
    [SerializeField] ParticleSystem complement;

    ParticleSystem.MainModule ps1Main;
    ParticleSystem.EmissionModule ps1Emission;

    ParticleSystem.MainModule ps2Main;
    ParticleSystem.EmissionModule ps2Emission;

    ParticleSystemRenderer ps2Renderer;

    AudioSource mySource;

    float z = 0;
    float y = 0;
    float timeExplosion=0;
    float timeRain = 0;
    void Awake()
    {
        mySource = GetComponent<AudioSource>();

        ps1Main = ps1.main;
        ps2Main = ps2.main;
        complement = ps2.transform.GetChild(0).GetComponent<ParticleSystem>();

        ps1Emission = ps1.emission;
        ps2Emission = ps2.emission;

        ps1Main.startColor = colorSmoke;
        ps2Main.startColor = colorSnow;

        ps1Main.startLifetime= explosionDuration;
        ps2Main.duration = rainDuration;

        ps1Emission.rateOverTime = new ParticleSystem.MinMaxCurve(ExplosionMaxParticles, explosionCurve);
        ps2Emission.rateOverTime = new ParticleSystem.MinMaxCurve(RainMaxParticles, RainCurve);

        
        light.color = colorSmoke;
    }

    // Update is called once per frame
    void Update()
    {
        if (play)
        {
            light.intensity = y;
            if (state == 0)
            {
                //light.intensity = z;
                //z = explosionCurve.Evaluate(timeExplosion / explosionDuration);
                timeExplosion += Time.deltaTime;

                if (!ps1.isPlaying)
                {
                    ps1.Play();
                    ps2.Play();
                    mySource.Play();
                }
                if (timeExplosion >= explosionDuration+0.1f)
                {
                    timeExplosion = 0;
                    state = 1;
                }
            }
            /*
            if (!ps2.isPlaying && state == 1)
            {
                ps2.Play();
                state = 2;
            }
            */
            mySource.volume = y;
            y = RainCurve.Evaluate(timeRain /( rainDuration + ps2Main.startLifetime.constant));
            timeRain += Time.deltaTime;
            if (timeRain >= rainDuration + ps2Main.startLifetime.constant)
            {
                timeRain = 0;
                play = false;
            } 
        }
    }
}
