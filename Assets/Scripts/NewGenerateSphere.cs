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
    private List<float> heights;
    [SerializeField] private ControllerObject lefthand;
    [SerializeField] private ControllerObject righthand;
    [SerializeField] private ControllerObject head;
    [SerializeField] Material left;
    [SerializeField] Material right;
    [SerializeField] GameObject lineObject;
    [SerializeField] AttackController leftAttackController;
    [SerializeField] AttackController rightAttackController;
    [SerializeField] LineConfig config;

    private float scaler = 0.8f;
    // [SerializeField] Vector3Ref rightAttackPos; // this is confusing, this should be both left and right attack pos
    LineRenderer lr;
    private int counter = 0;
    public int numBeatWait = 4;
    private ControllerObject currController;
    private LassoGenerateLine generateLine;

    float runtime = .55f;

    bool recording;
    Vector3[] positions;
    public Vector3 initPos;
    
    private void Start()
    {
        heights = new List<float>();
        currController = righthand;
        generateLine = GetComponent<LassoGenerateLine>();
        positions = new Vector3[0];
        //TODO: add in lasso and line generation here
        leftAttackController.BallSession.AddStartListener(() => GiveMeSphere(leftAttackController.Place.val, lefthand));
        rightAttackController.BallSession.AddStartListener(() => GiveMeSphere(rightAttackController.Place.val, righthand));
        leftAttackController.LassoSession.AddStartListener(() => {initPos = AdjustInitPosition(leftAttackController.Place.val) ; recording = true; StopAllCoroutines(); StartCoroutine(Record(initPos)); });
        leftAttackController.LassoSession.AddStopListener(() => recording = false);
    
    // ballSession.AddStartListener(() => GiveMeSphere(rightAttackPos.val));
    // ballSession.AddStopListener(() => GiveMeSphere(rightAttackPos.val));
        StartCoroutine(startCalibration());
    }

    IEnumerator startCalibration()
    {
        yield return StartCoroutine(AverageHeight());
        height = ListAverage(heights) - transform.parent.position.y; //need to minuse the height of the dance floor
        width = scaler * height;
        Debug.Log(height);
    }

    private Vector3 AdjustInitPosition(Vector3 location)
    {
        return new Vector3(location.x * width / 2.0f + transform.position.x, location.y * height, transform.position.z);

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

    IEnumerator AverageHeight()
    {

        while (Time.time <5)
        {
            heights.Add( head.pos.y);
            yield return null;
        }
        
    }
    
    void GiveMeSphere(Vector3 location, ControllerObject cObject)
    {

        initPos = AdjustInitPosition(location);
        currController = cObject;

        var block = Instantiate(SpherePrefab, initPos, Quaternion.identity, null);
        block.GetComponent<HandleBlockCollisions>().Setup(currController);
        Light bL = block.GetComponent<Light>();
        var _MeshRenderer = block.GetComponent<MeshRenderer>();
        if (currController.Equals(lefthand))
        {
            bL.color = new Color(209, 157, 0);
            _MeshRenderer.material = left;
        }
        else
        {
            bL.color = new Color(255, 255, 255);
            _MeshRenderer.material = right;
        }
    }



    void stopListener()
    {
        Debug.Log("stop invoked");
        StopAllCoroutines();

        if (positions.Length > 3)
        {
            GameObject line = Instantiate(lineObject, Vector3.zero, Quaternion.identity);
            //TODO: Handle conlission in line 
            line.GetComponent<LassoHandleCollisions>().Setup(positions);
        }
     

        positions = new Vector3[0];
        lr.positionCount = 0;
        lr.SetPositions(positions);
    }

    IEnumerator Record(Vector3 iniPos)
    {
        Debug.Log("starting record");

        for (float dur = 0; dur < runtime; dur += Time.deltaTime)
        {
            Vector3[] newPositions = new Vector3[positions.Length + 1];
            newPositions[0] = iniPos;

            // Update old positions by moving them forward based on how much time has passed
            for (int i = 1; i < positions.Length + 1; i++)
            {
                newPositions[i] = positions[i - 1] + Vector3.back * config.stepSize * config.speed;
                // yield return null;
            }
         
            positions = newPositions;
            lr.positionCount = positions.Length;
            lr.SetPositions(positions);

            yield return new WaitForSeconds(config.stepSize);
        }
        stopListener();
        yield return new WaitForSeconds(runtime);
        if (recording)
        {
            yield return null;

            StartCoroutine(Record(iniPos));
            yield break;
        }
    
    }


}
