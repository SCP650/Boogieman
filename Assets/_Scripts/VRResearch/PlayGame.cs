using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGame : MonoBehaviour
{
    public GameObject musicPlayer;
    public GameObject grid;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void StartGame()
    {
        musicPlayer.SetActive(true);
        grid.SetActive(true);
    }
}
