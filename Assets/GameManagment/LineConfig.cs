using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Boogieman/Config")]
public class LineConfig : ScriptableObject
{
    public float speed = 1;
    public float stepSize = 0.05f;
    public float correctHandGrace = 1;
    public float incorrectHandGrace = 1;
    
    public bool hit_from_the_end_only = true;
    public float hit_threshold = 2;
}
