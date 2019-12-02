using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calibration : MonoBehaviour
{
    [SerializeField] private float scaler = 0.6f;
    [SerializeField] private ControllerObject head;
    [SerializeField] private GameObject Dancer;
    [SerializeField] private float BMHeight;
    [SerializeField] private float BMWidth;

    private float width; // the width of player's arm streach out
    private float height; // the height of player's head;
    private List<float> widths;
    private List<float> heights;

    private void Start()
    {
        heights = new List<float>();
        //StartCoroutine(startCalibration());
    }
    
    IEnumerator startCalibration()
    {
        Debug.Log("Calibration Of Width and height Starts");
        yield return StartCoroutine(AverageHeight());
        height = ListAverage(heights); //- transform.parent.position.y; need to minuse the height of the dance floor
        width = scaler * height;
        Debug.Log("The average width is " + width + "the average height is " + height);
        Debug.Log("Start resizing boogie man");
        Dancer.transform.localScale = new Vector3(width / BMWidth, height / BMHeight, 1);
    }

    /* Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Calibration Of Width and height Starts");
            widths.RemoveRange(0, widths.Count);
            heights.RemoveRange(0, heights.Count);
            StartCoroutine(AverageWidthAndHeight());
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            width = ListAverage(widths);
            height = ListAverage(heights);
            Debug.Log("The average width is " + width + "the average height is " + height);
            Debug.Log("Start resizing boogie man");
            Dancer.transform.localScale = new Vector3(width / BMWidth, height / BMHeight, 1);

        }
    }*/
    
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

    IEnumerator AverageHeight()
    {

        while (Time.time < 5)
        {
            heights.Add(head.pos.y);
            Debug.Log(head.pos.y);
            yield return null;
        }
        
    }

}
