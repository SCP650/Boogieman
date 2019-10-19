using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingSphere : MonoBehaviour
{
    float angle = 0;
    float speed = (2 * Mathf.PI) / 5; //2*PI in degress is 360, so you get 5 seconds to complete a circle
    float radius = 2;
    float startx;
    float starty;

    [SerializeField] UnitEvent start;
    [SerializeField] UnitEvent stop;

    private void Start()
    {
        startx = transform.position.x;
        starty = transform.position.y;
        StartCoroutine(Line());
    }
    void Update()
    {
        angle += speed * Time.deltaTime; //if you want to switch direction, use -= instead of +=
        float x = Mathf.Cos(angle) * radius + startx;
        float y = Mathf.Sin(angle) * radius + starty;
        transform.position = new Vector3(x, y, transform.position.z);
    }

    IEnumerator Line()
    {
        while (true) {
            start.Invoke();
            Debug.Log("Try to generate line");
            yield return new WaitForSeconds(2);
            Debug.Log("Try to stop line");
            stop.Invoke();
            yield return new WaitForSeconds(3);
        }
    }
}
