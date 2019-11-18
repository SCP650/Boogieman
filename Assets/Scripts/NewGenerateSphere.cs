using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGenerateSphere : MonoBehaviour
{
    //this script should be attached to the center of the game object you want spheres to generate from -- Sebastian Yang, or is it?
    [SerializeField] Vec3UnityEvent spherelocation;
    [SerializeField] GameObject SpherePrefab;
    // Start is called before the first frame update
    void Start()
    {
        spherelocation.AddListener(GiveMeSphere);
    }
    
    void GiveMeSphere(Vector3 location)
    {
        Instantiate(SpherePrefab, location, Quaternion.identity);
    }
}
