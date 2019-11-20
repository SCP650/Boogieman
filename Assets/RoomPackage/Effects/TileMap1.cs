using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileMap1 : MonoBehaviour
{
    [SerializeField] GameObject[] tiles;
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
        pattern = "center";
        colors = new Color[7];
        options = new Color[7];
        lights = new Light[81];
        options[0] = new Color(1f, 0f, 0f);
        options[1] = new Color(1f, 157 / 255f, 0f);
        options[2] = new Color(1f, 1f, 0f);
        options[3] = new Color(0f, 1f, 25f / 255f);
        options[4] = new Color(0, 251f / 255f, 1f);
        options[5] = new Color(214f / 255f, 0f, 1f);
        options[6] = new Color(1f, 0f, 188f / 255f);
        centerPattern();
        materials = new Material[81];
        int count = 0;
        foreach (GameObject tile in tiles)
        {
            foreach (Light l in tile.GetComponentsInChildren<Light>())
            {
                lights[count] = l;
            }
            materials[count] = new Material(tile.GetComponent<Renderer>().sharedMaterial);
            tile.GetComponent<Renderer>().material = materials[count];
            count += 1;
        }

    }

    private void setTileColor(int num, Color c)
    {
       
        float h, s, v;
        Color.RGBToHSV(c, out h, out s, out v);
                    Color desat = Color.HSVToRGB(h, s * 0.3f, v);
        materials[num].SetColor("_BaseColor", c);
        materials[num].SetVector("_EmissionColor", desat);
        lights[num].color = c;
    }


    public void change()
    {
        Color c = new Color();
        if (pattern == "checker")
        {
            
            int currCol = 0;
            int count = 0;
            foreach (GameObject tile in tiles)
            {
                c = colors[currCol % 2];
                lights[count].color = c;
                setTileColor(count, c);
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
                c = colors[(currCol + startVal) % 7];
                setTileColor(count, c);
                currCol += 1;
                count += 1;
            }
            startVal += 1;


        }
        else if (pattern == "center")
        {
            int tileNum = 0;
           
            foreach (GameObject tile in tiles)
            {
        
                if (tileNum < (32))
                {
                    c = colors[0];
                }
                else if (tileNum < (56))
                {
                    c = colors[1];
                }
                else if (tileNum < 72)
                {
                    c = colors[2];
                }
                else if (tileNum < (80))
                {
                    c = colors[3];
                }
                else
                {
                    c = colors[4];
                }
                setTileColor(tileNum, c);
               
                tileNum += 1;
            }
            Color temp = colors[0];
            colors[0] = colors[1];
            colors[1] = colors[2];
            colors[2] = colors[3];
            colors[3] = colors[4];
            colors[4] = temp;
        }
    }


    public void checkPattern()
    {
        int val = Random.Range(0, 7);
        colors[0] = options[val];
        colors[1] = options[6 - val];
        pattern = "checker";

    }
    public void spinPattern()
    {
        //startVal = Random.Range(0, 7);
        for (int i = 0; i < 7; i++)
        {
            colors[i] = options[i];
        }
        pattern = "spin";
    }
    public void centerPattern()
    {
        for (int i = 0; i < 7; i++)
        {
            colors[i] = options[i];
        }
        pattern = "center";
        /*int val = Random.Range(0, 7);
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
        colors[3] = options[val4];
        colors[5] = */
     }
}
