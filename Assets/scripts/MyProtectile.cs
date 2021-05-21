using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyProtectile : MonoBehaviour
{
    Rigidbody myrig;
    Vector3 dir;
    [SerializeField] float speed, destructiontime;
    AudioController audioController;
    private void Awake()
    {
        myrig = GetComponent<Rigidbody>();
        dir = transform.forward;
        audioController = GetComponent<AudioController>();
    }


    // Start is called before the first frame update
    void Start()
    {
        myrig.AddForce(dir * speed);
        audioController.PlayLoop(0);
        Invoke("Destruction", destructiontime);
    }

    private void Destruction()
    {
        Destroy(gameObject);
    }


}
