using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatInvoke : MonoBehaviour
{
    [SerializeField] private UnitEvent beat;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InvokeBeatEvent());
    }

    IEnumerator InvokeBeatEvent()
    {
        beat.Invoke();
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(InvokeBeatEvent());
    }
}
