using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct audioBundle

{
[SerializeField] public AudioClip good;
[SerializeField] public AudioClip bad;
[SerializeField] public int threshold;
}

public class adhoc_audio : MonoBehaviour
{


    [SerializeField] public AudioSource ass;

    [SerializeField] private audioBundle[] b;

    private int i = 0;

    [SerializeField]        
    IntRef score;
        // Start is called before the first frame update
    void onEnable()
    {
        ass.PlayOneShot(score.val > b[i].threshold ? b[i].good : b[i].bad);
        i++;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
