using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// A behaviour that is attached to a playable
public class Set_SessionPlayable : PlayableBehaviour
{
    public Session s;
    // Called when the owning graph starts playing
    public override void OnGraphStart(Playable playable)
    {
       
    }

    // Called when the owning graph stops playing
    public override void OnGraphStop(Playable playable)
    {
        
    }

    // Called when the state of the playable is set to Play
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        
    }

    // Called when the state of the playable is set to Paused
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        s.StopEvent.Invoke();
    }

    // Called each frame while the state is set to Play
    public override void PrepareFrame(Playable playable, FrameData info)
    {
        
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (!s)
        {
            s = playerData as Session;
            s.StartEvent.Invoke();
        }
    }
}
