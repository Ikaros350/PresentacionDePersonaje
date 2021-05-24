using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffects : MonoBehaviour
{
    [Header("Principal")]
    [SerializeField] ParticleSystem ps1;
    [SerializeField] ParticleSystem ps2;
    [SerializeField] Light light;
    [SerializeField] ParticleSystem complement;
    [SerializeField] Projector floor;
    [SerializeField] bool play = true;
    [SerializeField] int state = 0;
    [SerializeField] AnimationCurve explosionCurve;
    [SerializeField] AnimationCurve RainCurve;
    [SerializeField] AnimationCurve ComplementCurve;
    [SerializeField] AnimationCurve ComplementEmissionCurve;
    [SerializeField] float floorSize, explosionDuration, rainDuration, ExplosionMaxParticles, RainMaxParticles;
    [Header("Ice effect")]
    [SerializeField] Color colorIce;
    [SerializeField] Gradient colorSnow;
    [SerializeField] Material psMaterialSnow;
    [SerializeField] Material iceFloor;  

    [Header("FireEffect")]
    [SerializeField] Color colorSmoke;
    [SerializeField] Gradient colorFire;
    [SerializeField] Material psMaterialCarbon;
    [SerializeField] Material lavaFloor;

    ParticleSystem.MainModule ps1Main;
    ParticleSystem.EmissionModule ps1Emission;

    ParticleSystem.MainModule ps2Main;
    ParticleSystem.EmissionModule ps2Emission;

    ParticleSystem.MainModule complementMain;
    ParticleSystem.EmissionModule complementEmission;
    ParticleSystem.ShapeModule complementShape;

    ParticleSystem.TextureSheetAnimationModule complementAnimation;

    ParticleSystemRenderer material;

    AudioSource mySource;
    
    float z = 0;
    float y = 0;
    float timeExplosion=0;
    float timeRain = 0;


    public bool Play { get => play; set => play = value; }

    void Awake()
    {
        mySource = GetComponent<AudioSource>();
        complement = ps2.transform.GetChild(0).GetComponent<ParticleSystem>();
        material = complement.GetComponent<ParticleSystemRenderer>();
        floor.orthographicSize = floorSize;
        ps1Main = ps1.main;
        ps2Main = ps2.main;
        complementMain = complement.main;

        ps1Emission = ps1.emission;
        ps2Emission = ps2.emission;
        complementEmission = complement.emission;

        complementAnimation = complement.textureSheetAnimation;
        complementShape = complement.shape;
        complementShape = complement.shape;
        ps1Main.startLifetime= explosionDuration;
        ps2Main.duration = rainDuration;
        complementMain.duration = rainDuration;
        complementMain.startSpeed = new ParticleSystem.MinMaxCurve(5, ComplementCurve);

        ps1Emission.rateOverTime = new ParticleSystem.MinMaxCurve(ExplosionMaxParticles, explosionCurve);
        ps2Emission.rateOverTime = new ParticleSystem.MinMaxCurve(RainMaxParticles, RainCurve);
        complementEmission.rateOverTime = new ParticleSystem.MinMaxCurve(RainMaxParticles, ComplementEmissionCurve);
        if (CompareTag("Ice"))
        {
            Ice();
        }
        else
        {
            Fire();
        }
        complementShape.radius = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (play)
        {
            light.intensity = y;
            floor.material.SetFloat("_Alpha", y);
            floor.material.SetFloat("_Factor", z);
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
                    floor.gameObject.SetActive(true);
                    timeExplosion = 0;
                    complementShape.radius = 2;
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
            z = 0.5f * Mathf.Sin(((2f * Mathf.PI / 1.5f) * timeRain) - (Mathf.PI / 2f)) + 0.5f;
            timeRain += Time.deltaTime;
            if (timeRain >= rainDuration + ps2Main.startLifetime.constant)
            {
                timeRain = 0;
                play = false;
                floor.enabled = false;
            } 
        }
    }
    void Fire()
    {
        ps1Main.startColor = colorSmoke;
        ps2Main.startColor = colorFire;
        complementAnimation.enabled = true;
        complementAnimation.numTilesX = 5;
        complementAnimation.numTilesY = 6;
        complementMain.startColor = colorSmoke;
        light.color = colorSmoke;
        material.material = psMaterialCarbon;
        floor.material = Instantiate(lavaFloor);
    }
    void Ice()
    {
        ps1Main.startColor = colorIce;
        ps2Main.startColor = colorSnow;
        complementMain.startColor = colorIce;
        light.color = colorIce;
        material.material = psMaterialSnow;
        floor.material = Instantiate(iceFloor);
    }
}
