using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class generateBlocks : MonoBehaviour
{

    [SerializeField] GameObject oneBlock;
 
    private List<Block> blocks;
    private MapFile map;
    private Information description;
    private List<Obstacle> obstacles;

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
        //TODO: Adrian: return a list of block that contains block informaion
        GetBlock("Assets/Resources/Normal.json", "Assets/Resources/info.json");
        blocks = map._notes;
        obstacles = map._obstacles;
        float bps = description._beatsPerMinute / 60;

        //change time to difference in seconds
        for (int i = 0; i < blocks.Count - 1; i++)
        {
            blocks[i]._time = (blocks[i + 1]._time - blocks[i]._time) / bps;
        }
        //change time to difference in seconds
        for (int i = 0; i < obstacles.Count - 1; i++)
        {
            obstacles[i]._time = (obstacles[i + 1]._time - obstacles[i]._time) / bps;
            obstacles[i]._duration = (obstacles[i]._duration)/bps;
        }

        StartCoroutine(Generate(blocks));
        StartCoroutine(generateObstacles(obstacles));

    }

    private IEnumerator generateObstacles(List<Obstacle> obsticles)
    {

        foreach (Obstacle O in obsticles)
        {
            Debug.Log(O._time);
            yield return null;
        }
    }

    private IEnumerator Generate(List<Block> blocks)
    {
        foreach (Block B in blocks)
        {
           
            //TODO: intantiate the blocks
            GameObject gb;
            if (B._type == 0)
            {
                gb = Instantiate(oneBlock);
                beat b = gb.GetComponent<beat>();
                b.color = beat.Color.red;
                b.dir = getDirection(B._cutDirection);


            }
            else
            {
                gb = Instantiate(oneBlock);
          
                beat b = gb.GetComponent<beat>();
                b.color = beat.Color.blue;
                b.dir = getDirection(B._cutDirection);

            }

            gb.transform.position = getPosition(B._lineIndex, B._lineLayer);
            if(B._time == 0)
            {
                continue;
            }
            else
            {
                yield return new WaitForSeconds(B._time);
            }

        }
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
        float y = 1.5f + row * 0.5f;
        float x = -1 + col * 0.5f;
        return new Vector3(x, y, transform.position.z);
    }
}
