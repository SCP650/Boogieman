using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin : MonoBehaviour
{
    [SerializeField] float speed=100f;
    [SerializeField] float direction = 1f;
    [SerializeField] float time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        if (time > 0)
        {
            StartCoroutine(switchDirection());
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0, direction * speed * Time.deltaTime, 0), Space.World);

    }

    IEnumerator switchDirection()
    {
        while (true)
        {
            direction = -direction;
            yield return new WaitForSeconds(time);
        }
        yield return null;

    }
}
