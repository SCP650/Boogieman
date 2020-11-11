using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBlocks : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
		DataTracker.on_miss(true, 10.0f); // TODO - set congruent and reaction time here
		Destroy(other.gameObject);
    }
}
