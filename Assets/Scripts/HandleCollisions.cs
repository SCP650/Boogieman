using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class HandleCollisions : MonoBehaviour
{
    private LineRenderer lr;
    [SerializeField] private ParticleSystem goodParticlePrefab;
    [SerializeField] private ParticleSystem badParticlePrefab;
    [SerializeField] private LineConfig config;
    [SerializeField] private ControllerObject controller;
    private ControllerObject otherController;
    [SerializeField] private controllerSet controllers;
    [SerializeField] private IntEvent ScorePoint;

    private Vector3[] points;
    
    
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        if (controller == controllers.leftHand)
            otherController = controllers.rightHand;
        if (controller == controllers.rightHand)
            otherController = controllers.rightHand;
    }


    public void Setup(Vector3[] new_points)
    {
        this.points = new_points;
    }


    IEnumerator CheckCollisions()
    {
        while (true)
        {
            for (int i = points.Length - 1; i >= 0; --i)
            {
                var point = points[i] + transform.position;
                if (CheckController(controller, point, config.correctHandGrace))
                {
                    Instantiate(goodParticlePrefab, point,Quaternion.identity);
                    StartCoroutine(DestroyRest(i + 1));
                    ScorePoint.Invoke(1);
                    yield return new WaitForSeconds(.05f);
                    break;
                }
                if (otherController != null) //our controller is not the head
                {
                    if (CheckController(otherController, point, config.incorrectHandGrace))
                    {
                        StartCoroutine(DestroyRest(i));
                        break;
                    }
                }
            }
            yield return null;
        }
    }

    IEnumerator DestroyRest(int j)
    {
        for (; j < points.Length; j++)
        {
            Instantiate(badParticlePrefab, points[j] + transform.position, Quaternion.identity);
            yield return new WaitForSeconds(.1f);
        }
    }

    bool CheckController(ControllerObject checkController, Vector3 position,float dist)
    {
        return Vector3.Distance(checkController.pos, position) < dist;
    }
}
