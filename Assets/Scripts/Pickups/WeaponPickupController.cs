using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickupController : MonoBehaviour
{
    void Start()
    {
        gameObject.transform.parent = GameObject.Find("Pickups").transform;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().ApplyFlamethrower();
            Destroy(gameObject);
        }
    }
}
