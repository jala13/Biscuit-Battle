using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GermanSoldierAController : MonoBehaviour
{
    public float speed;
    public Animator anim;
    private bool active;

    //Bullets
    public Transform bulletSpawn;
    public float fireRate;
    private float nextFire;
    public GameObject bullet;

    //Health
    public int startHealth;
    private int health;
    private bool isDead;

    //SFX
    public AudioClip germanSoldierADeathSound;

    //Player object
    private GameObject player;
    public float alertRange;

    //Drops
    public Transform biscuitSpawn;
    public GameObject biscuit;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isDead", false);
        health = startHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        nextFire = Time.time + fireRate;
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if mouse is pressed and fire a bullet, also change to correct animation state
        if (Time.time > nextFire && active && !isDead)
        {
            nextFire = Time.time + fireRate;
            anim.SetBool("isShooting", true);
            Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
        }
        else
        {
            anim.SetBool("isShooting", false);
        }
    }


    void FixedUpdate()
    {
        //Make the npc face and move towards the player
        float moveV = speed * Time.deltaTime;
        if (moveV != 0 && active && !isDead)
        {
            Vector3 playerPos = player.transform.position;
            Vector3 rotationDelta = new Vector3(playerPos.x - gameObject.transform.position.x, 0f,
                                                playerPos.z - gameObject.transform.position.z);
            Quaternion rotation = Quaternion.LookRotation(rotationDelta);
            gameObject.transform.rotation = rotation;

            anim.SetBool("isWalking", true);
            transform.Translate(0, 0, moveV);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }


    public void ReduceHealth()
    {
        if (active)
        {
            health -= 1;
        }

        if (health == 0)
        {
            isDead = true;
            anim.SetBool("isDead", true);
            AudioSource.PlayClipAtPoint(germanSoldierADeathSound, gameObject.transform.position);
            Instantiate(biscuit, biscuitSpawn.position, biscuitSpawn.rotation);
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            Destroy(gameObject, 10);
        }
    }

    public void Disable()
    {
        active = false;
    }

    public void Enable()
    {
        active = true;
    }
}
