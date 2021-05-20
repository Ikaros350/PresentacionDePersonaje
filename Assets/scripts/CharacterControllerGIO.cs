﻿using UnityEngine;

public class CharacterControllerGIO : MonoBehaviour
{
    Animator anim;
    [SerializeField] Transform shootPos, shieldPosition;
    [SerializeField] GameObject fireProyectile, iceProyectile;
    bool frozen;
    Vector3 originalPos;
    Quaternion originalRot;
    [SerializeField] float shootCooldown, shieldCooldown;
    float shootTime, shieldTime;
    [SerializeField] RotateControl shieldControl;
    Transform shieldActualPosition;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        originalRot = transform.rotation;
        frozen = false;
        anim = GetComponent<Animator>();
        shootTime = shootCooldown;
        shieldTime = shieldCooldown;
        shieldActualPosition = shieldControl.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        shootTime += Time.deltaTime;
        shieldTime += Time.deltaTime;
        Shoot();
        RestorePosition();
        if (Input.GetKeyDown("e") && shieldTime >= shieldCooldown && !frozen)
        {

            shieldActualPosition.position = shieldPosition.position;
            Shield();
            shieldTime = 0;
        }
    }

    void Shoot()
    {
        if (Input.GetKeyDown("f") && shootTime >= shootCooldown)
        {
            GameObject gameObject = Instantiate(fireProyectile, shootPos.position, Quaternion.identity);
            shootTime = 0;
        }
        if (Input.GetKeyDown("h") && shootTime >= shootCooldown)
        {
            GameObject gameObject = Instantiate(iceProyectile, shootPos.position, Quaternion.identity);
            shootTime = 0;
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
        anim.SetTrigger("Shield");
        shieldControl.CanUseShield(true);
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
            if (!frozen)
            {
                anim.SetTrigger("Hit");
            }
            frozen = true;
        }
    }
}
