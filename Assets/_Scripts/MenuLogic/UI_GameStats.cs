using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_GameStats : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public Text txt;
    public string obstacleText = ""; //Total Obstacles:  OVERLAP'D/TOTAL
    public string obstacleOverallapedHealth = ""; //Player Obstacle Overlapped Health: 100.0
    public string totalBlocksMissed = ""; //Total Blocks Missed #
    public string totalBlocksHit  = ""; //Total Blocks Hit %
    public string avgHitPrecision = ""; //Avg. Hit Precision: 1.0.f
    public string score = ""; //out of currScore/maxScore
    public string combo = ""; //out of 0->infinity
    public string highestCombo = ""; 
    public string stroopScore = "";//Stroop Score
    public string playerHealth = "";

    private int totalBlocks = 0;
    private int totalObstacles = 0;
    private float maxHealth = 0;
    private bool isStroop = false;
    private bool isSeated = false;
    /*Total Obstacles:  OVERLAP'D/TOTAL
    Player Obstacle Overlapped Health: 100.0

    Total blocks misses #
    Total Blocks Hit %
    Avg.Hit Precision: 1.0

    SCORE: 100
    Stroop Score: N/A
    Combo: 100
    Highest Combo: 100
    Player Health: 55%*/

    // Update is called once per frame
    void Start()
    {
        isStroop = ExpManager.instance.stroopCondition;
        isSeated = ExpManager.instance.seatedCondition;
        maxHealth = ExpManager.instance.maxPlayerHealth;
        //totalBlocks = ExpManager.instance.totalBlocks; 
        //totalObstacles = ExpManager.instance.totalObstacles;
        //maxHealth = ExpManager.instance.maxHealth;
    }
    void Update()
    {
        obstacleOverallapedHealth = "Player Health: " + ExpManager.instance.currPlayerHealth + "% \n";
        totalBlocksMissed = "Total blocks misses: ? [TEMP]\n";
        totalBlocksHit = "Total Blocks Hit: ?/? or ?% [TEMP]\n";
        avgHitPrecision = "Avg Hit Precision: ? Block per Min [TEMP]\n";
        score = "Curr Score: " + ExpManager.instance.score.ToString() + "\n";
        combo = "Curr Combo: " + ExpManager.instance.combo.ToString() + "\n";
        highestCombo = "Max Combo: " + ExpManager.instance.maxCombo.ToString() + "\n";
        

        txt.text = obstacleOverallapedHealth + totalBlocksMissed + totalBlocksHit + avgHitPrecision
            + score + combo + highestCombo;



	}
}







