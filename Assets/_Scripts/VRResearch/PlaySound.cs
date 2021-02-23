using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField]
    AudioSource audio;

    private int index;

    [SerializeField]
    List<AudioClip> audioClips;
    private void Start()
    {
        Messenger.AddListener("Start", StartPlayingMusic);

    }

    private void OnDestroy()
    {
        Messenger.RemoveListener("Start", StartPlayingMusic);
    }
    private void StartPlayingMusic()
    {
        index = ExpManager.instance.getCurrentSong();
        audio.clip = audioClips[index];
        audio.Play();
    }
      
    
}
