using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GermanSoldierBController : MonoBehaviour
{
    public float speed;
    public Animator anim;
    private bool active;

    //Bullets
    public Transform bulletSpawn;
    public float fireRate;
    private float nextFire;
    public GameObject flamethrowerProj;
    public float shootAngle;

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
    public Transform weaponSpawn;
    public GameObject flamethrower;

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
        if(player.GetComponent<PlayerController>().GetHealth() < 1)
        {
            active = false;
        }

        if (Time.time > nextFire && active && !isDead)
        {
            nextFire = Time.time + fireRate;
            anim.SetBool("isShooting", true);
            GameObject instProj = Instantiate(flamethrowerProj, bulletSpawn.position, bulletSpawn.rotation);
            instProj.GetComponent<Rigidbody>().velocity = BallisticVelocity(player, shootAngle);
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
        if (active && !isDead)
        {
            Vector3 playerPos = player.transform.position;
            Vector3 rotationDelta = new Vector3(playerPos.x - gameObject.transform.position.x, 0f,
                                                playerPos.z - gameObject.transform.position.z);
            Quaternion rotation = Quaternion.LookRotation(rotationDelta);
            gameObject.transform.rotation = rotation;

            anim.SetBool("isWalking", true);
            transform.Translate(0, 0, moveV);
        }
        else if(!active && !isDead)
        {
            Vector3 playerPos = player.transform.position;
            Vector3 rotationDelta = new Vector3(playerPos.x - gameObject.transform.position.x, 0f,
                                                playerPos.z - gameObject.transform.position.z);
            Quaternion rotation = Quaternion.LookRotation(rotationDelta);
            gameObject.transform.rotation = rotation;
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
            Instantiate(flamethrower, weaponSpawn.position, weaponSpawn.rotation);
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ |
                                                               RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            Destroy(gameObject, 10);
        }
    }
    Vector3 BallisticVelocity(GameObject target, float angle)
    {
        Vector3 direction = target.transform.position - gameObject.transform.position;
        float height = direction.y;
        direction.y = 0;
        float distance = direction.magnitude;
        float radiansAngle = angle * Mathf.Deg2Rad;
        direction.y = distance * Mathf.Tan(radiansAngle);
        distance += height / Mathf.Tan(radiansAngle);
        float speed = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * radiansAngle));
        return speed * direction.normalized;
    }
    public void Disable()
    {
        active = false;
        SearchForChild("Bip001", transform).gameObject.SetActive(false);
        SearchForChild("Body_german_C", transform).gameObject.SetActive(false);
        SearchForChild("backpack_flamethrower", transform).gameObject.SetActive(false);
    }

    public void Enable()
    {
        active = true;
        SearchForChild("Bip001", transform).gameObject.SetActive(true);
        SearchForChild("Body_german_C", transform).gameObject.SetActive(true);
        SearchForChild("backpack_flamethrower", transform).gameObject.SetActive(true);
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
