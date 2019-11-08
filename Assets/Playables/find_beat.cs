using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[ExecuteInEditMode]
public class find_beat : MonoBehaviour
{
    [SerializeField] private AudioSource audio;

    [SerializeField] private float threshold;

    [SerializeField] private MarkerTrack mt;
    [SerializeField] private TrackAsset g;

    [SerializeField]
    Playable p;

    [SerializeField] private PlayableDirector d;
    
    
    
    [SerializeField] private SignalAsset beat;
    // Update is called once per frame
    void OnEnable()
    {
        
        int len = 1 << 10;
        float[] samples = new float[len];
        audio.GetSpectrumData(samples,0, FFTWindow.Rectangular);
        for (int i = 0; i < len; i++)
        {
            if (samples[i] > threshold)
            {
                g.CreateMarker<SignalEmitter>(len / (float) i).asset = beat;
            }
        }
    }

    private void OnDisable()
    {
        foreach (var m in g.GetMarkers())
        {
            g.DeleteMarker(m);
        }
    }
    
}
