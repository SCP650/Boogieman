using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class allLights : MonoBehaviour
{
    [SerializeField] GameObject[] all;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void allOff()
    {
        foreach (GameObject gameobject in all)
        {
            gameobject.SetActive(false);
        }
    }
    public void allOn ()
    {
        foreach (GameObject gameobject in all)
         {
            gameobject.SetActive(true);
        }
    }
}
