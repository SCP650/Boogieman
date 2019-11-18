using UnityEngine;
using UnityEngine.Playables;

public class Set_MConfig_Asset : PlayableAsset
{
    public float bpm = 110;
    public float beats_per_measure = 11;
    public int measures_between_p_and_boog = 1;
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<Set_MConfigPlayable>.Create(graph);
        var Reference = playable.GetBehaviour();
        Reference.bpm = bpm;
        Reference.beats_per_measure = beats_per_measure;
        Reference.measures_between_p_and_boog = measures_between_p_and_boog;
        return playable;
    }
}