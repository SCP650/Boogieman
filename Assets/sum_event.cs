using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sum_event : MonoBehaviour
{
    [SerializeField] private IntRef result;

    [SerializeField] private IntEvent input;
    // Start is called before the first frame update
    void Start()
    {
        result.val = 0;
        input.AddListener(i => result.val += i);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
