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
    private List<float> heights;
    [SerializeField] private ControllerObject lefthand;
    [SerializeField] private ControllerObject righthand;
    [SerializeField] private ControllerObject head;
    [SerializeField] Material blue;
    [SerializeField] Material red;
    [SerializeField] AttackController leftAttackController;
    [SerializeField] AttackController rightAttackController;
    [SerializeField] float scaler = 0.6f;
    // [SerializeField] Vector3Ref rightAttackPos; // this is confusing, this should be both left and right attack pos
    
    private int counter = 0;
    public int numBeatWait = 4;
    private ControllerObject currController;


    private void Start()
    {
        heights = new List<float>();
        currController = righthand;

        //TODO: add in lasso and line generation here
        ballSession.AddStartListener(() => GiveMeSphere(leftAttackController.Place.val, lefthand));
        ballSession.AddStartListener(() => GiveMeSphere(rightAttackController.Place.val, righthand));
        // ballSession.AddStartListener(() => GiveMeSphere(rightAttackPos.val));
        // ballSession.AddStopListener(() => GiveMeSphere(rightAttackPos.val));
        StartCoroutine(startCalibration());
    }

    IEnumerator startCalibration()
    {
        yield return StartCoroutine(AverageHeight());
        height = ListAverage(heights) - transform.parent.position.y; //need to minuse the height of the dance floor
        width = scaler * height;
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

        while (!Input.GetKeyUp(KeyCode.C))
        {
            heights.Add( head.pos.y);
            yield return null;
        }
        
    }
    
    void GiveMeSphere(Vector3 location, ControllerObject cObject)
    {
        Debug.Log("giving me sphere");
        Vector3 v = new Vector3(location.x * width/2.0f + transform.position.x, location.y * height/2.0f + height/2.0f*(5/4f), transform.position.z);
        currController = cObject;

        var block = Instantiate(SpherePrefab, v, Quaternion.identity, null);
        block.GetComponent<HandleBlockCollisions>().Setup(currController);
        Light bL = block.GetComponent<Light>();
        var _MeshRenderer = block.GetComponent<MeshRenderer>();
        if (currController.Equals(lefthand))
        {
            bL.color = new Color(209, 157, 0);
            _MeshRenderer.material = blue;
        }
        else
        {
            bL.color = new Color(255, 255, 255);
            _MeshRenderer.material = red;
        }
    }

}
