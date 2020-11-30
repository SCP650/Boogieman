using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saber : MonoBehaviour
{
    public int layer;
    private Vector3 previousPos;
    private float rotation;
    private int toleration = 40;
    public float maxAngle = 95;
    public Rigidbody rb;
    public OVRInput.Controller OwningController;

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.Log("moan");
        }
        Messenger.Broadcast("Goodhit");

    }

    // Update is called once per frame
    void Update()
    {
        previousPos = transform.position;
    }

    //When the Primitive collides with the walls, it will reverse direction
    private void OnTriggerEnter(Collider other)
    {
        rotation = Vector3.Angle(transform.position - previousPos, other.transform.up);
        if (other.transform.gameObject.tag == "beat")
        {
            //(rotation - toleration) <= 180 && 180 <= (rotation + toleration)
            beat beatObject = other.transform.gameObject.GetComponent<beat>();
            bool validRot = rotation + other.transform.rotation.z >= (180 - toleration);
            
            
            if (validRot && layer == other.transform.gameObject.layer)//if our hit is at the required angle +- toleration
            {
                //Debug.Log("Play good note here");
                if (beatObject.isStroop)//will be replace by manager.isStroop
                {
                    FeedbackSystem.S.negativeFeedback();
                    Messenger.Broadcast("Badhit");
                }
                else
                {
                    if (layer == 9)
                    {
                        FeedbackSystem.S.positiveFeedback(FeedbackSystem.SaberSide.Left);
                    }
                    else
                    {
                        FeedbackSystem.S.positiveFeedback(FeedbackSystem.SaberSide.Right);
                    }
                    Messenger.Broadcast("Goodhit");
                }
                DataTracker.on_slice(true, true, 10.0f); // TODO - set congruent and reaction time here
            }
            else
            {
                if (beatObject.isStroop)//will be replace by manager.isStroop
                {
                    if (layer == 9)
                    {
                        FeedbackSystem.S.positiveFeedback(FeedbackSystem.SaberSide.Left);
                    }
                    else
                    {
                        FeedbackSystem.S.positiveFeedback(FeedbackSystem.SaberSide.Right);
                    }
                    Messenger.Broadcast("Goodhit");
                }
                else
                {
                    FeedbackSystem.S.negativeFeedback();
                    Messenger.Broadcast("Badhit");
                }
                
				DataTracker.on_slice(true, false, 10.0f); // TODO - set congruent and reaction time here

            }
            Destroy(other.gameObject);

        }
        else if (other.transform.gameObject.tag == "bomb") {
            Messenger.Broadcast("Badhit");
            FeedbackSystem.S.negativeFeedback();
            Destroy(other.gameObject);
        }
        
    }
     
}
