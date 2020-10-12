using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class generateBlocks : MonoBehaviour
{

    private List<Block> blocks;

    public void GetBlock(string songname)
    {
        using (StreamReader r = File.OpenText(songname))
        {
            string song = r.ReadToEnd();
            blocks = JsonConvert.DeserializeObject<List<Block>>(song);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //TODO: Adrian: return a list of block that contains block informaion
        GetBlock("ExpertPlus.json");
        StartCoroutine(Generate(blocks));
    }

    private IEnumerator Generate(List<Block> blocks)
    {
        foreach (Block B in blocks)
        {
            //TODO: intantiate the blocks
            yield return new WaitForSeconds(2);
        }
    }
}
