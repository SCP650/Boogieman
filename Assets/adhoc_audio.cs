using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adhoc_audio : MonoBehaviour
{


    [SerializeField] public AudioSource ass;
    
    [SerializeField]
    int threshold = 500;
        

    [SerializeField]        
    IntRef score;
        // Start is called before the first frame update
    void Start()
    {
        if(score.val > threshold)
            ass.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
