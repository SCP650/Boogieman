using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class discoTile : MonoBehaviour
{
    private Color[] colors;
    private Color[] emitColor;

    // Start is called before the first frame update
    void Start()
    {
        colors = new Color[7];
        colors[0] = new Color(0f, 85f, 120f);
        colors[1] = new Color(0f, 55f, 0f);
        colors[2] = new Color(85f, 0f, 30f);
        colors[3] = new Color(20f, 0f, 80f);
        colors[4] = new Color(50f, 20f, 0f);
        colors[5] = new Color(7, 55f, 0f);
        colors[6] = new Color(50f, 0f, 0f);
        emitColor = new Color[7];
        colors[0] = new Color(0f, 125f, 150f);
        colors[1] = new Color(0f, 85f, 0f);
        colors[2] = new Color(125f, 0f, 60f);
        colors[3] = new Color(0f, 0f, 120f);
        colors[4] = new Color(100f, 70f, 0f);
        colors[5] = new Color(25f, 85f, 0f);
        colors[6] = new Color(80f, 0f, 0f);
        StartCoroutine(materialChange());
    }

    IEnumerator materialChange()
    {
        while (true)
        {
            int val = Random.Range(0, 7);
            Color color = colors[val];
            this.GetComponent<Renderer>().material.SetColor("_Color", color);
            //this.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
            yield return new WaitForSeconds(0.5f);
        }
        yield return null;

    }
}
