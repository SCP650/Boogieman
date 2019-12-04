using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUpdate : MonoBehaviour
{
    [SerializeField] IntEvent ScorePoint;
    // Start is called before the first frame update
    void Start()
    {
        ScorePoint.AddListener(UpdateScore);
    }

    // Update is called once per frame
    void UpdateScore()
    {
        
    }
}
