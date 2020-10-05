using System.Collections;
using System.Collections.Generic;
//using UnityEditor.UIElements;
using UnityEngine;

public class PolarAttribute : PropertyAttribute
{
    public float scale = 1;

    public float theta { get; set; }
    
    
    public PolarAttribute(float scale){
        this.scale = scale;
        
    }
}
