using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beat : MonoBehaviour
{

    
    //Mine stuff
    public bool isMine = false;
    public MeshRenderer mineMesh;
    private int mineLayer = 10;

    //Color Enums 
    public enum Color { blue, red }
    public Color color;
    private int blueLayer = 8;
    private int redLayer = 9;

    
    //Color Direction enums 
    public enum Dir{top, left, right, bottom, topright, topleft, bottomright, bottomleft}
    public Dir dir;
    public bool Omnidirectional = true;
    private float direction;
    public int pointVal = 4;


    //Materials variables
    public Material RedMaterial;
    public Material RedOmniMaterial;
    public Material RedMaterialStoop;
    public Material BlueMaterial;

    public Material BlueOmniMaterial;
    public Material BlueMaterialStoop;

    //Mesh Variables
    public MeshRenderer leftSide;
    public MeshRenderer rightSide;

    private bool materialSet = false;

    private float time_created;


	private void Awake() {
        time_created = Time.time;
	}

	void Start()
    {

        if (!materialSet) //If block was set beforehand, we don't need to set it again during runtime
        {
            SetBlock();
        }

    }

    public void SetBlock()
    {
        if (isMine)
        {
            gameObject.layer = mineLayer;
            mineMesh.GetComponent<Renderer>().enabled = true;
            leftSide.GetComponent<Renderer>().enabled = false;
            rightSide.GetComponent<Renderer>().enabled = false;
            return;
        }
        mineMesh.GetComponent<Renderer>().enabled = false;
        leftSide.GetComponent<Renderer>().enabled = true;
        rightSide.GetComponent<Renderer>().enabled = true;
        switch (color)
        {
            case Color.blue: //TODO make this a function
                gameObject.layer = blueLayer;
                if (Omnidirectional)
                {
                    leftSide.material = BlueOmniMaterial;
                    rightSide.material = BlueOmniMaterial;
                    break;
                }
              /*  if (isStroop)
                {
                    leftSide.material = BlueMaterialStoop;
                    rightSide.material = BlueMaterialStoop;
                    break;
                }*/
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
           /*     if (isStroop)
                {
                    leftSide.material = RedMaterialStoop;
                    rightSide.material = RedMaterialStoop;
                    break;
                }*/
                leftSide.material = RedMaterial;
                rightSide.material = RedMaterial;
                break;
        }
        if (!Omnidirectional) //only rotates if we're not omnidirectional, there's no point to rotate if we are
        {
            switch (dir)
            {
                case Dir.top:
                    /*   if (isStroop) { direction = 180.0f; }
                       else { direction = 0.0f; }*/
                    direction = 0.0f;
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                case Dir.bottom:
                    /*  if (isStroop) { direction = 0.0f; }
                      else { direction = 180.0f; }*/
                    direction = 180.0f;
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, direction);
                    break;
                case Dir.right:
                    /*if (isStroop) { direction = 270.0f; }
                    else { direction = 90.0f; }*/
                    direction = 90.0f;
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, direction);
                    break;
                case Dir.left:
                    /* if (isStroop) { direction = 90.0f; }
                     else { direction = 270.0f; }*/
                    direction = 270.0f;
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, direction);
                    break;
                case Dir.topright:
                    direction = 315.0f;
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, direction);
                    break;
                case Dir.topleft:
                    direction = 45.0f;
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, direction);
                    break;
                case Dir.bottomright:
                    direction = 225.0f;
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, direction);
                    break;
                case Dir.bottomleft:
                    direction = 135.0f;
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, direction);
                    break;

            }
            materialSet = true;
        }
        
    }

    
    // Returns the amount of time that has passed since the block was created
    public float time_since_creation() {
        return Time.time - time_created;
	}
}
