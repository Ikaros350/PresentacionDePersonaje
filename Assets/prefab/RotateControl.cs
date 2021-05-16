using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AnimationCurve myCurve;
    Light pointlight;
    Renderer myRend;
    [SerializeField] float speed, intesity, alpha, duration;
    float t=0;
    Color prevColor;

    void Awake()
    {
        pointlight = GetComponentInChildren<Light>();
        myRend = GetComponentInChildren<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
        prevColor = myRend.material.color;
        float y = myCurve.Evaluate(t / duration);
        myRend.material.color = new Color(prevColor.r, prevColor.g, prevColor.b, y*alpha);
        
        //if (y > alpha)
        //{
        //    myRend.material.color = new Color(prevColor.r, prevColor.g, prevColor.b, alpha);
        //}
        pointlight.intensity = y*intesity;

        t += Time.deltaTime;

        //transform.RotateAround(transform.position, Vector3.down, -speed*Time.deltaTime);

        
    }
}
