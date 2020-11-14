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
            //(rotation - toleration) <= 180 && 180 <= (rotation + toleration)
            beat beatObject = other.transform.gameObject.GetComponent<beat>();
            bool validRot = rotation + other.transform.rotation.z >= (180 - toleration);
            if (beatObject.isStroop)
            {
                switch(beatObject.dir)
                {
                    case beat.Dir.top:
                        validRot = rotation + 0 >= (180 - toleration);
                        break;
                    case beat.Dir.bottom:
                        validRot = rotation + 180 >= (180 - toleration);
                        break;
                    case beat.Dir.right:
                        validRot = rotation + 90 >= (180 - toleration);
                        break;
                    case beat.Dir.left:
                        validRot = rotation + 270 >= (180 - toleration);
                        break;
                }
            }
            if (validRot && layer == other.transform.gameObject.layer)//if our hit is at the required angle +- toleration
            {
                Debug.Log("Play good note here");
                if (layer == 9)
                {
                    FeedbackSystem.S.positiveFeedback(FeedbackSystem.SaberSide.Left);
                }
                else
                {
                    FeedbackSystem.S.positiveFeedback(FeedbackSystem.SaberSide.Right);
                }
                DataTracker.on_slice(true, true, 10.0f); // TODO - set congruent and reaction time here

            }
            else
            {
                FeedbackSystem.S.negativeFeedback();
				DataTracker.on_slice(true, false, 10.0f); // TODO - set congruent and reaction time here
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
