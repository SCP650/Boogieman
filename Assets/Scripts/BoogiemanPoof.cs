using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoogiemanPoof : MonoBehaviour
{
    [SerializeField]
    private Session poof;

    [SerializeField]
    private ParticleSystem boogiePoof;

    private set_boogie_distance _DistanceComponent;
    private Vector3 initPos;

    bool onDanceFloor = false;

    void Awake()
    {
        //StartCoroutine(InvokePoofEvent());
        poof.AddStartListener(() => PoofBoogieman());
        poof.AddStopListener(() => PoofBoogieman());
        _DistanceComponent = GetComponent<set_boogie_distance>();
        _DistanceComponent.enabled = false;
        initPos = transform.position;
        onDanceFloor = false;
    }

    public void PoofBoogieman()
    {
        onDanceFloor = !onDanceFloor;
        _DistanceComponent.enabled = true;
        Debug.Log(onDanceFloor);
        if (onDanceFloor)
        {
            _DistanceComponent.UpdatePosition();
            Debug.Log("dance" + transform.position);
        }
        else
        {
            transform.position = initPos;
            Debug.Log(transform.position);
        }
        boogiePoof.Play();
        //boogieMan.SetActive(true);

    }

    // IEnumerator InvokePoofEvent()
    // {
    //     while(true)
    //     {
    //         if(Input.GetKeyDown("p"))
    //         {
    //             poof.Invoke();
    //         }

    //         yield return null;
    //     }
    // }
}
