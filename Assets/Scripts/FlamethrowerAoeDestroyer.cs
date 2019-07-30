using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerAoeDestroyer : MonoBehaviour
{
    public AudioClip playerHitSound;
    public GameObject hitVFX;
    public float sizeHitVFX;

    public AudioClip germanSoldierAHitSound;
    public AudioClip germanSoldierBHitSound;

    public bool fromEnemy;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && fromEnemy == true)
        {
            other.gameObject.GetComponent<PlayerController>().ReduceHealth();
            AudioSource.PlayClipAtPoint(playerHitSound, gameObject.transform.position);
            hitVFX.transform.localScale = new Vector3(sizeHitVFX, sizeHitVFX, sizeHitVFX);
            GameObject tmpHit = Instantiate(hitVFX, transform.position, transform.rotation);
            Destroy(tmpHit, 1);
        }

        else if (other.gameObject.tag == "German Soldier A" && fromEnemy == false)
        {
            AudioSource.PlayClipAtPoint(germanSoldierAHitSound, gameObject.transform.position);
            other.gameObject.GetComponent<GermanSoldierAController>().ReduceHealth();
            hitVFX.transform.localScale = new Vector3(sizeHitVFX, sizeHitVFX, sizeHitVFX);
            GameObject tmpHit = Instantiate(hitVFX, transform.position, transform.rotation);
            Destroy(tmpHit, 1);
        }
        else if (other.gameObject.tag == "German Soldier B" && fromEnemy == false)
        {
            AudioSource.PlayClipAtPoint(germanSoldierBHitSound, gameObject.transform.position);
            other.gameObject.GetComponent<GermanSoldierBController>().ReduceHealth();
            hitVFX.transform.localScale = new Vector3(sizeHitVFX, sizeHitVFX, sizeHitVFX);
            GameObject tmpHit = Instantiate(hitVFX, transform.position, transform.rotation);
            Destroy(tmpHit, 1);
        }
    }
}
