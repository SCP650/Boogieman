using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandleBlockCollisions : MonoBehaviour
{
    private LineRenderer lr;
    [SerializeField] private ParticleSystem goodParticlePrefab;
    [SerializeField] private ParticleSystem badParticlePrefab;
    [SerializeField] private LineConfig config;
    [SerializeField] private controllerSet controllers;
    [SerializeField] private IntEvent ScorePoint;
    [SerializeField] private SteamVR_Action_Vibration HapticAction;

    private SteamVR_Input_Sources leftControllerHand, rightControllerHand, hapticController;

    private ControllerObject controller;
    private ControllerObject otherController;
    private MeshRenderer _MeshRenderer;

    private void Awake()
    {
        leftControllerHand = SteamVR_Input_Sources.LeftHand;
        rightControllerHand = SteamVR_Input_Sources.RightHand;
    }

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
                if (controller == controllers.leftHand)
                {
                    hapticController = leftControllerHand;
                }
                else
                {
                    hapticController = rightControllerHand;
                }

                HapticAction.Execute(0, 0.2f, 30, 0.5f, hapticController);
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

            if (transform.position.z <= config.hit_threshold && _MeshRenderer.material.color.a < 1) {
                var mat = _MeshRenderer.material.color;
                _MeshRenderer.material.color = new Color(mat.r, mat.g, mat.b, 1);
                Color colour = _MeshRenderer.material.GetColor("_EmissionColor");


                // Make color brighter
                _MeshRenderer.material.SetColor("_EmissionColor", colour * 3.0f);
            }
        }
    }

    bool CheckController(ControllerObject checkController, Vector3 position, float dist)
    {
        return Vector3.Distance(checkController.pos, position) < dist;
    }
}
