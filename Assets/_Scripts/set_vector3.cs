using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class set_vector3 : MonoBehaviour
{
    [SerializeField] private ControllerObject controller;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(setter());
    }

    IEnumerator setter()
    {
        while (true)
        {
            controller.set(transform.position);
            yield return null;
        }
    }
    
}
