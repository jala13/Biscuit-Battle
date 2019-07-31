using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZoneController : MonoBehaviour
{
    public int buiscuitsAtBase;
    public GameObject gameController;

    public AudioClip dropSound;
    public GameObject dropVFX;
    public float sizedropVFX;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<PlayerController>().biscuitsCarried > 0)
            {
                AudioSource.PlayClipAtPoint(dropSound, transform.position);
                dropVFX.transform.localScale = new Vector3(sizedropVFX, sizedropVFX, sizedropVFX);
                GameObject tmp = Instantiate(dropVFX, transform.position, transform.rotation);
                Destroy(tmp, 1);

                buiscuitsAtBase += other.gameObject.GetComponent<PlayerController>().biscuitsCarried;
                other.gameObject.GetComponent<PlayerController>().AddBiscuit(-other.gameObject.GetComponent<PlayerController>().biscuitsCarried);
                gameController.GetComponent<GameController>().SetScore(buiscuitsAtBase);
            }
        }
    }
}
