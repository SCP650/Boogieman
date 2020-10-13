using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saber : MonoBehaviour
{
    public LayerMask layer;
    private Vector3 previousPos;
    private float rotation;
    private int toleration;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward,out hit, 1, layer)) ;
        {
            rotation = Vector3.Angle(transform.position - previousPos, hit.transform.up);
            if (hit.transform.gameObject.tag == "beat")
            {
                var HisDir = hit.transform.GetComponent<beat>().Dir;
                if (rotation >= HisDir - toleration || rotation <= HisDir + toleration)//if our hit is at the required angle +- toleration
                {
                    Destroy(hit.transform.gameObject);
                }
            }
        }
        previousPos = transform.position;
    }
}
