using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class set_boogie_distance : MonoBehaviour
{
    [SerializeField]
    music_config m_config;
    float measure_delay = 2;

    [SerializeField] private LineConfig l_config;
    // Start is called before the first frame update
    void Start()
    {
        transform.position += Vector3.forward * l_config.speed * 60 * (1 / m_config.bpm) * 
                             m_config.beats_per_measure * m_config.measures_between_p_and_boog;
    }

    public void UpdatePosition()
    {
        transform.position = new Vector3(0, transform.position.y, 0);
        transform.position += Vector3.forward * l_config.speed * 60 * (1 / m_config.bpm) *
                             m_config.beats_per_measure * m_config.measures_between_p_and_boog;
    }

}