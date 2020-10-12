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


    // Start is called before the first frame update
    void Start()
    {
        //TODO: Adrian: return a list of block that contains block informaion
       GetBlock("Assets/ExpertPlus.json");
        StartCoroutine(Generate(blocks));
    }


    public void GetBlock(string songname)
    {
        using (StreamReader r = File.OpenText(songname))
        {
            string song = r.ReadToEnd();
            blocks = JsonConvert.DeserializeObject<List<Block>>(song);
        }

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
               gb =  Instantiate(RedBlock);
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
        float x = -1 + col *0.5f;
        return new Vector3(x, y, transform.position.z);
    }
}
