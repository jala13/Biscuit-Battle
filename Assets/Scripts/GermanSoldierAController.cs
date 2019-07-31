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
        Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerController>().GetHealth() < 1)
        {
            active = false;
        }

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
            //gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ |
                                                               RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            anim.SetBool("isDead", true);
            AudioSource.PlayClipAtPoint(germanSoldierADeathSound, gameObject.transform.position);
            Instantiate(biscuit, biscuitSpawn.position, biscuitSpawn.rotation);
            Destroy(gameObject, 10);
        }
    }

    public void Disable()
    {
        active = false;
        SearchForChild("Bip001", transform).gameObject.SetActive(false);
        SearchForChild("Body_german_A", transform).gameObject.SetActive(false);

    }

    public void Enable()
    {
        active = true;
        SearchForChild("Bip001", transform).gameObject.SetActive(true);
        SearchForChild("Body_german_A", transform).gameObject.SetActive(true);
    }

    Transform SearchForChild(string name, Transform currentTransform)
    {
        foreach (Transform child in currentTransform)
        {
            if (child.name == name)
            {
                return child;
            }
            else if (child.GetChildCount() != 0)
            {
                Transform deeperChild = SearchForChild(name, child);
                if (deeperChild.name == name)
                {
                    return deeperChild;
                }
            }
        }
        return transform;
    }
}
