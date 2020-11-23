using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField]
    AudioSource audio;

    [SerializeField]
    public int index;

    [SerializeField]
    List<AudioClip> audioClips;

    // Start is called before the first frame update
    void Start()
    {
        audio.clip = audioClips[index];
        audio.Play();
    }
}
