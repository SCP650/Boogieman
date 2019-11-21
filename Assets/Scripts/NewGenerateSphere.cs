using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGenerateSphere : MonoBehaviour
{
    //this script should be attached to the center of the game object you want spheres to generate from -- Sebastian Yang, or is it?
    [SerializeField] Vec3UnityEvent spherelocation;
    [SerializeField] GameObject SpherePrefab;

    [SerializeField] Session ballSession;
    [SerializeField] Session lineSession;
    [SerializeField] Session lassoSession;


    // Testing
    //TODO: use existing Calibration script
    public float width; // the width of player's arm streach out
    public float height; // the height of player's head;
    private List<float> widths;
    private List<float> heights;
    [SerializeField] private ControllerObject lefthand;
    [SerializeField] private ControllerObject righthand;
    [SerializeField] private ControllerObject head;
    [SerializeField] Material blue;
    [SerializeField] Material red;
    // [SerializeField] AttackController leftAttackController;
    // [SerializeField] AttackController rightAttackController;
    [SerializeField] Vector3Ref rightAttackPos;
    
    private int counter = 0;
    public int numBeatWait = 4;
    private ControllerObject currController;

    private void Start()
    {
        widths = new List<float>();
        heights = new List<float>();
        currController = righthand;

        //TODO: add in lasso and line generation here
        // ballSession.AddStartListener(() => GiveMeSphere(leftAttackController.place));
        ballSession.AddStartListener(() => GiveMeSphere(rightAttackPos.val));
        // ballSession.AddStopListener(() => GiveMeSphere(rightAttackPos.val));
    }

    
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
            width = ListAverage(widths)/2.0f;
            height = ListAverage(heights)/2.0f;
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
        Debug.Log("giving me sphere");
        // Vector3 v = new Vector3(Random.Range(-1, 2) * width + transform.position.x, Random.Range(0, 2) * height + height, transform.position.z);
        Vector3 v = new Vector3(location.x * width + transform.position.x, location.y * height + height, transform.position.z);
        if (v.x < 0) {
            currController = lefthand;
        } else if (v.x > 0) {
            currController = righthand;
        } else {
            var i = Random.Range(0, 2);
            switch (i)
            {
                case 0:
                    currController = lefthand;
                    break;
                default:
                    currController = righthand;
                    break;
            }
        }

        var block = Instantiate(SpherePrefab, v, Quaternion.identity, null);
        block.GetComponent<HandleBlockCollisions>().Setup(currController);
        var _MeshRenderer = block.GetComponent<MeshRenderer>();
        if (currController.Equals(lefthand))
        {
            _MeshRenderer.material = blue;
        }
        else
        {
            _MeshRenderer.material = red;
        }
    }

    // public void TestingRoutine() {
    //     // Debug.Log("Testing routine called");
    //     // Get calibrated width and height
    //     counter++;
    //     if (counter < numBeatWait) {
    //         return;
    //     } else {
    //         counter = 0;
    //     }

    //     // Scale location to width and height
    //     float xScale = width;
    //     float yScale = height;
    //     Vector3 v = new Vector3(Random.Range(-1, 2) * xScale + transform.position.x, Random.Range(0, 2) * yScale + yScale, transform.position.z);
    //     if (v.x < 0)
    //     {
    //         currController = lefthand;
    //     } else if (v.x > 0)
    //     {
    //         currController = righthand;
    //     } else
    //     {
    //         var i = Random.Range(0, 2);
    //         switch (i)
    //         {
    //             case 0:
    //                 currController = lefthand;
    //                 break;
    //             default:
    //                 currController = righthand;
    //                 break;
    //         }
    //     }
    //     GiveMeSphere(v);
    // }
}
