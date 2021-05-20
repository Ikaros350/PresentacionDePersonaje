using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    Transform mytransform;
    Vector3 dir = Vector3.right;
    [SerializeField] float speed;
    Rigidbody myrig;
    [SerializeField] ExplosionEffects explotion;
    [SerializeField] ParticleSystem proyectile;
    Collider col;
    // Start is called before the first frame update
    void Awake()
    {
        col = GetComponent<Collider>();
        mytransform = GetComponent<Transform>();
        myrig = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        myrig.AddForce(dir * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        explotion.Play = true;
        col.enabled = false;
        myrig.velocity = dir * 0f;
        proyectile.gameObject.SetActive(false);
    }

}
