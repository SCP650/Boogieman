using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpManager : MonoBehaviour
{
    
    public enum songSelector
    {
        song1,
        song2,
        song3,
        song4,
        song5,
        song6,
        song7,
        song8,
    };

    public static ExpManager instance;
    public PlaySound songPlayer;
    //public generateBlocks grid;

    public bool stroopCondition;
    public bool seatedCondition;
    private bool gameStarted = false;

    public float maxPlayerHealth = 100.0f;
    public float currPlayerHealth = 50.0f;
    public float incHealthBy = 1f;
    public float decHealthBy = 10f;

    public int score = 0;
    public int combo = 0;
    public int maxCombo = 0;

    public songSelector songChoice;
    private void Start()
    {
        //LoadVars();
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            Messenger.AddListener("Goodhit", GoodHit);
            Messenger.AddListener("Badhit", BadHit);
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener("Goodhit", GoodHit);
        Messenger.RemoveListener("Badhit", BadHit);
    }
    public void GoodHit()
    {
        combo++;
        score++;
        Messenger.Broadcast("UpdateUI");
    }

    public void BadHit()
    {
        maxCombo = combo;
        combo = 0;
        Messenger.Broadcast("UpdateUI");
    }

    public void StartGame()
    {
       if (!gameStarted)
        {
            Messenger.Broadcast("Start");
            gameStarted = true;
        }
    }
        
  

    public int getCurrentSong()
    {
        if (songChoice == songSelector.song1)
            return 0;
        else if (songChoice == songSelector.song2)
            return 1;
        else if (songChoice == songSelector.song3)
            return 2;
        else if (songChoice == songSelector.song4)
            return 3;
        else if (songChoice == songSelector.song5)
            return 4;
        else if (songChoice == songSelector.song6)
            return 5;
        else if (songChoice == songSelector.song7)
            return 6;
        else if (songChoice == songSelector.song8)
            return 7;
        else
            return 0;
    }

    //void LoadVars()
    //{
    //    if (songChoice == songSelector.song1)
    //        songPlayer.index = 0;
    //    else if (songChoice == songSelector.song2)
    //        songPlayer.index = 1;
    //    else if (songChoice == songSelector.song3)
    //        songPlayer.index = 2;
    //    else if (songChoice == songSelector.song4)
    //        songPlayer.index = 3;
    //    else if (songChoice == songSelector.song5)
    //        songPlayer.index = 4;


    //}


}
