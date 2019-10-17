using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLine : MonoBehaviour
{
    UnitEvent start;
    UnitEvent stop;

    void Start()
    {
        stop.AddListener(() => StopAllCoroutines());
        start.AddListener(() => StartCoroutine(Record()));
        // update unitevents start and stop, add listener to both
    }

    IEnumerator Record()
    {
        yield return null; //WaitForSeconds(config.stepSize)
    }

    //store array as a property in this class and in the colliissions class
    // call setpos on line renderer every time you change array as a setter (not necessarily setPos though)
}
