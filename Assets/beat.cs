using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beat : MonoBehaviour
{
    // Start is called before the first frame update
    public float Dir;
    void Start()
    {
        gameObject.transform.Rotate(0.0f, 0.0f, Dir);
        Debug.Log(gameObject.tag);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
