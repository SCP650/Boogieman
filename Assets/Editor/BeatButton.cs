using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(beat))]
public class BeatButton : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        beat myScript = (beat)target;
        if (GUILayout.Button("Set block settings"))
        {
            myScript.SetBlock();
        }
    }
}
