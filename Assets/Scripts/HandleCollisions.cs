using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(LineRenderer))]
public class HandleCollisions : MonoBehaviour
{
    //TODO: add in particle system
    private LineRenderer lr;
    [SerializeField] private ParticleSystem goodParticlePrefab;
    [SerializeField] private ParticleSystem badParticlePrefab;
    [SerializeField] private LineConfig config;
    private ControllerObject controller;
    private ControllerObject otherController;
    [SerializeField] private controllerSet controllers;
    [SerializeField] private IntEvent ScorePoint;
    [SerializeField] private LineRenderer lr_prefab;

    private Vector3[] points;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        points = new Vector3[0];
        lr = GetComponent<LineRenderer>();
        if (lr == null) Debug.LogError("Why isn't there a line renderer");
    }


    public void Setup(Vector3[] new_points,ControllerObject new_controller)
    {
        this.points = new_points;
        lr.positionCount = points.Length;
        lr.SetPositions(points);

        controller = new_controller;
        if (controller == controllers.leftHand)
            otherController = controllers.rightHand;
        if (controller == controllers.rightHand)
            otherController = controllers.rightHand;
        
        StartCoroutine(CheckCollisions());
    }

    //TODO: delete and reset points array when removing points
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
        float speed = 3;


        var new_points = points.Skip(j).ToArray();

        
        points = points.Take(j - 1).ToArray();
        
        lr.positionCount = points.Length;
        lr.SetPositions(points);
        if(new_points.Length == 0)
            yield break;
        
        var new_line = Instantiate(lr_prefab,transform,false);
        new_line.positionCount = new_points.Count();
        new_line.SetPositions(new_points);
        var magic_wand = Instantiate(badParticlePrefab, new_points[0] + transform.position, Quaternion.identity);
        
        for (; new_points.Length > 0;new_points = new_points.Skip(1).ToArray())
        {
            var dest = new_points[0] + transform.position;
            while (Vector3.Distance(magic_wand.transform.position, dest) > .1f)
            {
                magic_wand.transform.position += (dest - magic_wand.transform.position).normalized * speed * Time.deltaTime;
                yield return null;
            }
            new_line.positionCount = new_points.Count();
            new_line.SetPositions(new_points);
        }
        Destroy(new_line);
    }

    bool CheckController(ControllerObject checkController, Vector3 position,float dist)
    {
        return Vector3.Distance(checkController.pos, position) < dist;
    }
    
 
}
