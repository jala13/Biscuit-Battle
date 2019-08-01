using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupController : MonoBehaviour
{
    public AudioClip pickupSound;
    public GameObject pickupVFX;
    public float sizePickupVFX;
    void Start()
    {
        gameObject.transform.parent = GameObject.Find("Pickups").transform;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().SetHealth(other.gameObject.GetComponent<PlayerController>().startingHealth);
            other.gameObject.GetComponent<PlayerController>().gameController.GetComponent<GameController>().ResetHealth();

            AudioSource.PlayClipAtPoint(pickupSound, transform.position, 4);
            pickupVFX.transform.localScale = new Vector3(sizePickupVFX, sizePickupVFX, sizePickupVFX);
            GameObject tmp = Instantiate(pickupVFX, transform.position, transform.rotation);
            Destroy(tmp, 1);
            Destroy(gameObject);
        }
    }
}
