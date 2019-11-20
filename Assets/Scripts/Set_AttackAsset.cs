using UnityEngine;
using UnityEngine.Playables;

public class Set_AttackAsset : PlayableAsset
{
    [SerializeField] private AttackOption option;
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<Set_AttackPlayable>.Create(graph);
        var script = playable.GetBehaviour();
        script.val = option;
        return playable;
    }
}