using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHit : MonoBehaviour
{
    Animator myAnim;
    // Start is called before the first frame update
    void Awake()
    {
        myAnim = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fire"))
        {

            myAnim.SetBool("Frozen", false);
        }
        if (collision.gameObject.CompareTag("Ice"))
        {
            myAnim.SetBool("Frozen", true);
        }  
    }
}
