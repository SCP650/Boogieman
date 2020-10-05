using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[TrackBindingType(typeof(Reference<float>))]
[TrackClipType(typeof(Set_FloatAsset))]
public class Set_FloatTrack : TrackAsset
{}