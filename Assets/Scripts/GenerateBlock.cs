using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBlock : MonoBehaviour
{
    [SerializeField] Session session;
    [SerializeField] GameObject block;
    [SerializeField] ControllerObject controller;

    private void Awake() {
        session.AddStartListener(() => StartCoroutine(Record()));
        session.AddStopListener(() => stopListener());
    }

    void stopListener()
    {
        StopAllCoroutines();
    }

    IEnumerator Record()
    {
        var b = Instantiate(block, this.transform.position, Quaternion.identity);
        b.GetComponent<HandleBlockCollisions>().Setup(controller);

        yield return null;
    }
}
