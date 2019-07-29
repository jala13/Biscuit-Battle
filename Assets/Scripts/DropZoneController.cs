using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZoneController : MonoBehaviour
{
    public int buiscuitsAtBase;
    public GameObject gameController;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            buiscuitsAtBase += other.gameObject.GetComponent<PlayerController>().biscuitsCarried;
            other.gameObject.GetComponent<PlayerController>().AddBiscuit(-other.gameObject.GetComponent<PlayerController>().biscuitsCarried);
            gameController.GetComponent<GameController>().ShowScore(buiscuitsAtBase);
        }
    }
}
