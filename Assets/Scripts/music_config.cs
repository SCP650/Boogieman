using UnityEngine;

[CreateAssetMenu(menuName = "music_config")]
public class music_config : ScriptableObject
{
    [SerializeField] public float bpm = 110;
    [SerializeField] public float beats_per_measure = 4;
}