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

    bool onDanceFloor = true;

    void Start()
    {
        //StartCoroutine(InvokePoofEvent());
        poof.AddStartListener(() => StartCoroutine(PoofInBoogieman()));
        poof.AddStopListener(() => StartCoroutine(PoofOutBoogieman()));
        _DistanceComponent = GetComponent<set_boogie_distance>();
        _DistanceComponent.enabled = false;
    }

    IEnumerator PoofInBoogieman()
    {
        boogiePoof.Play();
        _DistanceComponent.enabled = true;
        boogieMan.SetActive(true);
        yield return null;
    }

    IEnumerator PoofOutBoogieman()
    {
        _DistanceComponent.enabled = false;
        boogieMan.SetActive(false);
        yield return null;
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
