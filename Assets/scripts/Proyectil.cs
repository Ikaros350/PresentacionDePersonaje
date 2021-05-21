using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    [SerializeField] Vector3 dir = Vector3.right;
    [SerializeField] float speed;
    Rigidbody myrig;
    [SerializeField] ExplosionEffects explotion;
    [SerializeField] ParticleSystem proyectile;
    Collider col;
    Transform player;
    AudioController audioController;
    bool active;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Target").GetComponent<Transform>();
        col = GetComponent<Collider>();
        myrig = GetComponent<Rigidbody>();
        audioController = GetComponent<AudioController>();
        active = true;
    }
    private void Start()
    {
        dir = new Vector3(player.transform.position.x - transform.position.x,
            player.transform.position.y - transform.position.y,
            player.transform.position.z - transform.position.z).normalized;
        transform.LookAt(player);
        myrig.AddForce(dir * speed);
        if (active)
        {
            audioController.PlayLoop(1);
        }
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        explotion.Play = true;
        col.enabled = false;
        myrig.velocity = dir * 0f;
        proyectile.gameObject.SetActive(false);
        active = false;
        audioController.Stop();
        audioController.PlayAction(0);
        Invoke("Destruction", 9f);
    }

    private void Destruction()
    {
        Destroy(gameObject);
    }

}
