using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateMove : MonoBehaviour
{
    [SerializeField] float maxRot = -180;
    [SerializeField] float minRot = 180;
    [SerializeField] GameObject leftGroup;
    [SerializeField] GameObject rightGroup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void turnPlayer()
    {
        Debug.Log("Turn");
        foreach (Transform child in leftGroup.GetComponent<Transform>())
        {
            child.Rotate(new Vector3(0, -maxRot, 0), Space.World);

        }
        foreach (Transform child in rightGroup.GetComponent<Transform>())
        {
            child.Rotate(new Vector3(0, maxRot, 0), Space.World);
        }
    }

    public void turnBoogie()
    {
        Debug.Log("Turn other");
        foreach (Transform child in leftGroup.GetComponent<Transform>())
        {
            child.Rotate(new Vector3(0, -minRot, 0), Space.World);

        }
        foreach (Transform child in rightGroup.GetComponent<Transform>())
        {
            child.Rotate(new Vector3(0, minRot, 0), Space.World);
        }
    }
}
