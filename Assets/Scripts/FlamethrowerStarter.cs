using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerStarter : MonoBehaviour
{
    public AudioClip fireSound;
    public GameObject fireVFX;
    public GameObject objectVFX;
    public float sizeVFX;
    public float sizeObject;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
        fireVFX.transform.localScale = new Vector3(sizeVFX, sizeVFX, sizeVFX);
        GameObject tmpFire = Instantiate(fireVFX, transform.position, transform.rotation);
        Destroy(tmpFire, 1);
        //objectVFX.transform.localScale = new Vector3(sizeObject, sizeObject, sizeObject);
        //GameObject objectFire = Instantiate(objectVFX, transform.position, transform.rotation);
        //objectFire.transform.parent = transform;
    }
}