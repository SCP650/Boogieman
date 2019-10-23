using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class set_to_mouse : MonoBehaviour
{
    [SerializeField] private ControllerObject controller_to_control;
   
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(set_position(GetComponent<Camera>()));
    }

    IEnumerator set_position(Camera cam)
    {
        while (true)
        {
            var screen_point = Input.mousePosition;
            var world_point = cam.ScreenToWorldPoint(screen_point);
            controller_to_control.set(new Vector3(0, world_point.y, world_point.z));
            yield return null;
        }
        
    }
}
