using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLine : MonoBehaviour
{
    [SerializeField] LineConfig config;

    IEnumerator MoveForward()
    {
        //TODO: is this forward or backward
        transform.position += (Vector3.forward * config.speed * Time.deltaTime);
        yield return null;
    }
}
