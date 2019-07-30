using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Animator anim;
    public GameObject gameController;
    private Vector3 planePoint;
    public float powerupTime;

    //Projectiles
    private bool powerupActive;
    private int powerupBank;
    private bool flamethrowerActive;
    public Transform bulletSpawn;
    public Transform flamewthrowerSpawn;
    public float flamethrowerFireRate;
    public float bulletFireRate;
    private float nextFire;
    public GameObject bullet;
    public GameObject flamethrowerProjectile;
    public float shootAngle;

    //Health
    private int health;
    private bool isDead;
    public float hitRate;
    private float nextHit;

    //Score
    public int biscuitsCarried;

    //SFX
    public AudioClip playerDeathSound;
 
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isDead", false);
        health = 3000;
        gameController.GetComponent<GameController>().ResetHealth();
        flamethrowerActive = false;
        powerupBank = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (flamethrowerActive)
        {
            if (Input.GetMouseButton(0) && Time.time > nextFire && !isDead)
            {
                nextFire = Time.time + flamethrowerFireRate;
                anim.SetBool("isShooting", true);
                GameObject instProj = Instantiate(flamethrowerProjectile, flamewthrowerSpawn.position, flamewthrowerSpawn.rotation);
                instProj.GetComponent<Rigidbody>().velocity = BallisticVelocity(planePoint, shootAngle);
            }
            else
            {
                anim.SetBool("isShooting", false);
            }
        }
        else
        {
            //Check if mouse is pressed and fire a bullet, also change to correct animation state
            if (Input.GetMouseButtonDown(0) && Time.time > nextFire && !isDead)
            {
                nextFire = Time.time + bulletFireRate;
                anim.SetBool("isShooting", true);
                Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
            }
            else
            {
                anim.SetBool("isShooting", false);
            }
        }
    }


    void FixedUpdate()
    {
        //Move the player and change to correct animation state
        float moveH = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveV = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        if ((moveH != 0 || moveV != 0) && !isDead)
        {
            anim.SetBool("isWalking", true);
            transform.Translate(moveH, 0, moveV);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        //Make the player face the mouse
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) && !isDead)
        {
            planePoint = new Vector3(hit.point.x, 0f, hit.point.z);
            transform.LookAt(planePoint);
            transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
        }
    }


    public void ReduceHealth()
    {
        if(Time.time > nextHit)
        {
            health -= 1;
            nextHit = Time.time + hitRate;
        }
        gameController.GetComponent<GameController>().ShowHealth(health);
        if(health == 0 && !isDead)
        {
            isDead = true;
            anim.SetBool("isDead", true);
            AudioSource.PlayClipAtPoint(playerDeathSound, gameObject.transform.position);
        }
    }

    public void AddBiscuit(int pickedup) 
    {
        biscuitsCarried += pickedup;
        gameController.GetComponent<GameController>().ShowBiscuitsCarried(biscuitsCarried);

    }

    Vector3 BallisticVelocity(Vector3 targetPoint, float angle)
    {
        Vector3 direction = targetPoint - gameObject.transform.position;
        float height = direction.y;
        direction.y = 0;
        float distance = direction.magnitude;
        float radiansAngle = angle * Mathf.Deg2Rad;
        direction.y = distance * Mathf.Tan(radiansAngle);
        distance += height / Mathf.Tan(radiansAngle);
        float speed = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * radiansAngle));
        return speed * direction.normalized;
    }

    public void ChangeToFlamethrower()
    {
        if (!flamethrowerActive)
        {
            flamethrowerActive = true;
            SearchForChild("w_flamethrower", transform).gameObject.SetActive(true);
            SearchForChild("backpack_flamethrower", transform).gameObject.SetActive(true);
            SearchForChild("w_rifle", transform).gameObject.SetActive(false);
            StartCoroutine(PowerupTimer());
        }
        else
        {
            powerupBank += 1;
            StartCoroutine(PowerupTimer());
        }
    }

    public void RemoveFlamethrower()
    {
        if (powerupBank == 0)
        {
            flamethrowerActive = false;
            SearchForChild("w_flamethrower", transform).gameObject.SetActive(false);
            SearchForChild("backpack_flamethrower", transform).gameObject.SetActive(false);
            SearchForChild("w_rifle", transform).gameObject.SetActive(true);
        }
        else
        {
            powerupBank -= 1;
        }
    }

    IEnumerator PowerupTimer()
    {
        yield return new WaitForSeconds(powerupTime);
        RemoveFlamethrower();
    }

    Transform SearchForChild(string name, Transform currentTransform)
    {
        foreach(Transform child in currentTransform)
        {
            if(child.name == name)
            {
                return child;
            }
            else if(child.GetChildCount() != 0)
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
