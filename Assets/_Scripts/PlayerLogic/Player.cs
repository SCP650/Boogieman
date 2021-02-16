using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private InputManager inputManager;

    private Camera m_MainCamera;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //This gets the Main Camera (player's Camera) from the Scene
        m_MainCamera = Camera.main; //This gets a camera with the tag "MainCamera" on it. 
        m_MainCamera.enabled = true; //Should still be enabled by default, I believe...
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("We Hit Something");
    }

}
