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
    [SerializeField] float speed, intesity, alpha, duration, maxOrbitalSpeed, maxEmision;
    float t=0;
    Color prevColor;
    ParticleSystem particle;
    ParticleSystem.VelocityOverLifetimeModule velOverlifeTime;
    ParticleSystem.EmissionModule emisionModule;
    float xOrbitalSpeed, orbitalTextureSpeed;
    [SerializeField] Transform ball;

    void Awake()
    {
        pointlight = GetComponentInChildren<Light>();
        myRend = GetComponentInChildren<Renderer>();
        particle = GetComponentInChildren<ParticleSystem>();
        velOverlifeTime = particle.velocityOverLifetime;
        emisionModule = particle.emission;
    }

    // Update is called once per frame
    void Update()
    {
        prevColor = myRend.material.color;
        float y = myCurve.Evaluate(t / duration);
        myRend.material.color = new Color(prevColor.r, prevColor.g, prevColor.b, y*alpha);
        xOrbitalSpeed = orbitalCurve.Evaluate(t / duration) * maxOrbitalSpeed;
        orbitalTextureSpeed = myCurve.Evaluate(t / duration);
        velOverlifeTime.orbitalY = xOrbitalSpeed;
        emisionModule.rateOverTime = emisionCurve.Evaluate(t / duration) * maxEmision;
        ball.RotateAround(transform.position, Vector3.down, orbitalTextureSpeed*-speed * Time.deltaTime);
        pointlight.intensity = y*intesity;
        t += Time.deltaTime;
        if (y > alpha)
        {
            myRend.material.color = new Color(prevColor.r, prevColor.g, prevColor.b, alpha);
        }
    }
}
