using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electric : MonoBehaviour
{
    [SerializeField] ParticleSystem ps;
    [SerializeField] Transform hand;
    [SerializeField] float duration;
    [SerializeField] bool play;
    [SerializeField] float time;

        
    // Start is called before the first frame update
    void Awake()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (play)
        {
            if (!ps.isPlaying)
            {
                ps.Play();
            }
            time += Time.deltaTime;
            transform.position = hand.position;
            if (time >= duration)
            {
                play = false;
            }
        }
    }
}
