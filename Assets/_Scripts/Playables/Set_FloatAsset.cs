using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Set_FloatAsset : PlayableAsset
{
    [SerializeField] private float constant;
    [SerializeField] private AnimationCurve curve;
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<Set_FloatPlayable>.Create(graph);
        var script = playable.GetBehaviour();
        script.val = constant;
        script.curve = curve;
        return playable;
    }
}