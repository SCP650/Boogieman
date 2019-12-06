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
    [SerializeField] AttackController leftAttackController;
    [SerializeField] AttackController rightAttackController;
    private float scaler = 0.8f;
    // [SerializeField] Vector3Ref rightAttackPos; // this is confusing, this should be both left and right attack pos
    
    private int counter = 0;
    public int numBeatWait = 4;
    [SerializeField] LineConfig config;
    [SerializeField] music_config m_config;
    [SerializeField] GameObject lineObject;
    private ControllerObject currController;
    private bool recording;
    LineRenderer lr;
    Vector3[] positions;
    public Vector3 initPos;
    float runtime = .55f;

    private void Start()
    {
        heights = new List<float>();
        currController = righthand;
        lr = GetComponent<LineRenderer>();
        positions = new Vector3[0];

        //TODO: add in lasso and line generation here
        leftAttackController.BallSession.AddStartListener(() => GiveMeSphere(leftAttackController.Place.val, lefthand));
        rightAttackController.BallSession.AddStartListener(() => GiveMeSphere(rightAttackController.Place.val, righthand));
        // ballSession.AddStartListener(() => GiveMeSphere(rightAttackPos.val));
        // ballSession.AddStopListener(() => GiveMeSphere(rightAttackPos.val));
        leftAttackController.LassoSession.AddStartListener(() => { initPos = UpdateInitPos(leftAttackController.Place.val, true); recording = true; StartCoroutine(Record(initPos)); });
        leftAttackController.LassoSession.AddStopListener(() => recording = false);

        StartCoroutine(startCalibration());

    }

    private Vector3 UpdateInitPos(Vector3 location, bool lasso)
    {
       
        var v = new Vector3(location.x * width / 2.0f + transform.position.x, location.y * height + transform.parent.position.y, transform.position.z);
        if (lasso) return v + Vector3.forward * config.speed * 60 * (1 / m_config.bpm) *
                             m_config.beats_per_measure * m_config.measures_between_p_and_boog;
        else return v;

    }

    IEnumerator startCalibration()
    {
        yield return StartCoroutine(AverageHeight());
        height = ListAverage(heights) - transform.parent.position.y; //need to minuse the height of the dance floor
        width = scaler * height;
        Debug.Log("the height is :" + height + "the width is " + width);
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

        initPos = UpdateInitPos(location, false);
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
            line.GetComponent<LassoHandleCollisions>().Setup(positions);
        }
       

        positions = new Vector3[0];
    }

    IEnumerator Record(Vector3 iniPos)
    {
        Debug.Log("starting record");

        while(recording)
        {
            Vector3[] newPositions = new Vector3[positions.Length + 1];
            newPositions[0] = iniPos;

            // Update old positions by moving them forward based on how much time has passed
            for (int i = 1; i < positions.Length + 1; i++)
            {
                newPositions[i] = positions[i - 1] + Vector3.back * config.stepSize * config.speed;
                // yield return null;
            }
            //TODO: hide line renderer when making blocks
            positions = newPositions;

            yield return new WaitForSeconds(config.stepSize);
        }
        stopListener();
     
    }
}
