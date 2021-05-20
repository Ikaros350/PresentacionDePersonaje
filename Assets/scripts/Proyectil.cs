using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    Transform mytransform;
    Vector3 dir = Vector3.right;
   [SerializeField] float speed;
    Rigidbody myrig;
    // Start is called before the first frame update
    void Awake()
    {
        mytransform = GetComponent<Transform>();
        myrig = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        myrig.AddForce(dir * speed);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
