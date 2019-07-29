using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : MonoBehaviour
{
    public float bulletSpeed;
    public AudioClip fireSound;
    public GameObject fireVFX;
    public float sizeVFX;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
        fireVFX.transform.localScale = new Vector3(sizeVFX, sizeVFX, sizeVFX);
        GameObject tmpExp = Instantiate(fireVFX, transform.position, transform.rotation);
        Destroy(tmpExp, 1);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
    }
}
