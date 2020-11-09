using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    // Attach this script to the player/integrate with player
    private int points = 0;
    // private float rotation;
    // private int toleration = 20;
    // private Vector3 previousPos;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        // float rotation = Vector3.Angle(transform.position - previousPos, other.transform.up);
        if (other.transform.gameObject.tag == "beat")
        {
           //TODO: check for correct angle of slice
            var HisDir = other.transform.GetComponent<beat>().dir;
            // if ((HisDir - toleration) <= rotation && rotation <= (HisDir + toleration))//if our hit is at the required angle +- toleration
            // { 
                points += other.gameObject.GetComponent<beat>().pointVal;
                //TODO: display particle effects
                Destroy(other.transform.gameObject);
            // }
        } else if (other.transform.gameObject.tag == "bomb") {
            points += other.gameObject.GetComponent<bomb>().pointVal;
            Destroy(other.gameObject);
        }
    }
}
