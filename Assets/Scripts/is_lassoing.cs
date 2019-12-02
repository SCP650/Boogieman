using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class is_lassoing : MonoBehaviour
{
    [SerializeField] public BoolRef hand_is_lassoing;

    private Vector3 reverse_dir;

    private Vector3 cur_dir;

    private Vector3 velocity;
    
    private void Start()
    {
        StartCoroutine(go());
    }


    IEnumerator go()
    {
        while (true)
        {
            yield return null;
            var time = Time.time;
            cur_dir = velocity;
            yield return new WaitUntil(() => Time.time > time + 2 || Vector3.Angle(cur_dir,velocity) > 90);
            
            hand_is_lassoing.val = reverse_dir != Vector3.zero && Vector3.Angle(velocity, reverse_dir) > 45;
            
            reverse_dir = -cur_dir;

        }
    }


    IEnumerator record_vel()
    {
        Vector3 pos;
        while (true)
        {
            var avg = Vector3.zero;
            for (int i = 0; i < 10; i++)
            {
                pos = transform.position;
                yield return null;
                avg += (transform.position - pos) / Time.deltaTime;
            }

            velocity = avg / 10;
            
        }
    }


}
