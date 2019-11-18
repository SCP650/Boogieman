using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class spawn_beat_asset : PlayableAsset
{
    [Polar(1)] public Vector2 loc;
    public Vector2 point;
    
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        return ScriptPlayable<spawn_beat_playable>.Create(graph);
    }
}
