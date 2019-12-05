using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class is_lassoing : MonoBehaviour
{
    [SerializeField] public BoolRef hand_is_lassoing;

    [Range(0, 2)]
    [SerializeField]
    float cur_fill;

    private Vector3 velocity;
    
    private void Start()
    {
        StartCoroutine(go());
    }

    IEnumerator go()
    {
        StartCoroutine(record_vel());
        cur_fill = 0.0f;
        var max_fill = 2;
        var decay = .2f;
        while(true)
        {
            var prev_vel = velocity;

            hand_is_lassoing.val = cur_fill > 0.005f;
            yield return null;
            cur_fill -= decay * Time.deltaTime;

            var points = Vector3.SqrMagnitude(velocity - prev_vel) > .0005f ? Vector3.Dot(velocity, prev_vel) * 6 : 0;
            if (points < 0)
            {
                yield return new WaitForSeconds(.005f);
                cur_fill *= 1f - Time.deltaTime;
            }
            else
                cur_fill += points;

            cur_fill = Mathf.Clamp(cur_fill, 0.0f, max_fill);
        }
    }

    /*
    IEnumerator go()
    {
        StartCoroutine(record_vel());
        while (true)
        {
            yield return null;
            var time = Time.time;
            cur_dir = velocity;
            yield return new WaitUntil(() => Time.time >= time + 2 || Vector3.Angle(cur_dir,velocity) > 90);
            
            hand_is_lassoing.val = time + 2 < Time.time && (reverse_dir == Vector3.zero || Vector3.Angle(velocity, reverse_dir) < 135);
            
            reverse_dir = -cur_dir;

        }
    }*/


    IEnumerator record_vel()
    {
        Vector3 pos;
        while (true)
        {
            var avg = Vector3.zero;
            for (int i = 0; i < 5; i++)
            {
                pos = transform.position + transform.up * .25f;
                var t = Time.time;
                yield return new WaitUntil(() => (transform.position + transform.up * .25f - pos).sqrMagnitude > .001f);
                avg += (transform.position + transform.up * .25f - pos) / (Time.time - t);
            }

            velocity = avg / 10;
            
        }
    }


}
