using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLine : MonoBehaviour
{
    [SerializeField] UnitEvent start;
    [SerializeField] UnitEvent stop;
    [SerializeField] LineConfig config;
    [SerializeField] GameObject lineObject;
    Vector3[] positions;
    [SerializeField] ControllerObject controller;

    //TODO: also keep track of current hand on here to send to instantiated object
    public Vector3[] RecordedPositions
    {
        get
        {
            return positions;
        }
    }

    void Start()
    {
        stop.AddListener(() => stopListener());
        start.AddListener(() => StartCoroutine(Record()));
    }

    void stopListener()
    {
        StopAllCoroutines();
        GameObject line = Instantiate(lineObject, transform.position, transform.rotation);
        line.GetComponent<HandleCollisions>().Setup(positions,controller);
    }

    IEnumerator Record()
    {
        Vector3[] newPositions = new Vector3[positions.Length + 1];
        newPositions[0] = transform.position;

        // Update old positions by moving them forward based on how much time has passed
        for (int i = 1; i < positions.Length; i++)
        {
            //TODO: forwards or backwards?
            newPositions[i] = positions[i - 1] + Vector3.forward * config.stepSize;
        }
        positions = newPositions;
        yield return new WaitForSeconds(config.stepSize);
    }
}
