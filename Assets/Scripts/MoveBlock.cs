using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour
{
    [SerializeField] private FloatRef speed;
    
    void Start()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        while(true)
        {
            this.transform.position += new Vector3(0, 0, speed.val * Time.deltaTime);
            yield return null;
        }
    }
}
