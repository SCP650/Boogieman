using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saber : MonoBehaviour
{
    public int layer;
    private Vector3 previousPos;
    private float rotation; //our current rotation at time of collision
    private int toleration = 40;
    public Rigidbody rb;
    public OVRInput.Controller OwningController;
    private bool validRot = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.Log("moan");
        }

    }

    // Update is called once per frame
    void Update()
    {
        previousPos = transform.position;
    }

    //When the Primitive collides with the walls, it will reverse direction
    private void OnTriggerEnter(Collider other)
    {
        //rotation = Vector3.Angle(transform.position - previousPos, other.transform.up);

        Vector3 transformPosOnPlane = transform.position;
        Vector3 previousPosOnPlane = previousPos;
        transformPosOnPlane.z = 0;
        previousPosOnPlane.z = 0;
        rotation = Vector3.Angle(transformPosOnPlane - previousPosOnPlane, other.transform.up);

        if (other.transform.gameObject.tag == "beat")
        {
            beat beatObject = other.transform.gameObject.GetComponent<beat>();
            if (ExpManager.instance.stroopCondition)
            {
                validRot = rotation + other.transform.rotation.z <= (0 + toleration); //opposite check

            }
            else
            {
                validRot = rotation + other.transform.rotation.z >= (180 - toleration);
            }

            if (validRot && layer == other.transform.gameObject.layer)
            {//if our hit is at the required angle +- toleration
                if (layer == 9)
                {
                    FeedbackSystem.S.positiveFeedback(FeedbackSystem.SaberSide.Left);
                    Messenger.Broadcast("Goodhit");
                }
                else
                {
                    FeedbackSystem.S.positiveFeedback(FeedbackSystem.SaberSide.Right);
                    Messenger.Broadcast("Goodhit");
                }
                DataTracker.on_slice(!ExpManager.instance.stroopCondition, true, beatObject.time_since_creation());
            }
            else // this is a problem 
            {
                FeedbackSystem.S.negativeFeedback();
                Messenger.Broadcast("Badhit");
                DataTracker.on_slice(!ExpManager.instance.stroopCondition, false, beatObject.time_since_creation());
            }
            Destroy(other.gameObject);
        }
        else if (other.transform.gameObject.tag == "bomb") {
            Messenger.Broadcast("Badhit");
            FeedbackSystem.S.negativeFeedback();
            // TODO - DataTracker save bomb hit
            Destroy(other.gameObject);
        }
        
    }
     
}
