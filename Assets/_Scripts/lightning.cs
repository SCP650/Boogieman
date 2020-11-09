using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightning : MonoBehaviour
{
    [SerializeField] private float time_step = .1f;
    [SerializeField] private int resolution = 20;
    [SerializeField] private float strong_force = .1f;
    [SerializeField] private float gravity = 1;
    [SerializeField] private float random_jerk;
    private Vector3[] points;
    private Vector3[] velocities;
    private LineRenderer lr;
    
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        points = new Vector3[resolution];
        velocities = new Vector3[resolution];
        StartCoroutine(anim());
    }

    IEnumerator anim()
    {
        lr.positionCount = resolution;
        for (int i = 0; i < resolution; ++i)
        {
            points[i] = Vector3.right *  Mathf.Lerp(-1.5f, 1.5f, i / (float)resolution);
        }
        lr.SetPositions(points);
        while (true)
        {
            for (int i = 0; i < resolution; ++i)
            {
                points[i] += velocities[i] * time_step;
                if (i != 0 && i != resolution - 1)
                {
                    velocities[i] += InverseSquare(points[i - 1], points[i]) * gravity * time_step
                                     + StrongForce(points[i],points[i - 1]) * strong_force * time_step;
                    
                    velocities[i + 1] += InverseSquare(points[i + 1], points[i]) * gravity * time_step
                                         + StrongForce(points[i],points[i + 1]) * strong_force * time_step;
                }

                if (UnityEngine.Random.value > .1f)
                {
                    velocities[i] += new Vector3(
                        UnityEngine.Random.value, 
                        UnityEngine.Random.value,
                        UnityEngine.Random.value).normalized * random_jerk * time_step;
                }
            }
            lr.SetPositions(points);
            yield return new WaitForSeconds(time_step);
        }
    }

    Vector3 StrongForce(Vector3 src, Vector3 dest)
    {
        return (dest - src) * Mathf.Pow(Vector3.Distance(dest, src),2);
    }
    
    Vector3 InverseSquare(Vector3 src, Vector3 dest)
    {
        if (Vector3.Distance(dest, src) == 0)
            return Vector3.positiveInfinity;
        return (dest - src).normalized * (1 / Mathf.Sqrt(Vector3.Distance(dest, src)));
    }
}
