using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyProtectile : MonoBehaviour
{
    Rigidbody myrig;
    Vector3 dir;
    [SerializeField] float speed, destructiontime;
    private void Awake()
    {
        myrig = GetComponent<Rigidbody>();
        dir = transform.forward;
        Invoke("Destruction", destructiontime);
    }


    // Start is called before the first frame update
    void Start()
    {
        myrig.AddForce(dir * speed);
    }

    private void Destruction()
    {
        Destroy(gameObject);
    }


}
