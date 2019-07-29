using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyer : MonoBehaviour
{
    //VFX
    public GameObject bloodVFX;
    public GameObject destroyVFX;
    public AudioClip destroySound;
    public float sizeBloodVFX;
    public float sizeDestroyVFX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().ReduceHealth();
            bloodVFX.transform.localScale = new Vector3(sizeBloodVFX, sizeBloodVFX, sizeBloodVFX);
            GameObject tmpHit = Instantiate(bloodVFX, transform.position, transform.rotation);
            Destroy(tmpHit, 1);
            Destroy(gameObject);
        }
        else if(other.gameObject.name != "Drop Zone")
        {
            AudioSource.PlayClipAtPoint(destroySound, gameObject.transform.position);
            destroyVFX.transform.localScale = new Vector3(sizeDestroyVFX, sizeDestroyVFX, sizeDestroyVFX);
            GameObject tmpExp = Instantiate(destroyVFX, transform.position, transform.rotation);
            Destroy(tmpExp, 1);
            Destroy(gameObject);
        }
    }
}
