using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUpdate : MonoBehaviour
{
    [SerializeField] IntEvent ScorePoint;
    [SerializeField] int scoreIncreaseNumber;

    private GameObject starLamps;
    private int ScorePointInvokes, starIndex;

    // Start is called before the first frame update
    void Start()
    {
        ScorePointInvokes = 0;
        starIndex = 0;
        starLamps = this.gameObject;
        ScorePoint.AddListener(unit => UpdateScore());
    }

    // Update is called once per frame
    IEnumerator UpdateScore()
    {
        ScorePointInvokes++;

        if(ScorePointInvokes >= scoreIncreaseNumber)
        {
            if(starIndex < starLamps.transform.ChildCount)
            {
                Material m = starLamps.transform.GetChild(starIndex++).gameObject.GetComponent<Renderer>().material;
                m.EnableKeyword("_EMISSION");
            }
        }
        yield return null;
    }
}
