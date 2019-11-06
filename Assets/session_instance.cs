using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class session_instance : MonoBehaviour
{
    [SerializeField] public Session val;

    private void OnEnable()
    {
        val.StartEvent.Invoke();
    }

    private void OnDisable()
    {
        val.StopEvent.Invoke();
    }

}
