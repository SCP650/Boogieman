using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Boogieman/Session")]
public class Session : ScriptableObject
{
    [SerializeField] UnitEvent start;
    [SerializeField] UnitEvent stop;
    private bool toggle = false;

    public UnitEvent StartEvent {
        get
        {
            if (start == null)
                start = CreateInstance<UnitEvent>();
            return start;
        }
    }

    public UnitEvent StopEvent
    {
        get
        {
            if (stop == null)
                stop = CreateInstance<UnitEvent>();
            return stop;
        }
    }

    public void SetToggle(bool b) {
        toggle = b;
    }

    public void Invoke()
    {
        Debug.Log(toggle);
        if (!toggle) StartEvent.Invoke();
        else StopEvent.Invoke();
        toggle = !toggle;
    }

    public void AddStartListener(System.Action listener)
    {
        StartEvent.AddListener(listener);
    }
    
    public void AddStopListener(System.Action listener)
    {
        StopEvent.AddListener(listener);
    }
}
