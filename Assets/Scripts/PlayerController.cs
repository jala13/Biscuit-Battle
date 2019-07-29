using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Animator anim;
    public GameObject gameController;

    //Bullets
    public Transform bulletSpawn;
    public float fireRate;
    private float nextFire;
    public GameObject bullet;

    //Health
    private int health;
    private bool isDead;

    //Score
    public int biscuitsCarried;
 
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isDead", false);
        health = 3;
        gameController.GetComponent<GameController>().ResetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if mouse is pressed and fire a bullet, also change to correct animation state
        if (Input.GetMouseButtonDown(0) && Time.time > nextFire && !isDead)
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
            Vector3 planePoint = new Vector3(hit.point.x, 0f, hit.point.z);
            transform.LookAt(planePoint);
            transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
        }
    }


    public void ReduceHealth()
    {
        health -= 1;
        gameController.GetComponent<GameController>().ShowHealth(health);
        if(health == 0)
        {
            isDead = true;
            anim.SetBool("isDead", true);
        }
    }

    public void AddBiscuit(int pickedup) 
    {
        biscuitsCarried += pickedup;
        gameController.GetComponent<GameController>().ShowBiscuitsCarried(biscuitsCarried);

    }
}
