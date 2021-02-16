using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_GameStats : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public string obstacleText = ""; //Total Obstacles:  OVERLAP 'D/TOTAL
    public string obstacleOverallapedHealth = ""; //Player Obstacle Overlapped Health: 100.0
    public string totalBlocksMissed = ""; //Total Blocks Missed #
    public string totalBlocksHit  = ""; //Total Blocks Hit %
    public string avgHitPrecision = ""; //Avg. Hit Precision: 1.0.f
    public string score = ""; //out of 100/100
    public string combo = ""; //out of 0->infinity
    public string highestCombo = ""; 
    public string stroopScore = "";//Stroop Score
    public bool isStroop = false;
    public string playerHealth = "";

    // Update is called once per frame
    void Update()
    {
		string txt = "";
	}
}







