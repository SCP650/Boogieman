using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class set_boogie_distance : MonoBehaviour
{
    [SerializeField]
    music_config m_config;

    [SerializeField] private LineConfig l_config;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = ((m_config.bpm / 60) / m_config.beats_per_measure) * Vector3.forward * l_config.speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}