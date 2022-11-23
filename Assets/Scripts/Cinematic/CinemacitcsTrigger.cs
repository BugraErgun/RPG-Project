using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Controller;

public class CinemacitcsTrigger : MonoBehaviour
{
    [SerializeField] GameObject Player;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag=="Player")
        {
            GetComponent<PlayableDirector>().Play();
            GetComponent<BoxCollider>().enabled = false;

            Player.GetComponent<PlayerController>().enabled = false;

            Invoke("EnableControl", 7f);
        }
        
    }


    void EnableControl()
    {
        Player.GetComponent<PlayerController>().enabled = true;
    }
}
