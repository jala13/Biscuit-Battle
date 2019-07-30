using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertController : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "German Soldier A")
        {
            other.gameObject.GetComponent<GermanSoldierAController>().Enable();
        }
        else if(other.gameObject.tag == "German Soldier B")
        {
            other.gameObject.GetComponent<GermanSoldierBController>().Enable();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "German Soldier A")
        {
            other.gameObject.GetComponent<GermanSoldierAController>().Disable();
        }
        else if (other.gameObject.tag == "German Soldier B")
        {
            other.gameObject.GetComponent<GermanSoldierBController>().Disable();
        }
    }
}
