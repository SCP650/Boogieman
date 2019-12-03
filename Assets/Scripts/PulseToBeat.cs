using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseToBeat : MonoBehaviour
{
    [SerializeField] private UnitEvent beat;
    [SerializeField] private AnimationCurve curve;
    private System.Action removeme;
    private Color originalColor;
    private Vector3 originalSize;
    // Start is called before the first frame update

    void Start()
    {
        originalColor = this.GetComponent<Renderer>().material.color;
        removeme = beat.AddRemovableListener(unit => Pulsate());
        originalSize = transform.localScale;
        //Debug.Log($"This is the original size: {originalSize}");
    }

    
    private void OnDestroy()
    {
        removeme();
        StopAllCoroutines();
    }
    void Pulsate()
    {
        StartCoroutine(PulsateBlockColor());
        StartCoroutine(PulsateBlockSize());
    }


    IEnumerator PulsateBlockColor()
    {
        //one color change per beat
        /*
        Color colorOfBlock = this.GetComponent<Renderer>().material.color;

        if(colorOfBlock == Color.white)
        {
            this.GetComponent<Renderer>().material.color = originalColor;
        }
        else
        {
            this.GetComponent<Renderer>().material.color = Color.white;
        }
        
        yield return null;
        */

        //green and white change in one beat
        this.GetComponent<Renderer>().material.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        this.GetComponent<Renderer>().material.color = originalColor;
    }
    IEnumerator PulsateBlockSize()
    {
        for (float dur = 0; dur < 1; dur += Time.deltaTime*3)
        {
            transform.localScale = originalSize * curve.Evaluate(dur);
            yield return null;
        }
    }
}
