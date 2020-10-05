using UnityEngine.Playables;

public class Set_MConfigPlayable : PlayableBehaviour
{
    public float bpm = 110;
    public float beats_per_measure = 11;
    public int measures_between_p_and_boog = 1;
    public music_config Reference;

    private float t = 0;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        Reference = playerData as music_config;
        
        Reference.bpm = bpm;
        Reference.beats_per_measure = beats_per_measure;
        Reference.measures_between_p_and_boog = measures_between_p_and_boog;

    }
}