using System;
using UnityEngine.Playables;

public class Set_AttackPlayable : PlayableBehaviour
{
    public AttackOption val;
    public AttackController contr;

    private float t = 0;

    private PlayableGraph pg;
    private Session s;
    
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        base.OnBehaviourPlay(playable, info);
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if(pg.IsValid())
            if( pg.IsPlaying())
                s.StopEvent.Invoke();
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        
        if (!s)
        {
            contr = (AttackController)playerData;
            switch (val.attackType)
            {
                case AttackType.ball:
                    s = contr.BallSession;
                    break;
                case AttackType.line:
                    s = contr.LineSession;
                    break;
                case AttackType.lasso:
                    s = contr.LassoSession;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            contr.Place.val = val.pos();
            contr.Orientation.val = val.dir();
            s.StartEvent.Invoke();
            pg = playable.GetGraph();
        }
    }
}