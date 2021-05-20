using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerGIO : MonoBehaviour
{
    Animator anim;
    [SerializeField] Transform shootPos;
    [SerializeField] GameObject fireProyectile, iceProyectile;
    bool frozen;
    Vector3 originalPos;
    Quaternion originalRot;
    [SerializeField] float shootCooldown;
    float time;
    
    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        originalRot = transform.rotation;
        frozen = false;
        anim = GetComponent<Animator>();
        time = shootCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        Shoot();
        RestorePosition();
    }

    void Shoot()
    {
        if (Input.GetKeyDown("f") && time >= shootCooldown)
        {
            GameObject gameObject = Instantiate(fireProyectile, shootPos.position, Quaternion.identity);
            time = 0;
        }
        if (Input.GetKeyDown("h") && time >= shootCooldown)
        {
            GameObject gameObject = Instantiate(iceProyectile, shootPos.position, Quaternion.identity);
            time = 0;
        }
    }

    void RestorePosition()
    {
        if (Input.GetKeyDown("r"))
        {
            transform.position = originalPos;
            transform.rotation = originalRot;
        }
    }

    void Shield()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fire"))
        {
            if (!frozen)
            {
                anim.SetTrigger("Hit");
            }
            frozen = false;
        }
        if (collision.gameObject.CompareTag("Ice"))
        {
            frozen = true;
            anim.SetTrigger("Hit");
        }
    }
}
