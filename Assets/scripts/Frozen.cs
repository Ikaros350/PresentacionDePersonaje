using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frozen : MonoBehaviour
{
    Animator myAnim;
    Renderer myRender;
    [SerializeField] float speedAnim, valueText, valueSpeed,time;
    [SerializeField] bool getFrezze;
    [SerializeField] ParticleSystem frezee;
    [SerializeField] ParticleSystem ambient;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        myRender = GetComponentInChildren<Renderer>();
        speedAnim = myAnim.speed;
        getFrezze = false;
    }

    // Update is called once per frame
    void Update()
    {
         
      

        
       //else if (Input.GetButtonDown("Fire1") && getFrezze)
       // {
       //     getFrezze = false;
       //     frezee.Play();
       //     ambient.Stop();

       // }

        if (getFrezze)
        {
            time += Time.deltaTime;
            
            valueText = Mathf.Lerp(0, 1, time);
            valueSpeed = Mathf.Lerp(speedAnim, 0, time);
            //setFloat("_Factor", valueText);
            getFrozen(valueText, valueSpeed);
        }
        /*else
        {
            exitFrozen();
            time = 0;
        }*/
       
    }

    void setFloat(string name, float value)
    {
        myRender.material.SetFloat(name, value);
    }

   public void getFrozen(float valueText,float valueSpeed)
    {
        setFloat("_Factor", valueText);
        myAnim.speed = valueSpeed;
        
    }
   public void exitFrozen()
    {
        setFloat("_Factor", 0);
        myAnim.speed = speedAnim;
        frezee.Play();
        ambient.Stop();
        time = 0;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fire"))
        {
            exitFrozen();
            getFrezze = false;
        }
        if (collision.gameObject.CompareTag("Ice"))
        {
            getFrezze = true;
        }
    }
}
