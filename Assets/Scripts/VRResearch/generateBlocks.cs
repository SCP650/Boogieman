using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class generateBlocks : MonoBehaviour
{

    [SerializeField] GameObject RedBlock;
    [SerializeField] GameObject BlueBlock;
    private List<Block> blocks;
    private MapFile map;
    private Information description;

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
        GetBlock("Assets/Normal.json", "Assets/info.json");
        blocks = map._notes;
        float bps = description._beatsPerMinute / 60;
        Debug.Log(bps);

        //change time to difference in seconds
        for (int i = 0; i < blocks.Count - 1; i++)
        {
            blocks[i]._time = (blocks[i + 1]._time - blocks[i]._time) / bps;
        }

        StartCoroutine(Generate(blocks));
    }

    private IEnumerator Generate(List<Block> blocks)
    {
        foreach (Block B in blocks)
        {
            Debug.Log(B._time);
            //TODO: intantiate the blocks
            GameObject gb;
            if (B._type == 0)
            {
                gb = Instantiate(RedBlock);
            }
            else
            {
                gb = Instantiate(BlueBlock);
            }
            gb.transform.position = getPosition(B._lineIndex, B._lineLayer);
            yield return new WaitForSeconds(B._time);
        }
    }

    private Vector3 getPosition(int col, int row)
    {
        float y = 1.5f + row * 0.5f;
        float x = -1 + col * 0.5f;
        return new Vector3(x, y, transform.position.z);
    }
}
