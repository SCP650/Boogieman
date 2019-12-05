using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LassoGenerateLine : MonoBehaviour
{
  
    [SerializeField] LineConfig config;
    [SerializeField] GameObject lineObject;
    [SerializeField] GameObject blockObject;
    [SerializeField] private ControllerObject lefthand;
    [SerializeField] private ControllerObject righthand;
    [SerializeField] private ControllerObject head;
    Vector3[] positions;
    public Vector3 initPos;
    [SerializeField] ControllerObject controller;
    [SerializeField] AttackController leftAttackController;
    LineRenderer lr;


    float runtime = .55f;

    bool recording;

    public Vector3[] RecordedPositions
    {
        get
        {
            return positions;
        }
    }

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        positions = new Vector3[0];

        leftAttackController.LassoSession.AddStartListener(() => { recording = true; StopAllCoroutines(); StartCoroutine(Record(initPos)); });
        leftAttackController.LassoSession.AddStopListener(() => recording = false);
      }



    void stopListener()
    {
        Debug.Log("stop invoked");
        StopAllCoroutines();

        if (positions.Length > 3) {
            GameObject line = Instantiate(lineObject, Vector3.zero, Quaternion.identity);
            line.GetComponent<HandleCollisions>().Setup(positions, controller);
        } else {
            Debug.Log("making block");
            var b = Instantiate(blockObject, transform.position, Quaternion.identity);
            b.GetComponent<HandleBlockCollisions>().Setup(controller);
            var _MeshRenderer = b.GetComponent<MeshRenderer>();
         
            //TODO: change colors in here
        }

        positions = new Vector3[0];
        lr.positionCount = 0;
        lr.SetPositions(positions);
    }

    IEnumerator Record(Vector3 iniPos)
    {
        Debug.Log("starting record");

        for (float dur = 0; dur < runtime;dur += Time.deltaTime) {
            Vector3[] newPositions = new Vector3[positions.Length + 1];
            newPositions[0] = iniPos;

            // Update old positions by moving them forward based on how much time has passed
            for (int i = 1; i < positions.Length + 1; i++)
            {
                newPositions[i] = positions[i - 1] + Vector3.back * config.stepSize * config.speed;
                // yield return null;
            }
            //TODO: hide line renderer when making blocks
            positions = newPositions;
            lr.positionCount = positions.Length;
            lr.SetPositions(positions);

            yield return new WaitForSeconds(config.stepSize);
        }
        stopListener();
        yield return new WaitForSeconds(runtime);
        if(recording)
        {
            yield return null;

            StartCoroutine(Record(iniPos));
            yield break;
        } else
        {
        }
    }
}
