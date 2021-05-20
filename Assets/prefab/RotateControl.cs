using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AnimationCurve myCurve;
    [SerializeField] AnimationCurve orbitalCurve;
    [SerializeField] AnimationCurve textureCurve;
    [SerializeField] AnimationCurve emisionCurve;
    Light pointlight;
    Renderer myRend;
    [SerializeField] float speed, intesity, alpha, duration, maxOrbitalSpeed, maxEmision, shieldColldown;
    float t = 0;
    Color prevColor;
   [SerializeField] ParticleSystem particleSkull;
   [SerializeField] ParticleSystem particleAshes;
    ParticleSystem.VelocityOverLifetimeModule velOverlifeTime;
    ParticleSystem.EmissionModule emisionModule;
    ParticleSystem.MainModule mainModuleSkull;
    ParticleSystem.MainModule mainModuleAshes;
    float xOrbitalSpeed, orbitalTextureSpeed;
    [SerializeField] Transform ball;
    Collider shieldCol;
    [SerializeField] bool usingShield;

    void Awake()
    {
        pointlight = GetComponentInChildren<Light>();
        myRend = GetComponentInChildren<Renderer>();
        particleSkull = GetComponentInChildren<ParticleSystem>();
        velOverlifeTime = particleSkull.velocityOverLifetime;
        mainModuleAshes = particleAshes.main;
        mainModuleSkull = particleSkull.main;
        emisionModule = particleSkull.emission;
        shieldCol = ball.GetComponent<Collider>();
        prevColor = myRend.material.color;
        myRend.material.color = new Color(prevColor.r, prevColor.g, prevColor.b, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (usingShield)
        {
            Shield();
        }
    }

    void Shield()
    {
        t += Time.deltaTime;
        if (!particleSkull.isPlaying)
        {
            particleSkull.Play();
        }
        
        float y = myCurve.Evaluate(t / duration);
        myRend.material.color = new Color(prevColor.r, prevColor.g, prevColor.b, y * alpha);
        xOrbitalSpeed = orbitalCurve.Evaluate(t / duration) * maxOrbitalSpeed;
        orbitalTextureSpeed = myCurve.Evaluate(t / duration);
        velOverlifeTime.orbitalY = xOrbitalSpeed;
        mainModuleSkull.startSize = myCurve.Evaluate(t / duration) * 0.2f;
        mainModuleAshes.startSize = myCurve.Evaluate(t / duration) * 0.2f;

        emisionModule.rateOverTime = emisionCurve.Evaluate(t / duration) * maxEmision;
        ball.RotateAround(transform.position, Vector3.down, orbitalTextureSpeed * -speed * Time.deltaTime);
        pointlight.intensity = y * intesity;
        if (y > alpha)
        {
            myRend.material.color = new Color(prevColor.r, prevColor.g, prevColor.b, alpha);
        }
        if (t > shieldColldown)
        {
            usingShield = false;
            shieldCol.enabled = false;
            t = 0;
        }
    }

    public void CanUseShield(bool use)
    {
        usingShield = use;
        if (usingShield)
        {
            t = 0;
            shieldCol.enabled = true;
        }
    }

}
