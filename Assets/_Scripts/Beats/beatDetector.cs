using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beatDetector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    //void OnCollisionEnter(Collision collision)
    {
        if (other.transform.gameObject.tag == "beat")
        {
            //Dom's stuff goes here!
            Destroy(other.gameObject);
        }
    }
}