using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotateHand : MonoBehaviour
{
    enum Direction { Up, Left, Right, Center };
    float timeCount = 0;
    [SerializeField] private float speed = 4;
    [SerializeField] private float radius = 0.25f;
    [SerializeField] private Direction direction;
    [SerializeField] private bool is_right_hand;
    // Update is called once per frame

    private float z;
    private float x;
    private float y;
    private void Start()
    {
        z = 0;
        x = 0;
        y = 0;
        switch (direction)
        {
            case Direction.Up:
                transform.Rotate(-90, 0, 0);
                break;
            case Direction.Left:
                transform.Rotate(-180, 90, 0);
                break;
            case Direction.Right:
                transform.Rotate(0, 90, 0);
                break;
            case Direction.Center:
                if (is_right_hand)
                {
                    transform.Rotate(-180, 90, 0);
                }
                else
                {
                    transform.Rotate(0, 90, 0);
                }
                break;
                    

        }
    }
    void Update()
    {
        timeCount += Time.deltaTime * speed;
        switch (direction)
        {
            case Direction.Up:
                z = Mathf.Cos(timeCount) * radius;
                x = Mathf.Sin(timeCount) * radius;
                y = 0;
                break;
            case Direction.Left:
                y = Mathf.Cos(timeCount) * radius;
                z = Mathf.Sin(timeCount) * radius;
                x = -5;
                break;
            case Direction.Right:
                y = Mathf.Cos(timeCount) * radius;
                z = Mathf.Sin(timeCount) * radius;
                x = 5;
                break;
            default:
                Debug.Log("F**k you, you didn't enter direction in editor.");
                break;
        }
        transform.position = new Vector3(x, y, z);




    }
}
