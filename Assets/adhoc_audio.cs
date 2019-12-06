using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adhoc_audio : MonoBehaviour
{


    [SerializeField] public AudioSource ass;
    [SerializeField] public AudioClip good;
    [SerializeField] public AudioClip bad;
    
    [SerializeField]
    int threshold = 500;
        

    [SerializeField]        
    IntRef score;
        // Start is called before the first frame update
    void Start()
    {
        ass.PlayOneShot(score.val > threshold ? good : bad);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
