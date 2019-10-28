﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calibration : MonoBehaviour
{
    [SerializeField] private ControllerObject lefthand;
    [SerializeField] private ControllerObject righthand;
    [SerializeField] private ControllerObject head;
    [SerializeField] private GameObject Dancer;
    [SerializeField] private float BMHeight;
    [SerializeField] private float BMWidth;
    private float width; // the width of player's arm streach out
    private float height; // the height of player's head;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Calibration Of Width and height Starts");

            StartCoroutine(AverageWidthAndHeight());
            Debug.Log("The average width is " + width + "the average height is " + height);
            Debug.Log("Start resizing boogie man");
            Dancer.transform.localScale = new Vector3(width/BMWidth, height/BMHeight, 1);
        }
    }
    
    private float ListAverage(List<float> temp)
    {
        float sum = 0;
        float count = 0;
        foreach(float f in temp)
        {
            count++;
            sum += f;
        }
        return sum / count;
    }

    IEnumerator AverageWidthAndHeight()
    {
        List<float> widths = new List<float>();
        List<float> heights = new List<float>();
        while (true) {
            if (Input.GetKeyUp(KeyCode.C))
            {
                width = ListAverage(widths);
                height = ListAverage(heights);

                yield break;
            }
            
            widths.Add(Vector3.Distance(lefthand.pos, righthand.pos));
            heights.Add(Vector3.Distance(Vector3.zero, head.pos));

            yield return new WaitForSeconds(1);
        }
    }

}
