using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiscuitPickupController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.parent = GameObject.Find("Pickups").transform;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().AddBiscuit(1);
        }
        Destroy(gameObject);
    }
}
