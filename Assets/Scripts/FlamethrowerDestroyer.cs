using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerDestroyer : MonoBehaviour
{
    //VFX
    //public GameObject bloodVFX;
   // public GameObject destroyVFX;
    public GameObject destroyAreaVFX;
    public GameObject destroyAreaObject;
    //public AudioClip playerHitSound;
    //public AudioClip germanSoldierAHitSound;
    //public AudioClip germanSoldierBHitSound;
    //public float sizeBloodVFX;
    public float sizeDestroyAreaVFX;

    //public bool fromEnemy;


    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject.name == "Player" && fromEnemy == true)
        {
            AudioSource.PlayClipAtPoint(playerHitSound, gameObject.transform.position);
            other.gameObject.GetComponent<PlayerController>().ReduceHealth();
            bloodVFX.transform.localScale = new Vector3(sizeBloodVFX, sizeBloodVFX, sizeBloodVFX);
            GameObject tmpHit = Instantiate(bloodVFX, transform.position, transform.rotation);
            Destroy(tmpHit, 1);
            Destroy(gameObject);
        }
        /*else if (other.gameObject.tag == "German Soldier A" && fromEnemy == false)
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
        }*/
        if (other.gameObject.tag != "Non Physical" && other.gameObject.tag != "Pickup" && other.gameObject.tag != "German Soldier A" && other.gameObject.tag != "German Soldier B")
        {
            destroyAreaVFX.transform.localScale = new Vector3(sizeDestroyAreaVFX, sizeDestroyAreaVFX, sizeDestroyAreaVFX);
            GameObject tmpExp = Instantiate(destroyAreaVFX, transform.position + new Vector3(0f,1f,0f), 
                                            new Quaternion(90f,0f,0f,90f));
            Destroy(tmpExp, 3);
            GameObject tmpAoe = Instantiate(destroyAreaObject, transform.position, transform.rotation);
            Destroy(tmpAoe, 3);
            Destroy(gameObject,1);
        }
    }
}
