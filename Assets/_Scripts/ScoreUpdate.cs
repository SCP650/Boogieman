using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUpdate : MonoBehaviour
{
    [SerializeField] IntEvent ScorePoint;
    [SerializeField] int scoreIncreaseNumber;
    [SerializeField]private GameObject[] starLamps;

    public int ScorePointInvokes, starIndex;

    // Start is called before the first frame update
    void Start()
    {
        ScorePointInvokes = 0;
        starIndex = 0;
        ScorePoint.AddListener(i => UpdateScore(i));
    }

    // Update is called once per frame
    void UpdateScore(int i)
    {
        ScorePointInvokes += i;

        if(ScorePointInvokes >= scoreIncreaseNumber)
        {
            ScorePointInvokes = 0;
            if(starIndex < starLamps.Length)
            {
                starLamps[starIndex].GetComponent<Light>().enabled = true;
                Material m = starLamps[starIndex++].GetComponent<Renderer>().material;
                m.EnableKeyword("_EMISSION");
            }
        }
    }
}
