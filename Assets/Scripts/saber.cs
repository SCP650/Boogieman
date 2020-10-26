using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saber : MonoBehaviour
{
    public LayerMask layer;
    private Vector3 previousPos;
    private float rotation;
    private int toleration = 20;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       //rotation = Vector3.Angle(transform.position - previousPos, other.transform.up);
        previousPos = transform.position;
    }

    //When the Primitive collides with the walls, it will reverse direction
    private void OnTriggerEnter(Collider other)
    {
        rotation = Vector3.Angle(transform.position - previousPos, other.transform.up);
        if (other.transform.gameObject.tag == "beat")
        {
           //var HisDir = other.transform.GetComponent<beat>().Dir;
            //Debug.Log("His dir");
            //Debug.Log(HisDir);
            //Debug.Log("My dir");
            print(rotation);
            //if ((HisDir - toleration) <= rotation && rotation <= (HisDir + toleration))//if our hit is at the required angle +- toleration
            //{ 
                Destroy(other.transform.gameObject);
            //}
        } else if (other.transform.gameObject.tag == "bomb") {
            Destroy(other.gameObject);
        }


    }
}
