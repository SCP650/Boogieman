using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saber : MonoBehaviour
{
    public LayerMask layer;
    private Vector3 previousPos;
    private float rotation;
    private int toleration = 30;
    public float maxAngle = 95;
    public Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.Log("bitch and moan");
        }
    }

    // Update is called once per frame
    void Update()
    {
       //rotation = Vector3.Angle(transform.position - previousPos, other.transform.up);
        previousPos = transform.position;
    }

    //When the Primitive collides with the walls, it will reverse direction
    private void OnTriggerEnter(Collider other)
    //void OnCollisionEnter(Collision collision)
    {
        rotation = Vector3.Angle(transform.position - previousPos, other.transform.up);



        if (other.transform.gameObject.tag == "beat")
        {
            Debug.Log(rotation);
        
            if ((rotation - toleration) <= 180 && 180 <= (rotation + toleration) && layer == other.transform.gameObject.layer)//if our hit is at the required angle +- toleration
            {
                Debug.Log("Play good note here");
                FeedbackSystem.S.positiveFeedback();
				DataTracker.on_slice(true, true, 10.0f);
                //do something with points/play sound?
                //TODO: this is not working, will always land in the else statement
            }
            else
            {
                FeedbackSystem.S.negativeFeedback();
				DataTracker.on_slice(true, false, 10.0f);
				//do something with points/play sound?
				Debug.Log("Play crappy note here");
            }
            Destroy(other.gameObject);
        }
        else if (other.transform.gameObject.tag == "bomb") {
            //do something with points/play sound?
            Destroy(other.gameObject);
        }
        
    }
}
