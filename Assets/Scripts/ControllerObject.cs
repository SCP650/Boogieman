using System.Collections;
using System.Collections.Generic;
using TypeUtil;
using UnityEngine;

[System.Serializable]
public struct controllerSet {
    public ControllerObject leftHand;
    public ControllerObject rightHand;
    public ControllerObject head;
}

[CreateAssetMenu(menuName = "ControllerObject")]
public class ControllerObject : ScriptableObject, Writer<Vector3>, Reader<Vector3>
{
    public Vector3 pos;

    public Unit set(Vector3 v)
    {
        pos = v;
        return new Unit();
    }

    public Vector3 get()
    {
        return pos;
    }
}