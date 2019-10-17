using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct controllerSet {
    ControllerObject leftHand;
    ControllerObject rightHand;
    ControllerObject head;
}

[CreateAssetMenu(menuName = "ControllerObject")]
public class ControllerObject : ScriptableObject
{
    Vector3 pos;
}
