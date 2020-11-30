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
        song5
    };

    public static ExpManager instance;
    public PlaySound songPlayer;
    //public generateBlocks grid;

    public bool stroopCondition;
    public bool seatedCondition;
    public songSelector songChoice;
    private void Start()
    {
        LoadVars();
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
            DontDestroyOnLoad(gameObject);
        }
    }

    void LoadVars()
    {
        if (songChoice == songSelector.song1)
            songPlayer.index = 0;
        else if (songChoice == songSelector.song2)
            songPlayer.index = 1;
        else if (songChoice == songSelector.song3)
            songPlayer.index = 2;
        else if (songChoice == songSelector.song4)
            songPlayer.index = 3;
        else if (songChoice == songSelector.song5)
            songPlayer.index = 4;


    }


}
