using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLine : MonoBehaviour
{
    [SerializeField] UnitEvent start;
    [SerializeField] UnitEvent stop;
    [SerializeField] LineConfig config;
    Vector3[] positions;

    //TODO: also add this property to collisions class
    public Vector3[] RecordedPositions
    {
        get
        {
            return positions;
        }

        set
        {
            //TODO: call linerenderer.setpos(positions)?
        }
    }

    void Start()
    {
        stop.AddListener(() => StopAllCoroutines());
        start.AddListener(() => StartCoroutine(Record()));
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
