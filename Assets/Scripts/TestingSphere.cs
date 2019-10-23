using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingSphere : MonoBehaviour
{
    [SerializeField] private bool shouldRotate;
    [SerializeField] private KeyCode trigger;
    float angle = 0;
    float speed = (2 * Mathf.PI) / 5; //2*PI in degress is 360, so you get 5 seconds to complete a circle
    float radius = 1;
    float startx;
    float starty;

    [SerializeField] Session session;

    private void Start()
    {
        startx = transform.position.x;
        starty = transform.position.y;
        StartCoroutine(Line());
    }
    void Update()
    {
        //angle += speed * Time.deltaTime; //if you want to switch direction, use -= instead of +=
        //float x = Mathf.Cos(angle) * radius + startx;
        //float y = Mathf.Sin(angle) * radius + starty;
        //transform.position = new Vector3(x, y, transform.position.z);


    }

    IEnumerator Line()
    {
        while (true) {
            yield return new WaitUntil(() => Input.GetKeyDown(trigger));
            session.Invoke();
            Debug.Log("Try to generate line");
            yield return new WaitUntil(() => Input.GetKeyUp(trigger));
            session.Invoke();
            Debug.Log("Try to stop line");
        }
    }
}
