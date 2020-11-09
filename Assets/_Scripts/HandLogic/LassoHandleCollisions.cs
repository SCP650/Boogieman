using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using TMPro;
using Valve.VR;

[RequireComponent(typeof(LineRenderer))]
public class LassoHandleCollisions : MonoBehaviour
{
    private LineRenderer lr;
    [SerializeField] private ParticleSystem goodParticlePrefab;
    [SerializeField] private ParticleSystem badParticlePrefab;
    [SerializeField] private LineConfig config;
    private ControllerObject RightController;
    private ControllerObject LeftController;
    [SerializeField] private controllerSet controllers;
    [SerializeField] private IntEvent ScorePoint;
    [SerializeField] private LineRenderer lr_prefab;
    [SerializeField] private GameObject endIndicator;
    [SerializeField] private SteamVR_Action_Vibration HapticAction;
    [SerializeField] private BoolRef left_lasso;     
    [SerializeField] private BoolRef right_lasso;
    
    
    private SteamVR_Input_Sources leftControllerHand, rightControllerHand, hapticController;
    private Vector3[] points;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        points = new Vector3[0];
        lr = GetComponent<LineRenderer>();
        if (lr == null) Debug.LogError("Why isn't there a line renderer");
        leftControllerHand = SteamVR_Input_Sources.LeftHand;
        rightControllerHand = SteamVR_Input_Sources.RightHand;
        LeftController = controllers.leftHand;
        RightController = controllers.rightHand;
      
    }


    public void Setup(Vector3[] new_points)
    {
        this.points = new_points;
        lr.positionCount = points.Length;
        lr.SetPositions(points);

        endIndicator = Instantiate(endIndicator,transform);
        var _MeshRenderer = endIndicator.GetComponent<MeshRenderer>();
     
            lr_prefab.startColor = (Color.blue);
            lr_prefab.endColor = Color.blue;
  
        StartCoroutine(CheckCollisions());
        StartCoroutine(set_hitable());
    }


    IEnumerator set_hitable()
    {
        while (true)
        {
            yield return null;
            int i = 0;
            for(; i < points.Length;i++)
            {
                if (points[i].z + transform.position.z < config.hit_threshold)
                {
                    break;
                }
            }
            
            var grad = new Gradient();
            GradientAlphaKey[] ks = new[]
            {
                new GradientAlphaKey(.2f,0), 
                new GradientAlphaKey(.2f, i / (float) points.Length - .05f),
                new GradientAlphaKey(1, i / (float) points.Length +.05f),
                new GradientAlphaKey(1,1) 
            };
            grad.SetKeys(lr.colorGradient.colorKeys,ks);
            lr.colorGradient = grad;
        }
    }

    //TODO: delete and reset points array when removing points
    IEnumerator CheckCollisions()
    {
        while (true)
        {
            for (int i = 0; i < points.Length; ++i)
            {
                var point = points[i] + transform.position;
                Debug.Log(config == null);
                Debug.Log(LeftController == null);
                Debug.Log(RightController == null);
                if (CheckController(point, config.correctHandGrace + Vector3.Distance(LeftController.pos,RightController.pos)))
                {
                    if(config.hit_from_the_end_only && i != points.Length - 1)
                        continue;

                  
                    Instantiate(goodParticlePrefab, point,Quaternion.identity);
                    HapticAction.Execute(0, 0.2f, 30, 0.5f, leftControllerHand);
                    HapticAction.Execute(0, 0.2f, 30, 0.5f, rightControllerHand);
                    StartCoroutine(DestroyRest(i + 1));
                    ScorePoint.Invoke(1);
                    break;
                }
                if ( LeftController != null && RightController != null) //our controller is not the head
                {
                    if(config.hit_from_the_end_only)
                        continue;
                    
                    if (CheckController(point, config.incorrectHandGrace))
                    {
                        StartCoroutine(DestroyRest(i));
                        break;
                    }
                }
            }

            if (points.Length == 0)
            {
                Destroy(endIndicator);
                Destroy(gameObject);
                yield break;
            }
            endIndicator.transform.localPosition = points.Last();
            yield return null;
        }

    }

    IEnumerator DestroyRest(int j)
    {
        float speed = 5;


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

    bool CheckController( Vector3 position,float dist)
    {
        return (right_lasso.val && left_lasso.val) && (Vector3.Distance(LeftController.pos, position) < dist || Vector3.Distance(RightController.pos, position) < dist); 
    }
    
 
}
