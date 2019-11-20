using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoogiemanPoof : MonoBehaviour
{
    [SerializeField]
    private Session poof;

    [SerializeField]
    private GameObject boogieMan;

    void Start()
    {
        //StartCoroutine(InvokePoofEvent());
        poof.AddStartListener(() => StartCoroutine(PoofInBoogieman()));
        poof.AddStopListener(() => StartCoroutine(PoofOutBoogieman()));
    }

    IEnumerator PoofInBoogieman()
    {
        boogieMan.SetActive(true);
        yield return null;
    }

    IEnumerator PoofOutBoogieman()
    {
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
