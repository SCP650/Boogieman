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

    public Vector3[] RecordedPositions
    {
        get
        {
            return positions;
        }
    }

    void Awake()
    {
        positions = new Vector3[0];
        start.AddListener(() => StartCoroutine(Record()));
        stop.AddListener(() => stopListener());
    }

    void stopListener()
    {
        StopAllCoroutines();
        GameObject line = Instantiate(lineObject, transform.position, transform.rotation);
        positions = new Vector3[0];
        line.GetComponent<HandleCollisions>().Setup(positions,controller);
    }

    IEnumerator Record()
    {
        while (true) {
            Vector3[] newPositions = new Vector3[positions.Length + 1];
            newPositions[0] = transform.position;

            // Update old positions by moving them forward based on how much time has passed
            for (int i = 1; i < positions.Length + 1; i++)
            {
]                newPositions[i] = positions[i - 1] + Vector3.back * config.stepSize;
                // yield return null;
            }
            positions = newPositions;
            Debug.Log(positions.Length);
            yield return new WaitForSeconds(config.stepSize);
        }
    }
}
