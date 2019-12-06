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

    [SerializeField] private AudioClip truce;
    [SerializeField] private AudioClip smellyoulater;
    
    private int i = 0;

    [SerializeField]        
    IntRef score;
        // Start is called before the first frame update



        void onEnable()
        {
            StartCoroutine(p());
        }
    public IEnumerator p()    
    {
        if (i == b.Length - 1)
        {
            ass.PlayOneShot(truce);
            yield return new WaitUntil(() => !ass.isPlaying);
            yield return new WaitForSeconds(.5f);
        }
        ass.PlayOneShot(score.val > b[i].threshold ? b[i].good : b[i].bad);
        if (i == b.Length - 1)
        {
            yield return new WaitUntil(() => !ass.isPlaying);
            yield return new WaitForSeconds(.5f);
            ass.PlayOneShot(smellyoulater);
        }
        i++;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
