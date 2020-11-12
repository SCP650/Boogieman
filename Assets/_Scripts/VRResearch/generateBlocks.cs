using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class generateBlocks : MonoBehaviour
{

    [SerializeField] GameObject oneBlock;

    [SerializeField] GameObject ObsticlePreFab;

    [SerializeField] GameObject oneBomb;

    [Tooltip("Difference between player and spawner divided by speed")]
    [SerializeField] float bufferTime = 2.5f;
    [SerializeField] float DistanceBetweenBlocks = 0.5f;



    private List<Block> blocks;
    private MapFile map;
    private Information description;
    private List<Obstacle> obstacles;
    private List<float> BlockTimeDiff = new List<float>();
    private List<float> ObsticleTimeDiff = new List<float>();


    public GameObject musicPlayer;

    [SerializeField]
    List<string> songBlockArray;
    [SerializeField]
    List<string> songInfoArray;

    public void GetBlock(string songname, string infofile)
    {
        using (StreamReader x = File.OpenText(infofile))
        {
            string info = x.ReadToEnd();
            description = JsonConvert.DeserializeObject<Information>(info);
        }

        using (StreamReader r = File.OpenText(songname))
        {
            string song = r.ReadToEnd();
            map = JsonConvert.DeserializeObject<MapFile>(song);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        musicPlayer = GameObject.Find("MusicPlayer");
        PlaySound playSoundScript = musicPlayer.GetComponent<PlaySound>();
        int songIndex = playSoundScript.index;
        Debug.Log(songIndex);

        GetBlock(songBlockArray[songIndex], songInfoArray[songIndex]);
        blocks = map._notes;
        obstacles = map._obstacles;
        float bps = description._beatsPerMinute / 60;

        //change time to difference in seconds
        BlockTimeDiff.Add(blocks[0]._time / bps - bufferTime);

        for (int i = 1; i < blocks.Count; i++)
        {


            BlockTimeDiff.Add((blocks[i]._time - blocks[i - 1]._time) / bps);
        }

        //change time to difference in seconds
        ObsticleTimeDiff.Add(obstacles[0]._time / bps - bufferTime);

        obstacles[0]._duration /= bps;
        for (int i = 1; i < obstacles.Count; i++)
        {
            ObsticleTimeDiff.Add((obstacles[i]._time - obstacles[i - 1]._time) / bps);
            obstacles[i]._duration /= bps;

        }

        // Reset data tracker before the song starts
        DataTracker.reset_tracked_data();

        StartCoroutine(Generate(blocks));
        StartCoroutine(generateObstacles(obstacles));

    }

    private IEnumerator generateObstacles(List<Obstacle> obsticles)
    {

        for (int i = 0; i < obsticles.Count; i++)
        {
            Obstacle O = obsticles[i];
            if (ObsticleTimeDiff[i] != 0)
            {
                yield return new WaitForSeconds(ObsticleTimeDiff[i]);
            }


            if (O._type == 0) //a verticle block 
            {
                GameObject gb = Instantiate(ObsticlePreFab);
                float x = O._width * DistanceBetweenBlocks;

                float y = 3 * DistanceBetweenBlocks;//all three rows

                float z = O._duration * 10;//block moving speed
                gb.transform.localScale = new Vector3(x, y, z);
                gb.transform.position = getPosition(O._lineIndex + 1, 0);
            }
            else if (O._type == 1)
            {
                GameObject gb = Instantiate(ObsticlePreFab);
                float x = O._width * DistanceBetweenBlocks; //all four cols

                float y = DistanceBetweenBlocks;

                float z = O._duration * 10;//block moving speed
                gb.transform.localScale = new Vector3(x, y, z);
                gb.transform.position = getPosition(2, O._lineIndex + 1);
            }
        }

    }

    private IEnumerator Generate(List<Block> blocks)
    {

        for (int i = 0; i < blocks.Count; i++)
        {

            Block B = blocks[i];

            if (BlockTimeDiff[i] != 0)
            {
                yield return new WaitForSeconds(BlockTimeDiff[i]);
            }


            GameObject gb;
            if (B._type == 0)
            {
                gb = Instantiate(oneBlock);
                beat b = gb.GetComponent<beat>();
                b.color = beat.Color.red;
                b.dir = getDirection(B._cutDirection);


            }
            else if (B._type == 3)
            {
                gb = Instantiate(oneBomb);
                gb.GetComponent<beat>().isMine = true;
            }
            else
            {
                gb = Instantiate(oneBlock);

                beat b = gb.GetComponent<beat>();
                b.color = beat.Color.blue;
                b.dir = getDirection(B._cutDirection);

            }

            gb.transform.position = getPosition(B._lineIndex, B._lineLayer);

        }
        // After all the blocks have spawned, save the data.
        // TODO - have to wait until all the blocks are actually gone too (sliced or missed)
        DataTracker.Save();
    }

    private beat.Dir getDirection(int i)
    {
        switch (i)
        {
            case 0:
                return beat.Dir.top;

            case 1:
                return beat.Dir.bottom;
            case 2:
                return beat.Dir.left;

            default:
                return beat.Dir.right;

        }

    }

    private Vector3 getPosition(int col, int row)
    {
        float y = 1.5f + row * DistanceBetweenBlocks;
        float x = getX(col);
        return new Vector3(x, y, transform.position.z);
    }

    private float getX(int col)
    {
        return -1 + col * DistanceBetweenBlocks;
    }
}
