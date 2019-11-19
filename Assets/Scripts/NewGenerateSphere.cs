using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGenerateSphere : MonoBehaviour
{
    //this script should be attached to the center of the game object you want spheres to generate from -- Sebastian Yang, or is it?
    [SerializeField] Vec3UnityEvent spherelocation;
    [SerializeField] GameObject SpherePrefab;

    // Testing
    //TODO: use existing Calibration script
    public float width; // the width of player's arm streach out
    public float height; // the height of player's head;
    private List<float> widths;
    private List<float> heights;
    [SerializeField] private ControllerObject lefthand;
    [SerializeField] private ControllerObject righthand;
    [SerializeField] private ControllerObject head;
    private int counter = 0;
    public int numBeatWait = 4;

    private void Start()
    {
        widths = new List<float>();
        heights = new List<float>();

        // StartCoroutine(TestingRoutine())
    }

    // void Start()
    // {
        // spherelocation.AddListener(GiveMeSphere);
    // }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Debug.Log("Calibration Of Width and height Starts");
            widths.RemoveRange(0, widths.Count);
            heights.RemoveRange(0, heights.Count);
            StartCoroutine(AverageWidthAndHeight());
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            width = ListAverage(widths);
            height = ListAverage(heights);
            // Debug.Log("The average width is " + width + "the average height is " + height);
            // Debug.Log("Start resizing boogie man");
            // Dancer.transform.localScale = new Vector3(width / BMWidth, height / BMHeight, 1);

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

        while (!Input.GetKeyUp(KeyCode.C))
        {
            
            widths.Add(Vector3.Distance(lefthand.pos, righthand.pos));
            
            heights.Add( head.pos.y);

            yield return null;
        }
        
    }
    
    void GiveMeSphere(Vector3 location)
    {
        var obj = Instantiate(SpherePrefab, location, Quaternion.identity, transform);
        obj.transform.parent = null;
    }

    public void TestingRoutine() {
        // Debug.Log("Testing routine called");
        // Get calibrated width and height
        counter++;
        if (counter < numBeatWait) {
            return;
        } else {
            counter = 0;
        }

        // Scale location to width and height
        float xScale = width/2.0f;
        float yScale = height/2.0f;
        GiveMeSphere(new Vector3(Random.Range(-1f, 1f) * xScale + transform.position.x, Random.Range(-1f, 1f) * yScale + transform.position.y, transform.position.z));
    }
}
