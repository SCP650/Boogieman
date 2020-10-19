using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beat : MonoBehaviour
{
    //Color Enums 
    public enum Color { blue, red }
    public Color color;
    private int blueLayer = 8;
    private int redLayer = 9;
    //Color Direction enums 
    public enum Dir{top, left, right, bottom }
    public Dir dir;
    public bool Omnidirectional = true;
    private float direction;


    //Materials variables
    public Material RedMaterial;
    public Material RedOmniMaterial;
    public Material BlueMaterial;
    public Material BlueOmniMaterial;
    public MeshRenderer leftSide;
    public MeshRenderer rightSide;

    private bool materialSet = false;
    
    void Start()
    {

        if (!materialSet)
        {
            SetBlock();
        }

    }

    public void SetBlock()
    {

        switch (color)
        {
            case Color.blue:
                gameObject.layer = blueLayer;
                if (Omnidirectional)
                {
                    leftSide.material = BlueOmniMaterial;
                    rightSide.material = BlueOmniMaterial;
                    break;
                }
                rightSide.material = BlueMaterial;
                leftSide.material = BlueMaterial;
                break;
            case Color.red:
                gameObject.layer = redLayer; 
                if (Omnidirectional)
                {
                    leftSide.material = RedOmniMaterial;
                    rightSide.material = RedOmniMaterial;
                    break;
                }
                leftSide.material = RedMaterial;
                rightSide.material = RedMaterial;
                break;
        }
        if (!Omnidirectional) //only rotates if we're not omnidirectional, there's no point to rotate if we are
        {
            switch (dir)
            {
                case Dir.top:
                    direction = 0.0f;
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                case Dir.bottom:
                    direction = 180.0f;
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, direction);
                    break;
                case Dir.right:
                    direction = 90.0f;
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, direction);
                    break;
                case Dir.left:
                    direction = 270.0f;
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, direction);
                    break;
            }
            materialSet = true;
        }
        
    }
}
