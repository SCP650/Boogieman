using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLine : MonoBehaviour
{
    [SerializeField] LineConfig config;

    void Start() {
        StartCoroutine(MoveForward());
    }

    IEnumerator MoveForward()
    {
        while (transform.position.z >= -10) {
            //TODO: is this forward or backward
            transform.position += (Vector3.back * config.speed * Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
    }
}
