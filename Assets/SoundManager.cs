using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
      
    public AudioClip[] LeftLongHit;
    public AudioClip[] LeftShortHit;
    public AudioClip[] RightLongHit;
    public AudioClip[] RightShortHit;
    public AudioClip[] Miss;
    
    public AudioSource audioSource;
    public AudioListener audioListener;

    // Start is called before the first frame update
    void Start()
    {
        audioListener = GetComponent<AudioListener>();
        if (audioListener == null)
        {
            Debug.Log("audioListener is null");
        }
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.Log("audioSource is null");
        }
    }
    //(!audioSource.isPlaying)
    // Update is called once per frame
    
    internal void PlayRandom(AudioClip[] music)
    {
        audioSource.clip = music[Random.Range(0, music.Length)];
        audioSource.Play();
    }

}
