using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class discoTile : MonoBehaviour
{
    private Color[] colors;
    [SerializeField] bool swap = false;
    [SerializeField] float bpm = 104f;
    [SerializeField] Light [] lights;
    private Material materials;


    // Start is called before the first frame update
    void Start()
    {
        materials = this.GetComponent<Renderer>().material;
        lights = this.GetComponentsInChildren<Light>();
        colors = new Color[7];
        colors[0] = new Color(1f, 0f, 0f);
        colors[1] = new Color(1f, 157 / 255f, 0f);
        colors[2] = new Color(1f, 1f, 0f);
        colors[3] = new Color(0f, 1f, 25f / 255f);
        colors[4] = new Color(0, 251f / 255f, 1f);
        colors[5] = new Color(214f / 255f, 0f, 1f);
        colors[6] = new Color(1f, 0f, 188f / 255f);


        if (swap)
        {
            StartCoroutine(materialChange());
        }
    }

    private void setTileColor( Color c)
    {
        Debug.Log("Change color");

        float h, s, v;
        Color.RGBToHSV(c, out h, out s, out v);
        Color desat = Color.HSVToRGB(h, s * 0.3f, v);
        materials.SetColor("_BaseColor", c);
        materials.SetVector("_EmissionColor", desat);
        lights[0].color = c;
    }


    public void changeColor()
    {
        int val = Random.Range(0, 7);
        //Color color = colors[val];
        setTileColor(colors[val]);
    }
   IEnumerator materialChange()
    {
        while (true)
        {
            Debug.Log("yes");
            changeColor();
             
            /*int val = Random.Range(0, 7);
            Color color = colors[val];
            Color emitColor = emitColors[0];
            this.GetComponent<Renderer>().material.SetColor("_Color", color);
            foreach (Light l in lights)
            { 
                l.color = colors[val];
            }*/
            //this.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(191f, 1501, 150f));
            yield return new WaitForSeconds(1f/(bpm/60f));
        }
        //yield return null;

    }
}
