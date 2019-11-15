using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileMap : MonoBehaviour
{
    [SerializeField] GameObject [] tiles;
    Material[] materials;
    private string pattern;
    private Color[] colors;
    private Color[] options;
    private int startVal = 0;
    [SerializeField] Light[] lights;
    [SerializeField] UnitEvent beat;

  

    // Start is called before the first frame update
    void Start()
    {
        beat.AddListener(unit => change());
        pattern = "checkered";
        colors = new Color[7];
        options = new Color[7];
        options[0] = new Color(255f, 0f, 0f);
        options[1] = new Color(255f, 157f, 0f);
        options[2] = new Color(255f, 255f, 0f);
        options[3] = new Color(0f, 255f, 25f);
        options[4] = new Color(0, 251f, 255f);
        options[5] = new Color(214, 0f, 255f);
        options[6] = new Color(255f, 0f, 188f);
        changeCheckered();
        materials = new Material[75];
        int count = 0;
        foreach (GameObject tile in tiles)
        {
            materials[count] = new Material(tile.GetComponent<Renderer>().sharedMaterial);
            tile.GetComponent<Renderer>().material = materials[count];
            count += 1;
        }

    }




    public void change()
    {
        Debug.Log("Change print");

        if (pattern == "checkered")
        {
            int currCol = 0;
            int count = 0;
            foreach (GameObject tile in tiles)
            {
                lights = tile.GetComponentsInChildren<Light>();
                //tile.GetComponent<Renderer>().material.SetColor("_Color", colors[currCol %2]);
                materials[count].SetColor("_Color", colors[currCol % 2]);
                foreach (Light l in lights)
                {
                    l.color = colors[currCol % 2];
                }
                currCol += 1;
                count += 1;
            }
            Color temp = colors[0];
            colors[0] = colors[1];
            colors[1] = temp;
        }
        else if (pattern == "spin")
        {
            int currCol = 0;
            int count = 0;
            foreach (GameObject tile in tiles)
            {
               lights = tile.GetComponentsInChildren<Light>();
                //tile.GetComponent<Renderer>().material.SetColor("_Color", colors[(currCol + startVal) % 7]);
                materials[count].SetColor("_Color", colors[(currCol + startVal) % 7]);
                foreach (Light l in lights)
                {
                    l.color = colors[(currCol + startVal) % 7];
                }
                currCol += 1;
                count += 1;
            }
            //startVal += 1;


        }
        else if (pattern == "center")
        {
            int tileNum = 0;
            foreach (GameObject tile in tiles)
            {
                lights = tile.GetComponentsInChildren<Light>();
                if (tileNum < 36)
                {
                    //tile.GetComponent<Renderer>().material.SetColor("_Color", colors[0]);
                    materials[tileNum].SetColor("_Color", colors[0]);
                    foreach (Light l in lights)
                    {
                        l.color = colors[0];
                    }

                }
                else if (tileNum < (28 + 36))
                {
                    materials[tileNum].SetColor("_Color", colors[1]);
                   // tile.GetComponent<Renderer>().material.SetColor("_Color", colors[1]);
                    foreach (Light l in lights)
                    {
                        l.color = colors[1];
                    }
                }
                else if (tileNum < (11 + 28 + 36))
                {
                    materials[tileNum].SetColor("_Color", colors[2]);
                    //tile.GetComponent<Renderer>().material.SetColor("_Color", colors[2]);
                    foreach (Light l in lights)
                    {
                        l.color = colors[2];
                    }
                }
                tileNum += 1;
             
            }
            Color temp = colors[0];
            colors[0] = colors[2];
            colors[2] = colors[1];
            colors[1] = temp;
        }
    }


    public void changeCheckered()
    {
        int val = Random.Range(0, 7);
        colors[0] = options[val];
        colors[1] = options[6 - val];
        pattern = "checkered";

    }
    public void spinPattern()
    {
        //startVal = Random.Range(0, 7);
        for (int i = 0; i < 7; i++)
        {
            colors[i] = options[i];
        }
    }
    public void centerPattern()
    {
        int val = Random.Range(0, 7);
        int val2 = Random.Range(0, 7);
        int val3 = Random.Range(0, 7);
        while (val2 == val)
        {
            val2 = Random.Range(0, 7);
        }
       
        while(val3 == val || val3 == val2)
        {
            val3 = Random.Range(0, 7);
        }
        colors[0] = options[val];
        colors[1] = options[val2];
        colors[2] = options[val3];
     }
}
