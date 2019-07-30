using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyer : MonoBehaviour
{
    //VFX
    public GameObject bloodVFX;
    public GameObject destroyVFX;
    public AudioClip destroySound;
    public AudioClip playerHitSound;
    public AudioClip germanSoldierAHitSound;
    public AudioClip germanSoldierBHitSound;
    public float sizeBloodVFX;
    public float sizeDestroyVFX;

    public bool fromEnemy;


    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            AudioSource.PlayClipAtPoint(playerHitSound, gameObject.transform.position);
            other.gameObject.GetComponent<PlayerController>().ReduceHealth();
            bloodVFX.transform.localScale = new Vector3(sizeBloodVFX, sizeBloodVFX, sizeBloodVFX);
            GameObject tmpHit = Instantiate(bloodVFX, transform.position, transform.rotation);
            Destroy(tmpHit, 1);
            Destroy(gameObject);
        }
        else if(other.gameObject.tag == "German Soldier A" && fromEnemy == false)
        {
            AudioSource.PlayClipAtPoint(germanSoldierAHitSound, gameObject.transform.position);
            other.gameObject.GetComponent<GermanSoldierAController>().ReduceHealth();
            bloodVFX.transform.localScale = new Vector3(sizeBloodVFX, sizeBloodVFX, sizeBloodVFX);
            GameObject tmpHit = Instantiate(bloodVFX, transform.position, transform.rotation);
            Destroy(tmpHit, 1);
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "German Soldier B" && fromEnemy == false)
        {
            AudioSource.PlayClipAtPoint(germanSoldierBHitSound, gameObject.transform.position);
            other.gameObject.GetComponent<GermanSoldierBController>().ReduceHealth();
            bloodVFX.transform.localScale = new Vector3(sizeBloodVFX, sizeBloodVFX, sizeBloodVFX);
            GameObject tmpHit = Instantiate(bloodVFX, transform.position, transform.rotation);
            Destroy(tmpHit, 1);
            Destroy(gameObject);
        }
        else if(other.gameObject.tag != "Non Physical" && other.gameObject.tag != "Pickup")
        {
            AudioSource.PlayClipAtPoint(destroySound, gameObject.transform.position);
            destroyVFX.transform.localScale = new Vector3(sizeDestroyVFX, sizeDestroyVFX, sizeDestroyVFX);
            GameObject tmpExp = Instantiate(destroyVFX, transform.position, transform.rotation);
            Destroy(tmpExp, 1);
            Destroy(gameObject);
        }
    }
}
