using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class discoTile : MonoBehaviour
{
    private Color[] colors;
    private Color[] emitColors;
    [SerializeField] float bpm;
    [SerializeField] Light [] lights;

    // Start is called before the first frame update
    void Start()
    {
        lights = this.GetComponentsInChildren<Light>();
        colors = new Color[7];
        colors[0] = new Color(209f, 0f, 0f);
        colors[1] = new Color(0f, 55f, 0f);
        colors[2] = new Color(85f, 0f, 30f);
        colors[3] = new Color(20f, 0f, 80f);
        colors[4] = new Color(50f, 20f, 0f);
        colors[5] = new Color(7, 55f, 0f);
        colors[6] = new Color(50f, 0f, 0f);
        emitColors = new Color[7];
        colors[0] = Color.HSVToRGB(0f, 21, 75f);
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
            Color emitColor = emitColors[0];
            this.GetComponent<Renderer>().material.SetColor("_Color", color);
            foreach (Light l in lights)
            { 
                l.color = colors[val];
            }
            //this.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(191f, 1501, 150f));
            yield return new WaitForSeconds(1f/(bpm/60f));
        }
        yield return null;

    }
}
