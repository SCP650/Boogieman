using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingBlock : MonoBehaviour
{
    [SerializeField] private KeyCode trigger;

    [SerializeField] Session session;

    private void Start()
    {
        StartCoroutine(Line());
    }

    IEnumerator Line()
    {
        while (true) {
            yield return new WaitUntil(() => Input.GetKeyDown(trigger));
            session.Invoke();
            Debug.Log("Try to generate block");
            yield return new WaitUntil(() => Input.GetKeyUp(trigger));
            session.Invoke();
            Debug.Log("Try to stop block");
        }
    }
}
