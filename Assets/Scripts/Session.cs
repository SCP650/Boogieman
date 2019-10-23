using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boogieman/Session")]
public class Session : ScriptableObject
{
    [SerializeField] UnitEvent start;
    [SerializeField] UnitEvent stop;
    private bool toggle = false;

    public UnitEvent StartEvent {
        get
        {
            return start;
        }
    }

    public UnitEvent StopEvent
    {
        get
        {
            return stop;
        }
    }

    public void Invoke()
    {
        if (!toggle) start.Invoke();
        else stop.Invoke();
        toggle = !toggle;
    }

    public void AddStartListener(System.Action listener)
    {
        start.AddListener(listener);
    }
    
    public void AddStopListener(System.Action listener)
    {
        stop.AddListener(listener);
    }
}
