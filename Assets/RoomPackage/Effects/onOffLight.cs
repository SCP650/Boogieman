using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onOffLight : MonoBehaviour
{
    [SerializeField] bool on = false;
    [SerializeField] Session sess;
    Light l;
    Material m;
    // Start is called before the first frame update
    void Start()
    {
        l = this.GetComponent<Light>();
        m = this.GetComponent<Renderer>().material;
        m.EnableKeyword("_EMISSION");
        //sess.AddStartListener(() => turnSelfOn());
        //sess.AddStopListener(() => turnSelfOff());


        //.AddListener(() => turnSelfOn());
        //this.AddListener(() => turnSelfOff());


        //this.GetComponent<Renderer>().material = m;

    }

    public void turnSelfOn()
    {
        Debug.Log("on");
        float h, s, v;
        Color c = m.GetColor("_Color");
        Color.RGBToHSV(c, out h, out s, out v);
        Color desat = Color.HSVToRGB(h, s, 0.85f* v);
        m.SetVector("_EmissionColor", desat);
       // m.SetColor("_EmissionColor", desat);
        //this.GetComponent<Renderer>().material = m;
        l.enabled = true;
        


    }
    public void turnSelfOff()
    {
        Debug.Log("off");
        l.enabled = false;
        m.SetVector("_EmissionColor", Color.black);
    }

    
}


