using UnityEngine;

public class CharacterControllerGIO : MonoBehaviour
{
    Animator anim;
    [SerializeField] Transform shootPos, shieldPosition, myProyectilePos;
    [SerializeField] GameObject fireProyectile, iceProyectile, myProyectile;
    bool frozen;
    Vector3 originalPos;
    Quaternion originalRot;
    [SerializeField] float shootCooldown, shieldCooldown, lightningCooldown, thunderCooldown;
    float shootTime, shieldTime, thunderTime;
    [SerializeField] RotateControl shieldControl;
    Transform shieldActualPosition;
    Lightning thunder;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        originalRot = transform.rotation;
        frozen = false;
        anim = GetComponent<Animator>();
        shootTime = shootCooldown;
        shieldTime = shieldCooldown;
        thunderTime = lightningCooldown;
        shieldActualPosition = shieldControl.gameObject.transform;
        thunder = GetComponent<Lightning>();
    }

    // Update is called once per frame
    void Update()
    {
        shootTime += Time.deltaTime;
        shieldTime += Time.deltaTime;
        thunderTime += Time.deltaTime;
        /*ShootFire();
        ShootIce();
        RestorePosition();
        Shield();
        Thunder();
        Exit();*/
    }

    public void ShootFire()
    {
        if (/*Input.GetKeyDown("f") &&*/ shootTime >= shootCooldown && thunderTime >= thunderCooldown)
        {
            GameObject gameObject = Instantiate(fireProyectile, shootPos.position, Quaternion.identity);
            shootTime = 0;
        }
        
    }

    public void ShootIce()
    {
        if (/*Input.GetKeyDown("h") &&*/ shootTime >= shootCooldown && thunderTime >= thunderCooldown)
        {
            GameObject gameObject = Instantiate(iceProyectile, shootPos.position, Quaternion.identity);
            shootTime = 0;
        }
    }

    public void RestorePosition()
    {
        //if (Input.GetKeyDown("r"))
        //{
            transform.position = originalPos;
            transform.rotation = originalRot;
        //}
    }

    public void Shield()
    {
        if (/*Input.GetKeyDown("e") &&*/ shieldTime >= shieldCooldown && !frozen && shootTime >= shootCooldown + 2f && thunderTime >= thunderCooldown)
        {
            shieldActualPosition.position = shieldPosition.position;
            anim.SetTrigger("Shield");
            shieldControl.CanUseShield();
            shieldTime = 0;
            Invoke("ShootMyProyectile", 6f);
        }  
    }

    public void Thunder()
    {
        if (/*Input.GetKeyDown("t") &&*/ thunderTime >= thunderCooldown && shieldTime >= shieldCooldown && shootTime >= shootCooldown + 2f)
        {
            thunderTime = 0f;
            thunder.Active(lightningCooldown);
        }
            
    }

    void ShootMyProyectile()
    {
        GameObject gameObject = Instantiate(myProyectile, myProyectilePos.position, myProyectilePos.rotation);
    }

    public void Exit()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
            Application.Quit();
        //} 
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
