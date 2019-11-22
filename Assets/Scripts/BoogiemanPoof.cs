using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoogiemanPoof : MonoBehaviour
{
    [SerializeField]
    private Session poof;

    [SerializeField]
    private GameObject boogieMan;

    [SerializeField]
    private ParticleSystem boogiePoof;

    private set_boogie_distance _DistanceComponent;
    private Vector3 initPos;

    bool onDanceFloor = false;

    void Awake()
    {
        //StartCoroutine(InvokePoofEvent());
        poof.AddStartListener(PoofInBoogieman);
        poof.AddStopListener(PoofOutBoogieman);
        _DistanceComponent = GetComponent<set_boogie_distance>();
        _DistanceComponent.enabled = false;
        initPos = transform.position;
    }

    public void PoofInBoogieman()
    {
        onDanceFloor = !onDanceFloor;
        boogiePoof.Play();
        if (onDanceFloor)
        {
            _DistanceComponent.enabled = true;
            _DistanceComponent.UpdatePosition();
        }
        else
        {
            transform.position = initPos;
        }
        boogieMan.SetActive(true);
         
    }

    public void PoofOutBoogieman()
    {
        _DistanceComponent.enabled = false;
        boogieMan.SetActive(false);
      
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
