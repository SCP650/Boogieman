using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleBlockCollisions : MonoBehaviour
{
    private LineRenderer lr;
    [SerializeField] private ParticleSystem goodParticlePrefab;
    [SerializeField] private ParticleSystem badParticlePrefab;
    [SerializeField] private LineConfig config;
    private ControllerObject controller;
    private ControllerObject otherController;
    [SerializeField] private controllerSet controllers;
    [SerializeField] private IntEvent ScorePoint;

    public void Setup(ControllerObject new_controller)
    {
        controller = new_controller;
        if (controller == controllers.leftHand)
            otherController = controllers.rightHand;
        if (controller == controllers.rightHand)
            otherController = controllers.rightHand;

        StartCoroutine(CheckCollisions());
    }

    IEnumerator CheckCollisions()
    {
        while (true)
        {
            if (CheckController(controller, transform.position, config.correctHandGrace))
            {
                Instantiate(goodParticlePrefab, transform.position, Quaternion.identity);
                ScorePoint.Invoke(1);
                Destroy(gameObject);
            }
            yield return null;
        }
    }

    bool CheckController(ControllerObject checkController, Vector3 position, float dist)
    {
        return Vector3.Distance(checkController.pos, position) < dist;
    }
}
