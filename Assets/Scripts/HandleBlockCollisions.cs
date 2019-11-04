using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleBlockCollisions : MonoBehaviour
{
    private LineRenderer lr;
    [SerializeField] private ParticleSystem goodParticlePrefab;
    [SerializeField] private ParticleSystem badParticlePrefab;
    [SerializeField] private LineConfig config;
    [SerializeField] private controllerSet controllers;
    [SerializeField] private IntEvent ScorePoint;

    private ControllerObject controller;
    private ControllerObject otherController;
    private MeshRenderer _MeshRenderer;

    public void Setup(ControllerObject new_controller)
    {
        _MeshRenderer = this.GetComponent<MeshRenderer>();
        controller = new_controller;
        if (controller == controllers.leftHand)
            otherController = controllers.rightHand;
        if (controller == controllers.rightHand)
            otherController = controllers.rightHand;

        StartCoroutine(CheckCollisions());
        StartCoroutine(SetHitable());
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

    IEnumerator SetHitable() {
        while (true) {
            yield return null;

            if (transform.position.z <= config.hit_threshold) {
                var mat = _MeshRenderer.material.color;
                _MeshRenderer.material.color = new Color(mat.r, mat.g, mat.b, 1);
            }
        }
    }

    bool CheckController(ControllerObject checkController, Vector3 position, float dist)
    {
        return Vector3.Distance(checkController.pos, position) < dist;
    }
}
