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
        transform.position = Vector3.forward * l_config.speed * 60 * (1 / m_config.bpm) * m_config.beats_per_measure;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}