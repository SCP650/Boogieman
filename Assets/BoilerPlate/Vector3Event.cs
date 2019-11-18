using UnityEngine;
using UnityEngine.Events;


public class Vec3UnityEvent : UnityEvent<Vector3> {}
public class Vector3Event : EventObject<Vector3,Vec3UnityEvent> {}