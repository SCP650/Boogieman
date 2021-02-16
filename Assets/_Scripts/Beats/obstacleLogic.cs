using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleLogic : MonoBehaviour
{
    // Start is called before the first frame update
    Player player = null;


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.tag == "MainCamera")
        {
            Debug.Log("Player's here!");
            player = other.transform.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.StartDealingDamage();
                Messenger.Broadcast("BadHit");
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.gameObject.tag == "MainCamera")
        {
            if (player != null)
            {
                Debug.Log("Player's inside t h e  z o n e");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.tag == "MainCamera")
        {
            if (player != null)
            {
                Debug.Log("Player's gone");
                player.StopDealingDamage();
            }
        }
    }
}
