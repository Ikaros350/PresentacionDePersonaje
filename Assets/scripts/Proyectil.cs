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
    [SerializeField] Transform player;
    // Start is called before the first frame update
    void Awake()
    {
        col = GetComponent<Collider>();
        myrig = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        dir = new Vector3(player.transform.position.x - transform.position.x,
            player.transform.position.y - transform.position.y,
            player.transform.position.z - transform.position.z).normalized;
        transform.LookAt(player);
        myrig.AddForce(dir * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        explotion.Play = true;
        col.enabled = false;
        myrig.velocity = dir * 0f;
        proyectile.gameObject.SetActive(false);
        Invoke("Destruction", 9f);
    }

    private void Destruction()
    {
        Destroy(gameObject);
    }

}
